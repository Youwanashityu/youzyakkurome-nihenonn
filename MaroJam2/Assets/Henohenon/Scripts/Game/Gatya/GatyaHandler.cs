using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

public class GatyaHandler : IDisposable
{
    private readonly GatyaElements _elements;
    private readonly GatyaTable _luxTable;
    private readonly ItemInfo _itemInfo;
    private readonly int _maxTenjoCount = 50;
    private int _tenjoCount = 10;
    private CancellationTokenSource _cts;
    private readonly Subject<Unit> _onStartGatya = new ();
    private readonly Subject<ItemType> _onGetItem = new ();
    private readonly Subject<ItemTier> _onPickItem = new ();
    public Observable<Unit> OnStartGatya => _onStartGatya;
    public Observable<ItemType> OnGetItem => _onGetItem;
    public Observable<ItemTier> OnPickItem => _onPickItem;

    public GatyaHandler(GatyaElements elements, GatyaTable luxTable, ItemInfo itemInfo)
    {
        _elements = elements;
        _luxTable = luxTable;
        _itemInfo = itemInfo;
        
        _elements.ChangeButton.onClick.AddListener(OnChange);
        _elements.OneButton.onClick.AddListener(OnOne);
        _elements.TenButton.onClick.AddListener(OnTen);
    }

    private void OnOne()
    {
        _onStartGatya.OnNext(Unit.Default);
        _cts = _cts.Reset();

        var type = _luxTable.One();
        var info = _itemInfo.DisplayInfo[type];
        _tenjoCount++;
        if (_tenjoCount >= _maxTenjoCount)
        {
            (type, info) = GetTenjo();
            _tenjoCount = 0;
        }

        _onGetItem.OnNext(type);
        _elements.OneResult.SkipButton.gameObject.SetActive(false);
        OnPick(info, _cts.Token).Forget();
    }
    
    private void OnTen()
    {
        _onStartGatya.OnNext(Unit.Default);
        _cts = _cts.Reset();

        var infos = new ItemDisplayInfo[10];
        var onlyCommon = true;
        for (var i = 0; i < 10; i++)
        {
            
            var type = _luxTable.One();
            var info = _itemInfo.DisplayInfo[type];
            _tenjoCount++;
            if (_tenjoCount >= _maxTenjoCount)
            {
                (type, info) = GetTenjo();
                _tenjoCount = 0;
            }

            infos[i] = info;
            _onGetItem.OnNext(type);
            onlyCommon &= infos[i].Tier == ItemTier.Common;
        }

        if (onlyCommon)
        {
            infos[9] = GetUpperRareInfo();
        }
        ShowTenResult(infos, CancellationToken.None).Forget();
    }

    private void OnChange()
    {
        _elements.CharacterSelector.Popup.Show();
    }
    
    private async UniTask ShowTenResult(ItemDisplayInfo[] infos, CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);

        var skipButton = _elements.OneResult.SkipButton;
        skipButton.gameObject.SetActive(true);
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(() =>
        {
            _cts.Cancel();
        });
        
        try
        {
            foreach (var info in infos)
            {
                await OnPick(info, token);
            }
        }
        finally
        {
            skipButton.onClick.RemoveAllListeners();

            _cts = _cts.Reset();
            linkedToken = _cts.LinkedToken(token);
            _elements.TenResult.ShowResult(infos, linkedToken).Forget();
        }
    }
    
    private async UniTask OnPick(ItemDisplayInfo info, CancellationToken token)
    {
        _onPickItem.OnNext(info.Tier);
        await _elements.OneResult.ShowResult(info, token);
    }

    private ItemDisplayInfo GetUpperRareInfo()
    {
        var result = _itemInfo.DisplayInfo[_luxTable.One()];
        while (result.Tier == ItemTier.Common)
        {
            result = _itemInfo.DisplayInfo[_luxTable.One()];
        }

        return result;
    }
    
    private (ItemType, ItemDisplayInfo) GetTenjo()
    {
        var type = ItemType.None;
        var result = _itemInfo.DisplayInfo[_luxTable.One()];
        // TODO: 流石に頭悪い説
        while (result.Tier != ItemTier.Epic)
        {
            type = _luxTable.One();
            result = _itemInfo.DisplayInfo[type];
        }

        return (type, result);
    }

    public void Dispose()
    {
        _cts.Reset();
        _onGetItem.Dispose();
    }
}
