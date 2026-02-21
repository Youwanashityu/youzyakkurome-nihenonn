
using System;
using UnityEngine;

[Serializable]
public class PurchaseInfo
{
    [SerializeField] private int badMoneyAmount;
    [SerializeField] private int keyAmount;

    public int BadMoneyAmount => badMoneyAmount;
    public int KeyAmount => keyAmount;
}