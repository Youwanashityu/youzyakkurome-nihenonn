using System;
using UnityEngine;

public class TitleTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private TitleElements elements;

    private TitleHandler _handler;
    
    private void Awake()
    {
        _handler = new TitleHandler(elements);
    }
    
    private void OnDestroy()
    {
        _handler.Dispose();
    }
}
