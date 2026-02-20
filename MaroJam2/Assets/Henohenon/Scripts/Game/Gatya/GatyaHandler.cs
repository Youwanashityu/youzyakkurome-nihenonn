using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class GatyaHandler : IDisposable
{
    private readonly GatyaElements _elements;
    private readonly GatyaTable _luxTable;
    private readonly ItemInfo _itemInfo;
    private readonly int _maxTenjoCount = 50;
    private int _tenjoCount = 10;
    private CancellationTokenSource _cts;
    
    public GatyaHandler(GatyaElements elements, GatyaTable luxTable, ItemInfo itemInfo)
    {
        _elements = elements;
        _luxTable = luxTable;
        _itemInfo = itemInfo;
        
        _elements.OneButton.onClick.AddListener(OnOne);
        _elements.TenButton.onClick.AddListener(OnTen);
    }

    private void OnOne()
    {
        _cts = _cts.Reset();

        var info = _itemInfo.DisplayInfo[_luxTable.One()];
        _tenjoCount++;
        if (_tenjoCount >= _maxTenjoCount)
        {
            info = GetTenjoInfo();
            _tenjoCount = 0;
        }

        _elements.OneResult.SkipButton.gameObject.SetActive(false);
        _elements.OneResult.ShowResult(info, _cts.Token).Forget();
    }
    
    private void OnTen()
    {
        var infos = new ItemDisplayInfo[10];
        var onlyCommon = true;
        for (var i = 0; i < 10; i++)
        {
            infos[i] = _itemInfo.DisplayInfo[_luxTable.One()];
            _tenjoCount++;
            if (_tenjoCount >= _maxTenjoCount)
            {
                infos[i] = GetTenjoInfo();
                _tenjoCount = 0;
            }
            
            onlyCommon &= infos[i].Tier == ItemTier.Common;
        }

        if (onlyCommon)
        {
            infos[9] = GetUpperRareInfo();
        }
        ShowTenResult(infos, CancellationToken.None).Forget();
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
                await _elements.OneResult.ShowResult(info, linkedToken);
            }
        }
        finally
        {
            skipButton.onClick.RemoveAllListeners();

            _cts = _cts.Reset();
            linkedToken = _cts.LinkedToken(token);
            _elements.TenResult. ShowResult(infos, linkedToken).Forget();
        }
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
    
    private ItemDisplayInfo GetTenjoInfo()
    {
        var result = _itemInfo.DisplayInfo[_luxTable.One()];
        // TODO: 流石に頭悪い説
        while (result.Tier != ItemTier.Epic)
        {
            result = _itemInfo.DisplayInfo[_luxTable.One()];
        }

        return result;
    }

    public void Dispose()
    {
    }
}
