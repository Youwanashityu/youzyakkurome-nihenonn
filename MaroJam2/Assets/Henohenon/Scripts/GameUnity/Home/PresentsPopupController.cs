using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;

public class PresentsPopupController: MonoBehaviour
{
    [SerializeField] private ItemBoxController itemBoxPrefab;
    [SerializeField] private RectTransform list;
    [SerializeField] private PopupElement popup;

    private readonly Subject<ItemType> _onPresent = new();
    public Observable<ItemType> OnPresent => _onPresent;
    private readonly Dictionary<ItemType, ItemBoxController> _instances = new Dictionary<ItemType, ItemBoxController>();
    private ItemType[] _itemFilter;
    public PopupElement Popup => popup;

    public void SetFilter(ItemType[] itemFilter)
    {
        _itemFilter = itemFilter;
        ApplyFilter();
    }

    public void Set(ItemType type, ItemDisplayInfo displayInfo, int number)
    {
        if (_instances.TryGetValue(type, out var exists))
        {
            exists.SetNumber(number);
        }
        else
        {
            var instance = Instantiate(itemBoxPrefab, list);
            instance.Initialize(displayInfo, number, _itemFilter.Contains(type));
            instance.OnClicked.AddListener(() => OnBoxClicked(type));
            
            _instances.Add(type, instance);
        }
    }

    private void OnBoxClicked(ItemType type)
    {
        _onPresent.OnNext(type);   
    }

    private void OnDestroy()
    {
        _onPresent.Dispose();
    }
    
    private void ApplyFilter()
    {
        foreach (var i in _instances)
        {
            i.Value.SetFiltered(_itemFilter.Contains(i.Key));
        }
    }
}