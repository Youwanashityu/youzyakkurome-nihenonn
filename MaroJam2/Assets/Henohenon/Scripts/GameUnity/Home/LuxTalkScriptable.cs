using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewLuxTalk",
    menuName = "Data/LuxTalk"
)]
public class LuxTalkScriptable : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<LuxTalkType, SimpleTalkParams> simpleParams;
    [SerializeField]
    private SerializedDictionary<LuxImageType, Sprite> images;
    [SerializeField]
    private SerializedDictionary<LuxVoiceType, AudioClip> voices;
    [SerializeField]
    private SerializedDictionary<int, LuxTalkType[]> randomTalks;
    
    public IReadOnlyDictionary<LuxTalkType, SimpleTalkParams> SimpleParams => simpleParams;
    public IReadOnlyDictionary<LuxImageType, Sprite> Images => images;
    public IReadOnlyDictionary<LuxVoiceType, AudioClip> Voices => voices;
    public IReadOnlyDictionary<int, LuxTalkType[]> RandomTalks => randomTalks;
}