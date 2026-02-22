using System.Linq;
using UnityEngine;


[CreateAssetMenu(
    fileName = "NewGraim",
    menuName = "Data/Graim"
)]
public class GraimScriptable : ScriptableObject
{
    [SerializeField]
    private float[] loveLvPoints;
    [SerializeField]
    private SerializedDictionary<GraimImageType, Sprite> images;
    [SerializeField]
    private SerializedDictionary<GraimVoiceType, AudioClip> voices;
    [SerializeField]
    private SerializedDictionary<GraimTalkType, SimpleTalkParams<GraimImageType, GraimVoiceType>> simpleParams;
    [SerializeField]
    private SerializedDictionary<int, GraimTalkType[]> randomTalks;
    [SerializeField]
    private SerializedDictionary<ItemType, PresentInfo<GraimTalkType>> presentsInfo;

    public CharacterData GetPureData => new CharacterData(
        loveLvPoints, images[GraimImageType.Default],
        images[GraimImageType.Mini_Default], 
        images.ToDictionary(x => (int)x.Key, x=> x.Value),
        voices.ToDictionary(x => (int)x.Key, x=> x.Value),
        simpleParams.ToDictionary(x => (int)x.Key, x => new SimpleTalkParams<int, int>(x.Value.Texts, (int)x.Value.Image, (int)x.Value.Voice)),
        randomTalks.ToDictionary(x => x.Key, x => x.Value.Select(t => (int)t).ToArray()),
        presentsInfo.ToDictionary(x => x.Key, x => new PresentInfo<int>(x.Value.TalkType.Select(t => (int)t).ToArray(), x.Value.LoveAmount))
    );
}