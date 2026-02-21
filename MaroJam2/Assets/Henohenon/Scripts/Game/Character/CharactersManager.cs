using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class CharactersManager: IDisposable
{
    public readonly IReadOnlyDictionary<CharacterType, ICharacterHandler> Characters;
    private readonly IReadOnlyDictionary<CharacterType, GatyaTable> _tables;
    private readonly HomeHandler _homeHandler;
    private readonly TalkController _talkController;
    private readonly IGatyaController _gatyaController;

    public CharactersManager(TalkController talkController, IVoicePlayer voicePlayer, HomeHandler homeHandler, IGatyaController gatyaController, IReadOnlyDictionary<CharacterType, GatyaTable> tables, CharacterData<LuxImageType, LuxVoiceType, LuxTalkType> luxData)
    {
        var luxTalkHandler = new LuxTalkHandler(
            talkController,
            voicePlayer,
            homeHandler.OnNext,
            luxData
        );
        var luxHandler =
            new CharacterHandler<LuxImageType, LuxVoiceType, LuxTalkType>(CharacterType.Lux, luxTalkHandler, luxData);

        Characters = new Dictionary<CharacterType, ICharacterHandler>
        {
            { CharacterType.Lux, luxHandler }
        };

        _tables = tables;
        _talkController = talkController;
        _homeHandler = homeHandler;
        _gatyaController = gatyaController;

        SetCharacter(CharacterType.Lux);
    }

    public void SetCharacter(CharacterType type)
    {
        var character = Characters[type];
        var data = character.Data;
        _homeHandler.Initialize(character, data.EventItemList);
        _talkController.Initialize(data.DefaultCharaImage, data.DefaultMiniImage);
        _gatyaController.SetTable(_tables[type]);
    }

    public void Dispose()
    {
        foreach (var c in Characters.Values)
        {
            c.Dispose();
        }
    }
}
