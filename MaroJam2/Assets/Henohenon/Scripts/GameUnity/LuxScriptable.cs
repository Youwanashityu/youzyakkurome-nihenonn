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
    private SerializedDictionary<LuxImageType, Sprite> images;
    [SerializeField]
    private SerializedDictionary<LuxVoiceType, AudioClip> voices;
    [SerializeField]
    private SerializedDictionary<LuxTalkType, LuxSimpleTalkParams> simpleParams;
    [SerializeField]
    private SerializedDictionary<int, LuxTalkType[]> randomTalks;
    [SerializeField]
    private SerializedDictionary<ItemType, PresentInfo<LuxTalkType>> presentsInfo;


    public CharacterData<LuxImageType, LuxVoiceType, LuxTalkType> GetPureData => new CharacterData<LuxImageType, LuxVoiceType, LuxTalkType>(loveLvPoints, images[LuxImageType.Default],images[LuxImageType.Mini_Default], images, voices, simpleParams, randomTalks, presentsInfo);
}