using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemBoxController: MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text numberText;
    [SerializeField]
    private Button button;

    public Button.ButtonClickedEvent OnClicked => button.onClick;

    public void Initialize(ItemDisplayInfo info, int number, bool filtered)
    {
        image.sprite = info.Icon;
        SetNumber(number);
        SetFiltered(filtered);
    }

    public void SetNumber(int number)
    {
        button.interactable = number > 0;
        numberText.text = number.ToString();
    }

    public void SetFiltered(bool filtered)
    {
        gameObject.SetActive(filtered);
    }
}
