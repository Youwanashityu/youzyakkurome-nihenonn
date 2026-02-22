using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using R3;
using UnityEngine;

public class GraimTalkHandler : TalkHandler, IDisposable
{
    private readonly TalkController _talkController;
    private readonly IVoicePlayer _voicePlayer;
    private readonly CharacterData _data;
    private readonly Observable<Unit> _onNext;

    private CancellationTokenSource _cts;

    public GraimTalkHandler(TalkController talkController, IVoicePlayer voicePlayer, Observable<Unit> onNext,
        CharacterData data)
    {
        _talkController = talkController;
        _voicePlayer = voicePlayer;
        _onNext = onNext;
        _data = data;
    }

    protected override async UniTask Talk(int t, CancellationToken token)
    {
        var type = (GraimTalkType)t;
        _talkController.TalkBox.gameObject.SetActive(true);
        switch (type)
        {
            default:
                if (!_data.SimpleParams.TryGetValue(t, out var param))
                {
                    throw new ArgumentException("Invalid talk type");
                }
                
                Voice(param.Voice);
                Image(param.Image);

                foreach (var text in param.Texts)
                {
                    await Text(text);
                }
                break;
        }
        _talkController.TalkBox.SetActive(false);

        async UniTask Delay(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
        }

        async UniTask Text(string text)
        {
            _talkController.TalkText.text = text;
            await _onNext.FirstAsync(token);
        }

        void Image(int imageType)
        {
            _talkController.MiniTalkButton.gameObject.SetActive(false);
            _talkController.CharaTalkButton.gameObject.SetActive(true);
            if ((GraimImageType)imageType == GraimImageType.None) return;
            if (_data.Images.TryGetValue(imageType, out var image)) _talkController.CharacterImage.sprite = image;
        }

        void MiniImage(int imageType)
        {
            _talkController.MiniTalkButton.gameObject.SetActive(true);
            _talkController.CharaTalkButton.gameObject.SetActive(false);
            if ((GraimImageType)imageType == GraimImageType.None) return;
            if (_data.Images.TryGetValue(imageType, out var image)) _talkController.MiniCharaImage.sprite = image;
        }
        
        void Voice(int voiceType)
        {
            if (_data.Voices.TryGetValue(voiceType, out var voice)) _voicePlayer.Play(voice);
        }

        async UniTask<SelectionType> Question(string alpha, string beta)
        {
            return await _talkController.Question(alpha, beta, token);
        }
    }

    public override async UniTask Tutorial(CancellationToken token)
    {

    }
}