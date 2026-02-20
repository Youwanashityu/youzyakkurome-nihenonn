using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

// ノリで作ったけどいらんかも
public class GameObjectShowHide: MonoBehaviour, IShowHide
{
    [SerializeField] private bool hideOnAwake = true;
    
    private void Awake()
    {
        if (hideOnAwake) gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}