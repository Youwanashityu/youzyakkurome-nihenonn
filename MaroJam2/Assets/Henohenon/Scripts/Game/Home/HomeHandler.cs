using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class HomeHandler : IDisposable
{
    private readonly Button _talkButton;
    private readonly LuxTalkHandler _luxTalkHandler;
    private readonly IReadOnlyDictionary<int, LuxTalkType[]> _randomTalks;
    private readonly Subject<Unit> _onNext;
    private CancellationTokenSource _cts;
    private bool _running = false;

    public HomeHandler(HomeElements elements, LuxTalkScriptable luxTalk)
    {
        var talkController = elements.TalkController;
        _talkButton = talkController.TalkButton;
        _randomTalks = luxTalk.RandomTalks;
        _onNext = new Subject<Unit>();
        _luxTalkHandler = new LuxTalkHandler(talkController, _onNext, luxTalk.Images, luxTalk.Voices, luxTalk.SimpleParams);
        
        _talkButton.onClick.AddListener(OnTalkButton);
    }

    public async UniTask RunTutorial(CancellationToken token)
    {
        // テスト楽にしたかったのでrunningによるreturnはナシ
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);
        
        _running = true;
        try
        {
            await _luxTalkHandler.ExecTalk(LuxTalkType.Tutorial, linkedToken);
        }
        finally
        {
            _running = false;
        }
    }

    public async UniTask RunTalk(LuxTalkType type, CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);
        
        _running = true;
        try
        {
            await _luxTalkHandler.ExecTalk(type, linkedToken);
        }
        finally
        {
            _running = false;
        }
    }

    public async UniTask RunPresent(ItemType type, CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);
        
        _running = true;
        var talkType = type switch
        {
            ItemType.SunShine => LuxTalkType.LIKE01_Hello,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        try
        {
            await _luxTalkHandler.ExecTalk(talkType, linkedToken);
        }
        finally
        {
            _running = false;
        }
    }
    
    private void OnTalkButton()
    {
        if (!_running)
        {
            var loveLevel = 0;
            var talkList = _randomTalks[loveLevel];
            var talkType = talkList[Random.Range(0, talkList.Length)];
            _cts = _cts.Reset();
            RunTalk(talkType, _cts.Token).Forget();
        }
        else
        {
            _onNext.OnNext(Unit.Default);
        }
    }

    public void Dispose()
    {
        _luxTalkHandler.Dispose();
        _talkButton.onClick.RemoveListener(OnTalkButton);
    }
}
