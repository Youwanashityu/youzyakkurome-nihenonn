using UnityEngine;

[CreateAssetMenu(
    fileName = "NewGatyaTable",
    menuName = "Data/GatyaTable"
)]
public class GatyaTableScriptable : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<ItemType, int> rateTable;
    
    [SerializeField][Header("以下見る用")]
    private float totalRatio;
    private SerializedDictionary<ItemType, float> gatyaPer;

    private void OnValidate()
    {
        totalRatio = 0;
        gatyaPer.Clear();
        foreach (var rate in rateTable)
        {
            totalRatio += rate.Value;
            gatyaPer.Add(rate.Key, rate.Value / totalRatio * 100);
        }
    }

    public GatyaTable GetPureTable()
    {
        return new GatyaTable(rateTable);
    }
}