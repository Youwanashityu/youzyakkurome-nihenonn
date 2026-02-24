using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseController: MonoBehaviour
{
    [SerializeField]
    private PopupElement popup;
    [SerializeField]
    private SerializedDictionary<PurchaseType, Button> buttons;
    [SerializeField]
    private TMP_Text akkaText;
    
    public PopupElement Popup => popup;
    public SerializedDictionary<PurchaseType, Button> Buttons => buttons;
    public TMP_Text AkkaText => akkaText;
}
