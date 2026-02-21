using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class HomeHandler : IDisposable
{
    private readonly HomeElements _elements;
    private readonly Subject<Unit> _onNext;
    private readonly IItemsHandler _itemsHandler;
    public Subject<Unit> OnNext => _onNext;
    private ICharacterHandler _characterHandler;
    private IDisposable _loveSubscription;
    private readonly CompositeDisposable _disposables;
    private CancellationTokenSource _tutorialCts;
    private CancellationTokenSource _presentCts;
    private CancellationTokenSource _talkCts;
    private bool _runningTutorial = false;
    private bool _runningPresent = false;
    private bool _runningTalk = false;

    public HomeHandler(HomeElements elements, IItemsHandler itemsHandler)
    {
        _elements = elements;
        _onNext = new Subject<Unit>();
        _disposables = new CompositeDisposable();
        _itemsHandler = itemsHandler;

        _elements.TalkController.CharaTalkButton.onClick.AddListener(OnTalkButton);
        _elements.TalkController.MiniTalkButton.onClick.AddListener(OnTalkButton);
        _elements.Presents.OnPresent.Subscribe(t => RunPresent(t, CancellationToken.None).Forget()).AddTo(_disposables);
        _elements.PresentButton.onClick.AddListener(OnPresentButton);
        _elements.SwitchCharaButton.onClick.AddListener(OnSwitchCharaButton);
    }

    public void Initialize(ICharacterHandler handler, ItemType[] filter)
    {
        _characterHandler = handler;
        _elements.LevelSlider.value = _characterHandler.Love.CurrentValue / 100f;
        _elements.Presents.SetFilter(filter);
        _loveSubscription?.Dispose();
        _loveSubscription = _characterHandler.Love.Subscribe(_ =>
        {
            _elements.LoveLvText.text = "Lv." + _characterHandler.GetLoveLv();
            _elements.LevelSlider.value = _characterHandler.GetLoveRatio();
        });
    }
    
    public async UniTask RunTutorial(CancellationToken token)
    {
        if (_runningTutorial) return;
        _tutorialCts = _tutorialCts.Reset();
        var linkedToken = _tutorialCts.LinkedToken(token);
        
        _runningTutorial = true;
        try
        {
            await _characterHandler.Tutorial(linkedToken);
        }
        finally
        {
            _runningTutorial = false;
        }
    }
    
    private async UniTask RunPresent(ItemType type, CancellationToken token)
    {
        if(_runningPresent) return;
        _presentCts = _presentCts.Reset();
        var linkedToken = _presentCts.LinkedToken(token);
        
        _runningPresent = true;
        try
        {
            var number = _itemsHandler.UseItem(type);
            number = Math.Max(0, number);
            await _characterHandler.Present(type, number, linkedToken);
        }
        finally
        {
            _runningPresent = false;
        }
    }
    
    private void OnTalkButton()
    {
        if (!_runningTalk && !_runningPresent && !_runningTutorial)
        {
            RunTalk().Forget();
        }
        else
        {
            _onNext.OnNext(Unit.Default);
        }
    }

    private async UniTask RunTalk()
    {
        if (_runningTalk) return;
        _talkCts = _talkCts.Reset();
        _runningTalk = true;
        try
        {
            await _characterHandler.RandomTalk(_talkCts.Token);
        }
        finally
        {
            _runningTalk = false;
        }
    }
    
    private void OnPresentButton()
    {
        _elements.Presents.Popup.Show();
    }
    
    private void OnSwitchCharaButton()
    {
        _elements.CharacterSelector.Popup.Show();
    }

    public void Dispose()
    {
        _elements.TalkController.CharaTalkButton.onClick.RemoveListener(OnTalkButton);
        _elements.TalkController.MiniTalkButton.onClick.RemoveListener(OnTalkButton);
        _loveSubscription?.Dispose();
        _disposables.Dispose();
    }
}
