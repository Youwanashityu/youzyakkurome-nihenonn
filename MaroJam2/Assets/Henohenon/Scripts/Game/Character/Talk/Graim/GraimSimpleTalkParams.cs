
using System;
using UnityEngine;

[Serializable]
public class GraimSimpleTalkParams
{
    [SerializeField] private string[] texts;
    [SerializeField] private LuxImageType image;
    [SerializeField] private LuxVoiceType voice;
    
    public string[] Texts => texts;
    public LuxImageType Image => image;
    public LuxVoiceType Voice => voice;
}