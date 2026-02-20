using UnityEngine;
using UnityEngine.UI;

public class TitleElements : MonoBehaviour
{
    // serializable interface的なの使うともっとおしゃれにはできるけどまぁ一旦
    [SerializeField]
    private GameObjectShowHide view;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button licenseButton;
    [SerializeField]
    private PopupElement licensePopup;

    public GameObjectShowHide View => view;
    public Button StartButton => startButton;
    public Button LicenseButton => licenseButton;
    public IShowHide LicensePopup => licensePopup;
}
