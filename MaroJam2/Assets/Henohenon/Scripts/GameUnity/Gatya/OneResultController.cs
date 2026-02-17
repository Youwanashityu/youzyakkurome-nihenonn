using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneResultController: MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text displayName;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    public void Show(ItemDisplayInfo displayInfo)
    {
        gameObject.SetActive(true);
        icon.sprite = displayInfo.Icon;
        displayName.text = displayInfo.DisplayName;
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(Hide);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
