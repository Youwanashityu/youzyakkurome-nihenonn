
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

public class GatyaController: IGatyaController, IDisposable
{
    private readonly GatyaElements _elements;
    private readonly IReadOnlyDictionary<ItemType, ItemDisplayInfo> _displayInfo;
    private readonly int _maxTenjoCount;
    private CancellationTokenSource _cts;
    private int _tenjoCount = 0;
    private GatyaTable _table;
    private readonly Subject<Unit> _onStartGatya = new ();
    private readonly Subject<ItemType> _onGetItem = new ();
    private readonly Subject<ItemTier> _onPickItem = new ();
    public Observable<Unit> OnStartGatya => _onStartGatya;
    public Observable<ItemType> OnGetItem => _onGetItem;
    public Observable<ItemTier> OnPickItem => _onPickItem;

    public GatyaController(GatyaElements elements, IReadOnlyDictionary<ItemType, ItemDisplayInfo> displayInfo, int maxTenjoCount)
    {
        _elements = elements;
        _displayInfo = displayInfo;
        _maxTenjoCount = maxTenjoCount;
        
        _elements.OneResult.SkipButton.onClick.AddListener(() =>
        {
            _cts = _cts.Clear();
        });
        _elements.OneResult.SkipButton.gameObject.SetActive(false);
    }

    public void SetTable(GatyaTable table)
    {
        _table = table;
    }

    public void OnOne()
    {
        _onStartGatya.OnNext(Unit.Default);
        _cts = _cts.Reset();

        var type = _table.One();
        var info = _displayInfo[type];
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
    
    public void OnTen()
    {
        _onStartGatya.OnNext(Unit.Default);

        var infos = new ItemDisplayInfo[10];
        var onlyCommon = true;
        for (var i = 0; i < 10; i++)
        {
            var type = _table.One();
            var info = _displayInfo[type];
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

    public void Dispose()
    {
        _cts.Clear();
        _onStartGatya.Dispose();
        _onPickItem.Dispose();
        _onGetItem.Dispose();
    }

    private async UniTask ShowTenResult(ItemDisplayInfo[] infos, CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);

        var skipButton = _elements.OneResult.SkipButton;
        skipButton.gameObject.SetActive(true);
        
        try
        {
            foreach (var info in infos)
            {
                await OnPick(info, linkedToken);
            }
        }
        finally
        {
            skipButton.gameObject.SetActive(true);
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
        var result = _displayInfo[_table.One()];
        while (result.Tier == ItemTier.Common)
        {
            result = _displayInfo[_table.One()];
        }

        return result;
    }
    
    private (ItemType, ItemDisplayInfo) GetTenjo()
    {
        var type = ItemType.None;
        var result = _displayInfo[_table.One()];
        // TODO: 流石に頭悪い説
        while (result.Tier != ItemTier.Epic)
        {
            type = _table.One();
            result = _displayInfo[type];
        }

        return (type, result);
    }
}