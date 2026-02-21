using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatyaElements : MonoBehaviour
{
    [SerializeField]
    private GameObjectShowHide view;
    [SerializeField]
    private TMP_Text keyText;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button tenButton;
    [SerializeField]
    private Button oneButton;
    [SerializeField]
    private Button changeButton;
    [SerializeField]
    private Button addKeysButton;
    [SerializeField]
    private PopupElement purchasePopup;
    [SerializeField]
    private SerializedDictionary<PurchaseType, Button> purchaseButtons;
    [SerializeField]
    private CharacterSelector characterSelector;
    [SerializeField] 
    private TenResultController tenResult;
    [SerializeField]
    private OneResultController oneResult;

    public IShowHide View => view;
    public TMP_Text KeyText => keyText;
    public Button BackButton => backButton;
    public Button OneButton => oneButton;
    public Button TenButton => tenButton;
    public Button ChangeButton => changeButton;
    public Button AddKeysButton => addKeysButton;
    public PopupElement PurchasePopup => purchasePopup;
    public SerializedDictionary<PurchaseType, Button> PurchaseButtons => purchaseButtons;
    public CharacterSelector CharacterSelector => characterSelector;
    
    public TenResultController TenResult => tenResult;
    public OneResultController OneResult => oneResult;
}
