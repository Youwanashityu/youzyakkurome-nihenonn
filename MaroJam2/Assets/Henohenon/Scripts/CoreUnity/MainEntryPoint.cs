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

    private TitleHandler _titleHandler;
    private HomeHandler _homeHandler;
    private GatyaHandler _gatyaHandler;

    private void Awake()
    {
        _titleHandler = new TitleHandler(titleElements);
        _homeHandler = new HomeHandler(homeElements);
        _gatyaHandler = new GatyaHandler(gatyaElements);
    }

    private void OnDestroy()
    {
        _titleHandler.Dispose();
        _homeHandler.Dispose();
        _gatyaHandler.Dispose();
    }
}
