using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData<TImage, TVoice, TTalk>: ICharacterData
    where TImage : Enum
    where TVoice : Enum
    where TTalk : Enum
{
    public Sprite DefaultImage { get; }
    public readonly IReadOnlyDictionary<TImage, Sprite> Images;
    public readonly IReadOnlyDictionary<TVoice, AudioClip> Voices;
    public readonly IReadOnlyDictionary<TTalk, SimpleTalkParams> SimpleParams;
    public readonly IReadOnlyDictionary<int, TTalk[]> RandomTalks;
    public readonly IReadOnlyDictionary<ItemType, PresentInfo<TTalk>> PresentsInfo;


    public CharacterData(Sprite defaultImage, IReadOnlyDictionary<TImage, Sprite> images, IReadOnlyDictionary<TVoice, AudioClip> voices, IReadOnlyDictionary<TTalk, SimpleTalkParams> simpleParams, IReadOnlyDictionary<int, TTalk[]> randomTalks, IReadOnlyDictionary<ItemType, PresentInfo<TTalk>> presentsInfo)
    {
        Images = images;
        Voices = voices;
        SimpleParams = simpleParams;
        RandomTalks = randomTalks;
        PresentsInfo = presentsInfo;
        DefaultImage = defaultImage;
    }
}