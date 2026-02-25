using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Henohenon.Scripts.GameUnity.General;
using R3;
using UnityEngine;

public class TalkTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private HomeElements homeElements;
    [SerializeField]
    private GeneralElements generalElements;
    [SerializeField]
    private ItemInfoScriptable itemInfo;
    [SerializeField]
    private GraimScriptable graim;
    [SerializeField]
    private LuxScriptable lux;

    private LuxTalkHandler _luxHandler;
    private GraimTalkHandler _graimHandler;

    private Subject<Unit> _onNext;
    
    private void Awake()
    {
        _onNext = new();
        
        _luxHandler = new LuxTalkHandler(homeElements.TalkController, generalElements.VoicePlayer, _onNext, lux.GetPureData);
        _graimHandler = new GraimTalkHandler(homeElements.TalkController, generalElements.VoicePlayer, _onNext, graim.GetPureData);
        
        homeElements.TalkController.CharaTalkButton.onClick.AddListener(OnTalkButton);
        homeElements.TalkController.MiniTalkButton.onClick.AddListener(OnTalkButton);
    }
    
    private void OnTalkButton()
    {
        _onNext.OnNext(Unit.Default);
    }
    
    private void OnDestroy()
    {
        _onNext.Dispose();
        _luxHandler.Dispose();
        _graimHandler.Dispose();
    }
    
    [Header("テスト用")]
    [SerializeField]
    private GraimTalkType graimTalkType;
    [SerializeField]
    private LuxTalkType luxTalkType;

    private GraimTalkType _lastGraimTalkType;
    private LuxTalkType _lastLuxTalkType;

    private void OnValidate()
    {
        if (_lastGraimTalkType != graimTalkType)
        {
            _lastGraimTalkType = graimTalkType;
            _graimHandler.ExecTalk((int)graimTalkType, CancellationToken.None).Forget();
        }
        else if (_lastLuxTalkType != luxTalkType)
        {
            _lastLuxTalkType = luxTalkType;
            _luxHandler.ExecTalk((int)luxTalkType, CancellationToken.None).Forget();
        }
    }
}
