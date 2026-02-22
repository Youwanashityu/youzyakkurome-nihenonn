using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterData: ICharacterData
{
    public float[] LoveLvPoints { get; }
    public Sprite DefaultCharaImage { get; }
    public Sprite DefaultMiniImage { get; }
    public ItemType[] EventItemList { get; }
    public readonly IReadOnlyDictionary<int, Sprite> Images;
    public readonly IReadOnlyDictionary<int, AudioClip> Voices;
    public readonly IReadOnlyDictionary<int, SimpleTalkParams<int, int>> SimpleParams;
    public readonly IReadOnlyDictionary<int, int[]> RandomTalks;
    public readonly IReadOnlyDictionary<ItemType, PresentInfo<int>> PresentsInfo;


    public CharacterData(float[] loveLvPoints, Sprite defaultCharaImage, Sprite defaultMiniImage, IReadOnlyDictionary<int, Sprite> images, IReadOnlyDictionary<int, AudioClip> voices, IReadOnlyDictionary<int, SimpleTalkParams<int, int>> simpleParams, IReadOnlyDictionary<int, int[]> randomTalks, IReadOnlyDictionary<ItemType, PresentInfo<int>> presentsInfo)
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