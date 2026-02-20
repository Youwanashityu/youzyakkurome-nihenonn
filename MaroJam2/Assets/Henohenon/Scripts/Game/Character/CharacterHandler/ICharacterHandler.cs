using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public interface ICharacterHandler: IDisposable
{
    public UniTask RandomTalk(CancellationToken token);
    public UniTask Tutorial(CancellationToken token);
    public UniTask Present(ItemType type, int numb, CancellationToken token);
    public ICharacterData Data { get; }
    public float Love { get; }
    public int LoveLv { get; }
}