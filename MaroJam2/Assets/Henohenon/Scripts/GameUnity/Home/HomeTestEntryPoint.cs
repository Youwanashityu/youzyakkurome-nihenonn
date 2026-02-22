using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Henohenon.Scripts.GameUnity.General;
using R3;
using UnityEngine;

public class HomeTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private HomeElements homeElements;
    [SerializeField]
    private GeneralElements generalElements;
    [SerializeField]
    private ItemInfoScriptable itemInfo;
    [SerializeField]
    private Dictionary<ItemType, int> initItems;
    
    private HomeHandler _handler;
    private Subject<ItemType> _onGetItem;

    private void Awake()
    {
        var inventoryHandler = new InventoryKeyHandler(itemInfo.GetPureData, homeElements.Presents, initItems);
        _handler = new HomeHandler(homeElements, inventoryHandler);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
        _onGetItem.Dispose();
    }

    /*
    [Header("以下テスト用")]
    [SerializeField]
    private LuxTalkType testTalkType;
    [SerializeField]
    private ItemType testPresentType;

    private LuxTalkType _lastTalkType;
    private ItemType _lastPresentType;

    // 謎にeditor拡張入れない縛りしてるけど多分入れたほうが良い
    private void OnValidate()
    {
        if (testTalkType != _lastTalkType)
        {
            Debug.LogWarning("実装できてないわw");
            //_handler.RunTalk(testTalkType, CancellationToken.None).Forget();
            _lastTalkType = testTalkType;
        }
        if (testPresentType != _lastPresentType)
        {
            //_handler.RunPresent(testPresentType, CancellationToken.None).Forget();
            _lastPresentType = testPresentType;
        }

    }*/
}
