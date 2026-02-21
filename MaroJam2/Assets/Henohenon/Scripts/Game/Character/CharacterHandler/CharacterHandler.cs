using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

public class CharacterHandler<TImage, TVoice, TTalk>: ICharacterHandler
    where TImage : Enum
    where TVoice : Enum
    where TTalk : Enum
{
    public readonly CharacterType CharacterType;
    private readonly TalkHandler<TTalk> _talkHandler; 
    private readonly CharacterData<TImage, TVoice, TTalk> _data;
    public ICharacterData Data => _data;
    private readonly ReactiveProperty<float> _love = new (0);
    public ReadOnlyReactiveProperty<float> Love => _love;
    
    public CharacterHandler(CharacterType type, TalkHandler<TTalk> talkHandler, CharacterData<TImage, TVoice, TTalk> data)
    {
        CharacterType = type;
        _talkHandler = talkHandler;
        _data = data;
    }

    // TODO: cts管理
    public async UniTask Talk(TTalk type, CancellationToken token)
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
        
        await _talkHandler.ExecTalk(talkType, token);

        _love.Value += info.LoveAmount * numb;
    }

    public float GetLoveRatio()
    {
        return _love.CurrentValue / _data.LoveLvPoints[GetLoveLv()];
    }

    public int GetLoveLv()
    {
        var result = 0;
        foreach (var point in _data.LoveLvPoints)
        {
            if (_love.CurrentValue >= point)
            {
                return result;
            }
            result++;
        }
        return result;
    }

    public void Dispose()
    {
        _talkHandler.Dispose();
    }
}
