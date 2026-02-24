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
    private CharacterSelector characterSelector;
    [SerializeField] 
    private TenResultController tenResult;
    [SerializeField]
    private OneResultController oneResult;
    [SerializeField]
    private PurchaseController purchaseController;
    [SerializeField] 
    private AlertShowHide alertText;
    
    public IShowHide View => view;
    public TMP_Text KeyText => keyText;
    public Button BackButton => backButton;
    public Button OneButton => oneButton;
    public Button TenButton => tenButton;
    public Button ChangeButton => changeButton;
    public Button AddKeysButton => addKeysButton;
    public CharacterSelector CharacterSelector => characterSelector;
    public PurchaseController PurchaseController => purchaseController;
    public AlertShowHide AlertText => alertText;

    public TenResultController TenResult => tenResult;
    public OneResultController OneResult => oneResult;
}
