using UnityEngine;
using UnityEngine.UI;

public class GatyaElements : MonoBehaviour
{
    [SerializeField]
    private GameObjectShowHide view;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button tenButton;
    [SerializeField]
    private Button oneButton;
    [SerializeField]
    private CharacterSelector characterSelector;
    [SerializeField] 
    private TenResultController tenResult;
    [SerializeField]
    private OneResultController oneResult;

    public IShowHide View => view;
    public Button BackButton => backButton;
    public Button OneButton => oneButton;
    public Button TenButton => tenButton;
    public CharacterSelector CharacterSelector => characterSelector;
    
    public TenResultController TenResult => tenResult;
    public OneResultController OneResult => oneResult;
}
