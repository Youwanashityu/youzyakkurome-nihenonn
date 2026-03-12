using System.Linq;
using UnityEngine;


[CreateAssetMenu(
    fileName = "NewLux",
    menuName = "Data/Lux"
)]
public class LuxScriptable : ScriptableObject
{
    [SerializeField]
    private float[] loveLvPoints;
    [SerializeField]

    //会話が何回目か
    public bool LuxTutorialDone;
    public bool LuxChocolateDone;



    [SerializeField]
    private SerializedDictionary<LuxImageType, Sprite> images;
    [SerializeField]
    private SerializedDictionary<LuxVoiceType, AudioClip> voices;
    [SerializeField]
    private SerializedDictionary<LuxTalkType, SimpleTalkParams<LuxImageType, LuxVoiceType>> simpleParams;
    [SerializeField]
    private SerializedDictionary<int, LuxTalkType[]> randomTalks;
    [SerializeField]
    private SerializedDictionary<ItemType, PresentInfo<LuxTalkType>> presentsInfo;

    public CharacterData GetPureData => new CharacterData(
        loveLvPoints, images[LuxImageType.Default],
        images[LuxImageType.Mini_Default], 
        images.ToDictionary(x => (int)x.Key, x=> x.Value),
        voices.ToDictionary(x => (int)x.Key, x=> x.Value),
        simpleParams.ToDictionary(x => (int)x.Key, x => new SimpleTalkParams<int, int>(x.Value.Texts, (int)x.Value.Image, (int)x.Value.Voice)),
        randomTalks.ToDictionary(x => x.Key, x => x.Value.Select(t => (int)t).ToArray()),
        presentsInfo.ToDictionary(x => x.Key, x => new PresentInfo<int>(x.Value.TalkType.Select(t => (int)t).ToArray(), x.Value.LoveAmount))
    );
}