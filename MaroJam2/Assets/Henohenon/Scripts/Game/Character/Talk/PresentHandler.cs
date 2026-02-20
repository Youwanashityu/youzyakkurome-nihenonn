using System;
using System.Collections.Generic;
using UnityEngine;

public class PresentHandler<T>: IDisposable where T : Enum
{
    private readonly TalkHandler<T> _talkHandler;
    private readonly IReadOnlyDictionary<ItemType, PresentInfo<T>> _presentsInfo;

    public PresentHandler(TalkHandler<T> talkHandler, IReadOnlyDictionary<ItemType, PresentInfo<T>> presentsInfo)
    {
        _talkHandler = talkHandler;
        _presentsInfo = presentsInfo;
    }

    public void Present(ItemType type)
    {
        if (!_presentsInfo.TryGetValue(type, out var info))
        {
            return;
        }

        
        // _characterHandler.AddLove(info.LoveAmount);
        
    }

    public void Dispose()
    {
    }
}
