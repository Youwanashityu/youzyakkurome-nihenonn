using System;
using System.Collections.Generic;
using R3;

public class CharactersManager: IDisposable
{
    private readonly Dictionary<CharacterType, ICharacterHandler> _characters;
    private readonly HomeHandler _homeHandler;
    private readonly TalkController _talkController;

    public CharactersManager(TalkController talkController, IVoicePlayer voicePlayer, HomeHandler homeHandler, CharacterData<LuxImageType, LuxVoiceType, LuxTalkType> luxData)
    {
        var luxTalkHandler = new LuxTalkHandler(
            talkController,
            voicePlayer,
            homeHandler.OnNext,
            luxData
        );
        var luxHandler =
            new CharacterHandler<LuxImageType, LuxVoiceType, LuxTalkType>(CharacterType.Lux, luxTalkHandler, luxData);

        _characters = new Dictionary<CharacterType, ICharacterHandler>
        {
            { CharacterType.Lux, luxHandler }
        };

        _talkController = talkController;
        _homeHandler = homeHandler;

        SetCharacter(CharacterType.Lux);
    }

    public void SetCharacter(CharacterType type)
    {
        _homeHandler.Initialize(_characters[type]);
        _talkController.Initialize(_characters[type].Data.DefaultImage);
    }

    public void Dispose()
    {
        foreach (var c in _characters.Values)
        {
            c.Dispose();
        }
    }
}
