using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Henohenon.Scripts.GameUnity.General;
using UnityEngine.UI;

public class MainHandler: IDisposable
{
    private readonly ViewHandler _viewHandler;
    private readonly TitleHandler _titleHandler;
    private readonly HomeHandler _homeHandler;
    private readonly GatyaHandler _gatyaHandler;
    private readonly GeneralHandler _generalHandler;
    private readonly CharacterSelectorHandler _charaSelectorHandler;
    private readonly InventoryKeyHandler _inventoryKeyHandler;
    private readonly Button _startButton;

    public MainHandler(TitleElements titleElements, HomeElements homeElements, GatyaElements gatyaElements, GeneralElements generalElements, ItemInfo itemInfo, GatyaData gatyaData,  CharacterData<LuxImageType, LuxVoiceType, LuxTalkType> luxData)
    {
        _viewHandler = new ViewHandler(titleElements, homeElements, gatyaElements);
        _titleHandler = new TitleHandler(titleElements);
        _gatyaHandler = new GatyaHandler(gatyaElements, gatyaData, itemInfo);
        _generalHandler = new GeneralHandler(generalElements);
        _inventoryKeyHandler = new InventoryKeyHandler(itemInfo, _gatyaHandler.GatyaController.OnGetItem, homeElements.Presents);
        _homeHandler = new HomeHandler(homeElements, _inventoryKeyHandler);
        _charaSelectorHandler = new CharacterSelectorHandler(homeElements, gatyaElements, generalElements.VoicePlayer, _homeHandler, gatyaData.Tables, luxData);
        
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
		_charaSelectorHandler.Dispose();
	}
}