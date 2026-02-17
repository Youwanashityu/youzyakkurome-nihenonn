using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TenResultController : MonoBehaviour
{
    [SerializeField]
    private TenResultRowController[] rows;
    [SerializeField]
    private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    private CancellationTokenSource _cts;

    public async UniTask Show(ItemDisplayInfo[] infos, CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);
        foreach (var t in rows)
        {
            t.Reset();
        }
        gameObject.SetActive(true);
        for (int i = 0; i < infos.Length && i < rows.Length; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: linkedToken);
            rows[i].Initialize(infos[i]);
        }
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
