using Henohenon.Scripts.CoreUnity;
using UnityEngine;
using UnityEngine.UI;

public class GatyaElements : MonoBehaviour
{
    [SerializeField]
    private GameObjectShowHide view;
    [SerializeField]
    private Button backButton;
    
    public IShowHide View => view;
    public Button BackButton => backButton;
}
