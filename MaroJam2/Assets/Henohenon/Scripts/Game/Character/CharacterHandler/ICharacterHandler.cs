using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

public interface ICharacterHandler: IDisposable
{
    public UniTask RandomTalk(CancellationToken token);
    public UniTask Tutorial(CancellationToken token);
    public UniTask Present(ItemType type, int numb, CancellationToken token);
    public ICharacterData Data { get; }
    public ReadOnlyReactiveProperty<float> Love { get; }
    public float GetLoveRatio();
    public int GetLoveLv();
}