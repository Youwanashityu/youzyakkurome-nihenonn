using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class MainHandler: IDisposable
{
    private readonly ViewHandler _viewHandler;
    private readonly TitleHandler _titleHandler;
    private readonly HomeHandler _homeHandler;
    private readonly GatyaHandler _gatyaHandler;
    private readonly GeneralHandler _generalHandler;
    private readonly Button _startButton;

    public MainHandler(TitleElements titleElements, HomeElements homeElements, GatyaElements gatyaElements, GeneralElements generalElements, ItemInfo itemInfo, GatyaTable luxTable, LuxTalkScriptable luxTalk)
    {
        _viewHandler = new ViewHandler(titleElements, homeElements, gatyaElements);
        _titleHandler = new TitleHandler(titleElements);
        _homeHandler = new HomeHandler(homeElements, luxTalk);
        _gatyaHandler = new GatyaHandler(gatyaElements, luxTable, itemInfo);
        _generalHandler = new GeneralHandler(generalElements);
        
        _startButton = titleElements.StartButton;
        _startButton.onClick.AddListener(RunTutorial);
    }

    private void RunTutorial()
    {
	    _homeHandler.RunTutorial(CancellationToken.None).Forget();
	    _startButton.onClick.RemoveListener(RunTutorial);
    }

	public void Dispose(){
		_titleHandler.Dispose();
		_homeHandler.Dispose();
		_gatyaHandler.Dispose();
		_generalHandler.Dispose();
		_viewHandler.Dispose();
	}
}