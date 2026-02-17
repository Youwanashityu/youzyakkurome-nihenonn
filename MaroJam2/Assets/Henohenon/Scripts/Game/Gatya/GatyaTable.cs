
using System.Collections.Generic;
using System.Linq;

public class GatyaTable
{
    private readonly IReadOnlyDictionary<ItemType, int> rateTable;
    private readonly int totalRatio;
    
    public GatyaTable(Dictionary<ItemType, int> rateTable)
    {
        this.rateTable = rateTable;
        totalRatio = rateTable.Values.Sum(r => r);
    }

    public ItemType One()
    {
        var random = UnityEngine.Random.Range(0, totalRatio);
        foreach (var rate in rateTable)
        {
            if (random < rate.Value)
            {
                return rate.Key;
            }
            random -= rate.Value;
        }
        return rateTable.Keys.First();
    }
}