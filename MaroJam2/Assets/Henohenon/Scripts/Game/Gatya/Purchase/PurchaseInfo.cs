
using System;
using UnityEngine;

[Serializable]
public class PurchaseInfo
{
    [SerializeField] private int akkaAmount;
    [SerializeField] private int keyAmount;

    public int AkkaAmount => akkaAmount;
    public int KeyAmount => keyAmount;
}