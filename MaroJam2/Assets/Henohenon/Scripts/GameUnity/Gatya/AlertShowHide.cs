using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class AlertShowHide : MonoBehaviour, IShowHideWithTask
{
    [SerializeField] private float showDuration = 0.3f;
    [SerializeField] private float showingDuration = 1;
    [SerializeField] private float hideDuration = 0.3f;
    
    private CancellationTokenSource _cts;

    public async UniTask Show(CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);

        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        await transform.DOScale(Vector3.one, showDuration).SetEase(Ease.OutBack).ToUniTask(cancellationToken: linkedToken);
        await UniTask.Delay(TimeSpan.FromSeconds(showingDuration), cancellationToken: linkedToken);
        await Hide(token);
    }
    public async UniTask Hide(CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);

        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
        await transform.DOScale(Vector3.zero, hideDuration).SetEase(Ease.OutCubic).ToUniTask(cancellationToken: linkedToken);
        gameObject.SetActive(false);
    }
}
