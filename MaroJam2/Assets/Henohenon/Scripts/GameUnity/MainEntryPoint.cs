using System.Threading;
using Cysharp.Threading.Tasks;
using Henohenon.Scripts.GameUnity.General;
using UnityEngine;

public class MainEntryPoint : MonoBehaviour
{
    [SerializeField]
    private TitleElements titleElements;
    [SerializeField]
    private HomeElements homeElements;
    [SerializeField]
    private GatyaElements gatyaElements;
    [SerializeField]
    private GeneralElements generalElements;
    [SerializeField]
    private GatyaTableScriptable luxTable;
    [SerializeField]
    private ItemInfoScriptable itemInfo;
    [SerializeField]
    private LuxTalkScriptable luxTalk;

    private MainHandler _mainHandler;

    private void Awake()
    {
        _mainHandler = new MainHandler(titleElements, homeElements, gatyaElements, generalElements, itemInfo.GetPureData, luxTable.GetPureData, luxTalk);
    }

    private void OnDestroy()
    {
        _mainHandler.Dispose();
    }
}
