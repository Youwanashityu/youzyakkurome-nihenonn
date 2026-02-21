using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterData<TImage, TVoice, TTalk>: ICharacterData
    where TImage : Enum
    where TVoice : Enum
    where TTalk : Enum
{
    public float[] LoveLvPoints { get; }
    public Sprite DefaultCharaImage { get; }
    public Sprite DefaultMiniImage { get; }
    public ItemType[] EventItemList { get; }
    public readonly IReadOnlyDictionary<TImage, Sprite> Images;
    public readonly IReadOnlyDictionary<TVoice, AudioClip> Voices;
    public readonly IReadOnlyDictionary<TTalk, LuxSimpleTalkParams> SimpleParams;
    public readonly IReadOnlyDictionary<int, TTalk[]> RandomTalks;
    public readonly IReadOnlyDictionary<ItemType, PresentInfo<TTalk>> PresentsInfo;


    public CharacterData(float[] loveLvPoints, Sprite defaultCharaImage, Sprite defaultMiniImage, IReadOnlyDictionary<TImage, Sprite> images, IReadOnlyDictionary<TVoice, AudioClip> voices, IReadOnlyDictionary<TTalk, LuxSimpleTalkParams> simpleParams, IReadOnlyDictionary<int, TTalk[]> randomTalks, IReadOnlyDictionary<ItemType, PresentInfo<TTalk>> presentsInfo)
    {
        LoveLvPoints = loveLvPoints;
        DefaultCharaImage = defaultCharaImage;
        DefaultMiniImage = defaultMiniImage;
        EventItemList = presentsInfo.Keys.ToArray();
        Images = images;
        Voices = voices;
        SimpleParams = simpleParams;
        RandomTalks = randomTalks;
        PresentsInfo = presentsInfo;
    }
}