using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class InventoryHandler: IDisposable
{
    private readonly ItemInfo _itemInfo;
    private readonly PresentsPopupController _presentsPopup;
    private readonly Dictionary<ItemType, int> _items;
    private readonly IDisposable _subscription;

    public InventoryHandler(ItemInfo itemInfo, Observable<ItemType> onGetItem, PresentsPopupController presentsPopup)
    {
        _itemInfo = itemInfo;
        _presentsPopup = presentsPopup;
        _items = new();

        _subscription = onGetItem.Subscribe(AddItem);
    }

    private void AddItem(ItemType type)
    {
        _items[type] = _items.GetValueOrDefault(type) + 1;
        _presentsPopup.Set(type, _itemInfo.DisplayInfo[type], _items[type]);
    }
    
    public int UseItem(ItemType type)
    {
        if (!_items.TryGetValue(type, out var result)) return 0;
        _items[type] = 0;
        _presentsPopup.Set(type, _itemInfo.DisplayInfo[type], _items[type]);

        return result;
    }

    public void Dispose()
    {
        _subscription.Dispose();
    }
}
