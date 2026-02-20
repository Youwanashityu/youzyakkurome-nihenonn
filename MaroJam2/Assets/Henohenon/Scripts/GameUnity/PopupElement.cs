using UnityEngine;
using UnityEngine.UI;

public class PopupElement: MonoBehaviour, IShowHide
{
    [SerializeField] private bool hideOnAwake = true;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button backgroundButton;
    
    private void Awake()
    {
        if (hideOnAwake) gameObject.SetActive(false);
        closeButton.onClick.AddListener(Hide);
        backgroundButton.onClick.AddListener(Hide);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(Hide);
        backgroundButton.onClick.RemoveListener(Hide);
    }
}