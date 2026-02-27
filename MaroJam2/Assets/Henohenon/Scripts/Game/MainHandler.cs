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
    private readonly CharactersManager _charactersManager;
    private readonly HomeCharacterHandler _homeCharacterHandler;
    private readonly GatyaHandler _gatyaHandler;
    private readonly GeneralHandler _generalHandler;
    private readonly CharacterSelectorHandler _charaSelectorHandler;
    private readonly InventoryKeyHandler _inventoryKeyHandler;
    private readonly SoundHandler _soundHandler;
    private readonly Button _startButton;

    public MainHandler(TitleElements titleElements, HomeElements homeElements, GatyaElements gatyaElements, GeneralElements generalElements, ItemInfo itemInfo, GatyaData gatyaData, IReadOnlyDictionary<CharacterType, CharacterData> data)
    {
        _viewHandler = new ViewHandler(titleElements, homeElements, gatyaElements);
        _titleHandler = new TitleHandler(titleElements);
        _generalHandler = new GeneralHandler(generalElements);
        _inventoryKeyHandler = new InventoryKeyHandler(itemInfo, homeElements.Presents, new ());
        _homeHandler = new HomeHandler(homeElements, _inventoryKeyHandler);
        _charactersManager = new CharactersManager(homeElements.TalkController, generalElements.VoicePlayer, _homeHandler, data);
        _homeCharacterHandler = new HomeCharacterHandler(_homeHandler, _charactersManager);
        _gatyaHandler = new GatyaHandler(gatyaElements, gatyaData, itemInfo, _inventoryKeyHandler, _charactersManager.OnCharaChange);
        _charaSelectorHandler = new CharacterSelectorHandler(new []{homeElements.CharacterSelector, gatyaElements.CharacterSelector}, _charactersManager);
        _soundHandler = new SoundHandler(generalElements, titleElements, _charaSelectorHandler.Characters, _gatyaHandler);
        
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
		_gatyaHandler.Dispose();
		_generalHandler.Dispose();
		_inventoryKeyHandler.Dispose();
		_viewHandler.Dispose();
		_homeHandler.Dispose();
		_charactersManager.Dispose();
		_homeCharacterHandler.Dispose();
		_charaSelectorHandler.Dispose();
		_soundHandler.Dispose();
	}
}