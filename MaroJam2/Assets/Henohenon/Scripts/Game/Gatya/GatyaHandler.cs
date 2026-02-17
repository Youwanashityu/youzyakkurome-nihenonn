using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class GatyaHandler : IDisposable
{
    private readonly GatyaElements _elements;
    private readonly GatyaTable _luxTable;
    private readonly ItemInfo _itemInfo;

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
        _elements.OneResult.Show(info);
    }
    
    private void OnTen()
    {
        var infos = new ItemDisplayInfo[10];
        for (var i = 0; i < 10; i++)
        {
            infos[i] = _itemInfo.DisplayInfo[_luxTable.One()];
        }
        _elements.TenResult.Show(infos, CancellationToken.None).Forget();
    }

    public void Dispose()
    {
    }
}
