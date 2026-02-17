using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnceResultController: MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text displanName;
    
    private void Initialize(ItemDisplayInfo displayInfo)
    {
        icon.sprite = displayInfo.Icon;
        displanName.text = displayInfo.DisplayName;
    }
}
