using Henohenon.Scripts.CoreUnity;
using UnityEngine;
using UnityEngine.UI;

public class TitleElements : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button creditButton;
    [SerializeField]
    private Button licenseButton;
    [SerializeField]
    private PopupElement creditPopup;
    [SerializeField]
    private PopupElement licensePopup;
    
    public Button StartButton => startButton;
    public Button CreditButton => creditButton;
    public Button LicenseButton => licenseButton;
    public IShowHide CreditPopup => creditPopup;
    public IShowHide LicensePopup => licensePopup;
}
