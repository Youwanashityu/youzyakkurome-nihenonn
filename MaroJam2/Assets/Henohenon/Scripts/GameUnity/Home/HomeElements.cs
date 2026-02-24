using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeElements : MonoBehaviour
{
    [SerializeField]
    private GameObjectShowHide view;
    [SerializeField]
    private Button gatyaButton;
    [SerializeField]
    private Button switchCharaButton;
    [SerializeField]
    private Button presentListButton;
    [SerializeField]
    private Button backToTitleButton;
    [SerializeField]
    private TMP_Text loveLvText;
    [SerializeField]
    private Slider levelSlider;
    [SerializeField]
    private TalkController talkController;
    [SerializeField]
    private PresentsPopupController presentsController;
    [SerializeField]
    private CharacterSelector characterSelector;

    public IShowHide View => view;
    public Button GatyaButton => gatyaButton;
    public Button SwitchCharaButton => switchCharaButton;
    public Button PresentButton => presentListButton;
    public Button BackToTitle => backToTitleButton;
    public TMP_Text LoveLvText => loveLvText;
    public Slider LevelSlider => levelSlider;
    public TalkController TalkController => talkController;
    public PresentsPopupController Presents => presentsController;
    public CharacterSelector CharacterSelector => characterSelector;
}
