using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine.UI;

public class HomeHandler : IDisposable
{
    private readonly HomeElements _elements;
    private readonly Subject<Unit> _onNext;
    public Subject<Unit> OnNext => _onNext;
    private ICharacterHandler _characterHandler;
    private IDisposable _loveSubscription;
    private readonly CompositeDisposable _disposables;
    private InventoryHandler _inventoryHandler;
    private CancellationTokenSource _cts;
    private bool _running = false;

    public HomeHandler(HomeElements elements, InventoryHandler inventoryHandler)
    {
        _elements = elements;
        _onNext = new Subject<Unit>();
        _disposables = new CompositeDisposable();
        _inventoryHandler = inventoryHandler;

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
        if (_running) return;
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
    
    private async UniTask RunPresent(ItemType type, CancellationToken token)
    {
        if (_running) return;
        _cts = _cts.Reset();
        
        _running = true;
        try
        {
            var number = _inventoryHandler.UseItem(type);
            number = Math.Max(0, number);
            await _characterHandler.Present(type, number, _cts.Token);
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
