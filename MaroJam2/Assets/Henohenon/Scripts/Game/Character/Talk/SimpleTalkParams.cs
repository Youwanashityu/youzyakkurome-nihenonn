using System;
using UnityEngine;

[Serializable]
public class SimpleTalkParams<TImage, TVoice>
{
    [SerializeField] private string[] texts;
    [SerializeField] private TImage image;
    [SerializeField] private TVoice voice;

    public SimpleTalkParams(string[] texts, TImage image, TVoice voice)
    {
        this.texts = texts;
        this.image = image;
        this.voice = voice;
    }
    
    public string[] Texts => texts;
    public TImage Image => image;
    public TVoice Voice => voice;
}