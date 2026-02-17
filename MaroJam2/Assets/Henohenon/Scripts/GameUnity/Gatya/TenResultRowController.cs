using UnityEngine;
using UnityEngine.UI;

public class TenResultRowController : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    
    public void Initialize(ItemDisplayInfo displayInfo)
    {
        icon.sprite = displayInfo.Icon;
    }
}
