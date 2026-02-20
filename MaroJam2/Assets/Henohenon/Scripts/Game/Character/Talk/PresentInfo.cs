
using System;
using UnityEngine;

[Serializable]
public class PresentInfo<T> where T : Enum
{
    [SerializeField] private T[] talkType;
    [SerializeField] private float loveAmount;

    public T[] TalkType => talkType;
    public float LoveAmount => loveAmount;
}