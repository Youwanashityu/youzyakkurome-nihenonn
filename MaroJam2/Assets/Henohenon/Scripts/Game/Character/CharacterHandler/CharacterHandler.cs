using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class CharacterHandler: ICharacterHandler
{
    public readonly CharacterType CharacterType;
    private readonly TalkHandler _talkHandler; 
    private readonly CharacterData _data;
    public ICharacterData Data => _data;
    private readonly ReactiveProperty<float> _love = new (0);
    public ReadOnlyReactiveProperty<float> Love => _love;
    
    public CharacterHandler(CharacterType type, TalkHandler talkHandler, CharacterData data)
    {
        CharacterType = type;
        _talkHandler = talkHandler;
        _data = data;
    }

    // TODO: cts管理
    public async UniTask Talk(int type, CancellationToken token)
    {
        await _talkHandler.ExecTalk(type, token);
    }
    
    public async UniTask RandomTalk(CancellationToken token)
    {
        var list = _data.RandomTalks[0]; // TODO: Lovelevel反映
        var type = list[UnityEngine.Random.Range(0, list.Length)];
        
        await _talkHandler.ExecTalk(type, token);
    }

    public async UniTask Tutorial(CancellationToken token)
    {
        await _talkHandler.Tutorial(token); // 諸説
    }

    public async UniTask Present(ItemType type, int numb, CancellationToken token)
    {
        var info = _data.PresentsInfo[type];
        var list = info.TalkType;
        var talkType = list[UnityEngine.Random.Range(0, list.Length)];

        try
        {
            await _talkHandler.ExecTalk(talkType, token);
        }
        finally
        {
            _love.Value = _love.CurrentValue + info.LoveAmount * numb;
        }
    }

    public float GetLoveRatio()
    {
        var last = _data.LoveLvPoints[GetLoveLv() - 1];
        var current = _data.LoveLvPoints[GetLoveLv()];
        return (_love.CurrentValue - last) / (current - last);
    }

    public int GetLoveLv()
    {
        var result = 0;
        var v = _love.CurrentValue;
        foreach (var point in _data.LoveLvPoints)
        {
            if (v < point)
            {
                return result;
            }
            result++;
        }
        return 10;
    }

    public void Dispose()
    {
        _talkHandler.Dispose();
    }
}
