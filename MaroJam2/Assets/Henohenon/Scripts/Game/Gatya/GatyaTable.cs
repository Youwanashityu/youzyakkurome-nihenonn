
using System.Collections.Generic;
using System.Linq;

public class GatyaTable
{
    private readonly IReadOnlyDictionary<ItemType, int> _rateTable;
    private readonly int _totalRatio;
    
    public GatyaTable(IReadOnlyDictionary<ItemType, int> rateTable)
    {
        this._rateTable = rateTable;
        _totalRatio = rateTable.Values.Sum(r => r);
    }

    public ItemType One()
    {
        // TODO: unityengineのランダムって確かそんな良くなかったのでガチでやるならイイカンジのライブラリ入れたが良い
        var random = UnityEngine.Random.Range(0, _totalRatio);
        foreach (var rate in _rateTable)
        {
            if (random < rate.Value)
            {
                return rate.Key;
            }
            random -= rate.Value;
        }
        return _rateTable.Keys.First();
    }
}