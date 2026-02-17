using System.Threading;
using Cysharp.Threading.Tasks;
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

    private TitleHandler _titleHandler;
    private HomeHandler _homeHandler;
    private GatyaHandler _gatyaHandler;
    private GeneralHandler _generalHandler;
    private MainHandler _mainHandler;

    private void Awake()
    {
        _titleHandler = new TitleHandler(titleElements);
        _homeHandler = new HomeHandler(homeElements);
        _gatyaHandler = new GatyaHandler(gatyaElements, luxTable.GetPureData, itemInfo.GetPureData);
        _generalHandler = new GeneralHandler(generalElements);
        _mainHandler = new MainHandler(titleElements, homeElements, gatyaElements);
    }

    private void OnDestroy()
    {
        _titleHandler.Dispose();
        _homeHandler.Dispose();
        _gatyaHandler.Dispose();
        _generalHandler.Dispose();
        _mainHandler.Dispose();
    }
}
