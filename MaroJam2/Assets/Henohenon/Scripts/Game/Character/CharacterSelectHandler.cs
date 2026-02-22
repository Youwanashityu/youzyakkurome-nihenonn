
using System.Collections.Generic;

public class CharacterSelectorHandler
{
    private readonly CharacterSelector _homeSelector;
    private readonly CharacterSelector _gatyaSelector;
    private readonly CharactersManager _charactersManager;
    public CharacterSelectorHandler(HomeElements homeElements, GatyaElements gatyaElements, IVoicePlayer voicePlayer, HomeHandler homeHandler, IGatyaController gatyaController, IReadOnlyDictionary<CharacterType, GatyaTable> tables, IReadOnlyDictionary<CharacterType, CharacterData> data)
    {
        _charactersManager = new CharactersManager(homeElements.TalkController, voicePlayer, homeHandler, gatyaController, tables, data);
        _homeSelector = homeElements.CharacterSelector;
        _gatyaSelector = gatyaElements.CharacterSelector;
        AddListenCharacterSelector(_homeSelector);
        AddListenCharacterSelector(_gatyaSelector);
    }

    public void Dispose()
    {
        _charactersManager.Dispose();
        RemoveListenCharacterSelector(_homeSelector);
        RemoveListenCharacterSelector(_gatyaSelector);
    }

    private void AddListenCharacterSelector(CharacterSelector selector)
    {
        selector.Graim.onClick.AddListener(OnGraim);
        selector.Lux.onClick.AddListener(OnLux);
    }
    private void RemoveListenCharacterSelector(CharacterSelector selector)
    {
        selector.Graim.onClick.RemoveListener(OnGraim);
        selector.Lux.onClick.RemoveListener(OnLux);
    }

    private void OnGraim()
    {
        _charactersManager.SetCharacter(CharacterType.Graim);
    }
    
    private void OnLux()
    {
        _charactersManager.SetCharacter(CharacterType.Lux);
    }
}