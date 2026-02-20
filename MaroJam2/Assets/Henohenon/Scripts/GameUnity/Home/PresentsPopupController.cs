using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopupElement))]
public class PresentsPopupController: MonoBehaviour
{
    [SerializeField] private ItemBoxController itemBoxPrefab;
    [SerializeField] private RectTransform list;
    
    private readonly Dictionary<ItemType, ItemBoxController> _instances = new Dictionary<ItemType, ItemBoxController>();
    private PopupElement _popup;

    public void Initialize()
    {
        _popup = GetComponent<PopupElement>();
    }

    public void Clear()
    {
        foreach (var instance in _instances.Values)
        {
            Destroy(instance);
        }
        _instances.Clear();
    }

    public void Set(ItemType type, ItemDisplayInfo displayInfo, int number)
    {
        if (_instances.ContainsKey(type))
        {
            _instances[type].SetNumber(number);
        }
        else
        {
            var instance = Instantiate(itemBoxPrefab, list);
            instance.Initialize(displayInfo, number);
            
            _instances.Add(type, instance);
        }
    }

}