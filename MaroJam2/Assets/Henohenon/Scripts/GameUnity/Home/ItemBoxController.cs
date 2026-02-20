using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemBoxController: MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text numberText;
    private Button _button;

    public Button.ButtonClickedEvent OnClicked => _button.onClick;

    public void Initialize(ItemDisplayInfo info, int number)
    {
        _button = GetComponent<Button>();
        image.sprite = info.Icon;
        SetNumber(number);
    }

    public void SetNumber(int number)
    {
        _button.interactable = number <= 0;
        numberText.text = number.ToString();
    }
}
