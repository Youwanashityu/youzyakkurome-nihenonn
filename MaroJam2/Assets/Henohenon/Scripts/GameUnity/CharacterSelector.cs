using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector: MonoBehaviour
{
    [SerializeField]
    private Button graim;
    [SerializeField]
    private Button lux;
    [SerializeField]
    private PopupElement popup;
    
    public Button Graim => graim;
    public Button Lux => lux;
    public PopupElement Popup => popup;
}
