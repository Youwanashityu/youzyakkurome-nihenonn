using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IShowHideWithTask
{
    public UniTask Show(CancellationToken token);
    public UniTask Hide(CancellationToken token);
}