using System;
using System.Collections.Generic;
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

    public PopupElement Popup => popup;
    
    public void Clear()
    {
        foreach (var instance in _instances.Values)
        {
            instance.OnClicked.RemoveAllListeners();
            Destroy(instance);
        }
        _instances.Clear();
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
            instance.Initialize(displayInfo, number);
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
        Clear();
        _onPresent.Dispose();
    }
}