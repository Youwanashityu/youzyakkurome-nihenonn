using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PopupElement))]
public class CharacterSelector: MonoBehaviour
{
    [SerializeField]
    private Button graim;
    [SerializeField]
    private Button lux;

    private PopupElement _popup;

    private void Awake()
    {
        _popup = GetComponent<PopupElement>();
        Debug.Log(_popup);
    }

    public Button Graim => graim;
    public Button Lux => lux;
    public PopupElement Popup => _popup;
}
