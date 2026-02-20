using System.Linq;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewGatyaTable",
    menuName = "Data/GatyaTable"
)]
public class GatyaTableScriptable : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<ItemType, int> rateTable;
    
    [Header("以下見る用")]
    [SerializeField]
    private float totalRatio;
    [SerializeField]
    private SerializedDictionary<ItemType, float> gatyaPer;

    private void OnValidate()
    {
        totalRatio = rateTable.Values.Sum(r => r);
        gatyaPer.Clear();
        foreach (var rate in rateTable)
        {
            gatyaPer.Add(rate.Key, rate.Value / totalRatio * 100);
        }
    }

    public GatyaTable GetPureData => new GatyaTable(rateTable);
}