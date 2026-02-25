
using System.Collections.Generic;
using System.Linq;

public class CharacterSelectorHandler
{
    private readonly CharacterSelector[] _selectors;
    private readonly CharactersManager _charactersManager;
    public ICharacterHandler[] Characters => _charactersManager.Characters.Values.ToArray();
    public CharacterSelectorHandler(CharacterSelector[] selectors, CharactersManager charactersManager)
    {
        _charactersManager = charactersManager;
        _selectors = selectors;

        foreach (var selector in selectors)
        {
            AddListenCharacterSelector(selector);
        }
    }

    public void Dispose()
    {
        _charactersManager.Dispose();

        foreach (var selector in _selectors)
        {
            RemoveListenCharacterSelector(selector);
        }
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