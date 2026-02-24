
using System;
using UnityEngine;

[Serializable]
public class PresentInfo<T>
{
    [SerializeField] private T[] talkType;
    [SerializeField] private float loveAmount;

    public PresentInfo(T[] talkType, float loveAmount)
    {
        this.talkType = talkType;
        this.loveAmount = loveAmount;
    }

    public T[] TalkType => talkType;
    public float LoveAmount => loveAmount;
}