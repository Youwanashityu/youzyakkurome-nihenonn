using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

public class TenResultRowController : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Image background;
    [SerializeField]
    private RectTransform animation;
    [SerializeField]
    private SerializedDictionary<ItemTier, Color> tierColors;

    public void Reset()
    {
        animation.DOKill();
        animation.localScale = Vector3.zero;
        icon.sprite = null;
        background.color = tierColors[ItemTier.Common];
    }
    
    public void Initialize(ItemDisplayInfo displayInfo)
    {
        Reset();
        icon.sprite = displayInfo.Icon;
        background.color = tierColors[displayInfo.Tier];
        animation.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
}
