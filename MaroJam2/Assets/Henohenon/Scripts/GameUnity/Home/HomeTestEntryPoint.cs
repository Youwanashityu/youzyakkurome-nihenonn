using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HomeTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private HomeElements elements;
    [SerializeField]
    private LuxTalkScriptable luxTalk;

    private HomeHandler _handler;

    private void Awake()
    {
        _handler = new HomeHandler(elements, luxTalk);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
    }
    
    [Header("以下テスト用")]
    [SerializeField]
    private LuxTalkType testTalkType;
    [SerializeField]
    private ItemType testPresentType;

    private LuxTalkType _lastTalkType;
    private ItemType _lastPresentType;

    // 謎にeditor拡張入れない縛りしてるけど多分入れたほうが良い
    private void OnValidate()
    {
        if (testTalkType != _lastTalkType)
        {
            _handler.RunTalk(testTalkType, CancellationToken.None).Forget();
            _lastTalkType = testTalkType;
        }
        if (testPresentType != _lastPresentType)
        {
            _handler.RunPresent(testPresentType, CancellationToken.None).Forget();
            _lastPresentType = testPresentType;
        }

    }
}
