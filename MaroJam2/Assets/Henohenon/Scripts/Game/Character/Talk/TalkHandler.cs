using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public abstract class TalkHandler<T> : IDisposable
    where T : Enum
{
    private CancellationTokenSource _cts;

    public async UniTask ExecTalk(T type, CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);
        await Talk(type, linkedToken);
    }
    
    protected abstract UniTask Talk(T type, CancellationToken token);
    public abstract UniTask Tutorial(CancellationToken token);
    
    public void Dispose()
    {
        _cts = _cts.Clear();
    }
}