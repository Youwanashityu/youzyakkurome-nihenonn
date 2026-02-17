using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class GatyaHandler : IDisposable
{
    private readonly GatyaElements _elements;
    private readonly GatyaTable _luxTable;
    private readonly ItemInfo _itemInfo;
    private readonly int MaxTenjoCount = 30;
    private int tenjoCount = 10;

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
        var info = _itemInfo.DisplayInfo[_luxTable.One()];
        tenjoCount++;
        if (tenjoCount >= MaxTenjoCount)
        {
            // TODO: 流石に頭悪い説
            while (info.Tier != ItemTier.Epic)
            {
                info = _itemInfo.DisplayInfo[_luxTable.One()];
            }
            tenjoCount = 0;
        }
        _elements.OneResult.Show(info);
    }
    
    private void OnTen()
    {
        var infos = new ItemDisplayInfo[10];
        var onlyCommon = true;
        for (var i = 0; i < 10; i++)
        {
            infos[i] = _itemInfo.DisplayInfo[_luxTable.One()];
            tenjoCount++;
    
            if (tenjoCount >= MaxTenjoCount)
            {
                // TODO: 流石に頭悪い説
                while (infos[i].Tier != ItemTier.Epic)
                {
                    infos[i] = _itemInfo.DisplayInfo[_luxTable.One()];
                }

                tenjoCount = 0;
            }
            
            onlyCommon &= infos[i].Tier == ItemTier.Common;
        }

        if (onlyCommon)
        {
            while (infos[10].Tier != ItemTier.Common)
            {
                infos[10] = _itemInfo.DisplayInfo[_luxTable.One()];
            }
        }
        
        _elements.TenResult.Show(infos, CancellationToken.None).Forget();
    }

    public void Dispose()
    {
    }
}
