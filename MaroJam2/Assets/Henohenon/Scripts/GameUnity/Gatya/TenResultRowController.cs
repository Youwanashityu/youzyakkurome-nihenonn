using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

public class TenResultRowController : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private RectTransform animation;

    public void Reset()
    {
        animation.DOKill();
        animation.localScale = Vector3.zero;
        icon.sprite = null;
    }
    
    public void Initialize(ItemDisplayInfo displayInfo)
    {
        Reset();
        icon.sprite = displayInfo.Icon;
        animation.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
}
