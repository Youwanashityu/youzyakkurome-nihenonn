
using System.Collections.Generic;
using Henohenon.Scripts.GameUnity.General;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class SoundHandler
{
    private readonly GeneralElements _generalElements;
    private readonly Button _startButton; 
    private readonly CompositeDisposable _disposable;
    
    public SoundHandler(GeneralElements generalElements, TitleElements titleElements, ICharacterHandler[] characters, GatyaHandler gatyaHandler)
    {
        _generalElements = generalElements;
        _startButton = titleElements.StartButton;
        _disposable = new CompositeDisposable();
        
        _startButton.onClick.AddListener(OnClickDesitionSe);
        gatyaHandler.GatyaController.OnStartGatya.Subscribe(_ => OnGatyaStartSe()).AddTo(_disposable);
        gatyaHandler.GatyaController.OnPickItem.Subscribe(tier =>
        {
            if (tier == ItemTier.Common) OnGatyaNSe();
            else OnGatyaSrSe();
        }).AddTo(_disposable);
        foreach (var chara in characters)
        {
            chara.Love.Subscribe(_ => OnLikeUp()).AddTo(_disposable);
        }
    }

    private void OnClickSe() => PlaySe(_generalElements.Click);
    private void OnClickDesitionSe() => PlaySe(_generalElements.ClickDecision);
    private void OnGatyaStartSe() => PlaySe(_generalElements.GatyaStart);
    private void OnGatyaNSe() => PlaySe(_generalElements.GatyaSingleN);
    private void OnGatyaSrSe() => PlaySe(_generalElements.GatyaSingleSr);
    private void OnLikeUp() => PlaySe(_generalElements.GatyaSingleN);
    private void OnLikeDown() => PlaySe(_generalElements.GatyaSingleSr);

    private void PlaySe(AudioClip clip)
    {
        _generalElements.SoundEffectsPlayer.Play(clip);
    }

    public void Dispose()
    {
        _disposable.Dispose();
        _startButton.onClick.AddListener(OnClickDesitionSe);
    }
}