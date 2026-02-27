using System.Collections.Generic;
using Henohenon.Scripts.GameUnity.General;
using R3;
using UnityEngine;

public class HomeTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private HomeElements homeElements;
    [SerializeField]
    private GeneralElements generalElements;
    [SerializeField]
    private GraimScriptable graim;
    [SerializeField]
    private LuxScriptable lux;
    [SerializeField]
    private ItemInfoScriptable itemInfo;
    [SerializeField]
    private SerializedDictionary<ItemType, int> initItems;

    private HomeHandler _handler;
    private CharactersManager _charactersManager;
    private CharacterSelectorHandler _selectorHandler;
    private HomeCharacterHandler _homeCharacterHandler;
    private Subject<ItemType> _onGetItem;

    private void Awake()
    {
        var inventoryHandler = new InventoryKeyHandler(itemInfo.GetPureData, homeElements.Presents, initItems);
        _handler = new HomeHandler(homeElements, inventoryHandler);
        _charactersManager = new CharactersManager(homeElements.TalkController, generalElements.VoicePlayer, _handler, new Dictionary<CharacterType, CharacterData>
        {
            {CharacterType.Lux, lux.GetPureData},
            {CharacterType.Graim, graim.GetPureData},
        });
        _selectorHandler = new CharacterSelectorHandler(new []{homeElements.CharacterSelector}, _charactersManager);
        _homeCharacterHandler = new HomeCharacterHandler(_handler, _charactersManager);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
        _onGetItem.Dispose();
        _selectorHandler.Dispose();
        _homeCharacterHandler.Dispose();
    }
}
