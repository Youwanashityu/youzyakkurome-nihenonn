using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine.UI;

public class HomeHandler : IDisposable
{
    private readonly Button _talkButton;
    private readonly Subject<Unit> _onNext;
    public Subject<Unit> OnNext => _onNext;
    private ICharacterHandler _characterHandler;
    private InventoryHandler _inventoryHandler;
    private CancellationTokenSource _cts;
    private bool _running = false;

    public HomeHandler(HomeElements elements)
    {
        var talkController = elements.TalkController;
        _talkButton = talkController.TalkButton;
        _onNext = new Subject<Unit>();
        
        _talkButton.onClick.AddListener(OnTalkButton);
    }

    public void Initialize(ICharacterHandler handler)
    {
        _characterHandler = handler;
    }
    
    public async UniTask RunTutorial(CancellationToken token)
    {
        // テスト楽にしたかったのでrunningによるreturnはナシ
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);
        
        _running = true;
        try
        {
            await _characterHandler.Tutorial(linkedToken);
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
        try
        {
            await _characterHandler.Present(type, linkedToken);
        }
        finally
        {
            _running = false;
        }
    }
    
    private void OnTalkButton()
    {
        _cts = _cts.Reset();
        if (!_running)
        {
            _characterHandler.RandomTalk(_cts.Token).Forget();
        }
        else
        {
            _onNext.OnNext(Unit.Default);
        }
    }

    public void Dispose()
    {
        _talkButton.onClick.RemoveListener(OnTalkButton);
    }
}
