using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IShowHide
{
    public UniTask Show(CancellationToken token);
    public UniTask Hide(CancellationToken token);
}