using UnityEngine;
using UnityEngine.UI;

public class HomeElements : MonoBehaviour
{
    [SerializeField]
    private GameObjectShowHide view;
    [SerializeField]
    private Button switchCharaButton;
    [SerializeField]
    private Button presentListButton;
    [SerializeField]
    private Button gatyaButton;
    [SerializeField]
    private TalkController talkController;

    public TalkController TalkController => talkController;
    public IShowHide View => view;
    public Button GatyaButton => gatyaButton;
}
