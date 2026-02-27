using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class CharactersManager: IDisposable
{
    public readonly IReadOnlyDictionary<CharacterType, ICharacterHandler> Characters;
    public Observable<CharacterType> OnCharaChange => _onCharaChange;
    private readonly Subject<CharacterType> _onCharaChange;

    public CharactersManager(TalkController talkController, IVoicePlayer voicePlayer, HomeHandler homeHandler, IReadOnlyDictionary<CharacterType, CharacterData> data)
    {
        var graimTalkHandler = new GraimTalkHandler(
            talkController,
            voicePlayer,
            homeHandler.OnNext,
            data[CharacterType.Graim]
        );
        var luxTalkHandler = new LuxTalkHandler(
            talkController,
            voicePlayer,
            homeHandler.OnNext,
            data[CharacterType.Lux]
        );
        var graimHandler =
            new CharacterHandler(CharacterType.Graim, graimTalkHandler, data[CharacterType.Graim]);
        var luxHandler =
            new CharacterHandler(CharacterType.Lux, luxTalkHandler, data[CharacterType.Lux]);

        Characters = new Dictionary<CharacterType, ICharacterHandler>
        {
            { CharacterType.Graim, graimHandler },
            { CharacterType.Lux, luxHandler }
        };

        _onCharaChange = new();

        SetCharacter(CharacterType.Lux);
    }

    public void SetCharacter(CharacterType type)
    {
        _onCharaChange.OnNext(type);
    }

    public void Dispose()
    {
        _onCharaChange.Dispose();
        foreach (var c in Characters.Values)
        {
            c.Dispose();
        }
    }
}
