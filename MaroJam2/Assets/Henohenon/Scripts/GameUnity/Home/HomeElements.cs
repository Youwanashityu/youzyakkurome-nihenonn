using Henohenon.Scripts.CoreUnity;
using UnityEngine;
using UnityEngine.UI;

public class HomeElements : MonoBehaviour
{
    [SerializeField]
    private GameObjectShowHide view;
    [SerializeField]
    private Button gatyaButton;
    
    public IShowHide View => view;
    public Button GatyaButton => gatyaButton;
}
