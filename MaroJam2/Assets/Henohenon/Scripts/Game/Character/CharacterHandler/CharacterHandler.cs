using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class CharacterHandler<TImage, TVoice, TTalk>: ICharacterHandler
    where TImage : Enum
    where TVoice : Enum
    where TTalk : Enum
{
    public readonly CharacterType CharacterType;
    private readonly TalkHandler<TTalk> _talkHandler;
    private readonly PresentHandler<TTalk> _presentHandler;
    private readonly CharacterData<TImage, TVoice, TTalk> _data;
    public ICharacterData Data => _data;
    private float _love;
    
    public CharacterHandler(CharacterType type, TalkHandler<TTalk> talkHandler, CharacterData<TImage, TVoice, TTalk> data)
    {
        CharacterType = type;
        _talkHandler = talkHandler;
        _presentHandler = new PresentHandler<TTalk>(_talkHandler, data.PresentsInfo);
        _data = data;
    }

    public async UniTask Talk(TTalk type, CancellationToken token)
    {
        await _talkHandler.ExecTalk(type, token);
    }
    
    public async UniTask RandomTalk(CancellationToken token)
    {
        var list = _data.RandomTalks[LoveLv];
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

        _love += info.LoveAmount * numb;
    }
    
    public float Love => _love;
    public int LoveLv
    {
        get
        {
            if (_love < 10) return 0;
            if (_love < 20) return 1;
            return 2;
        }
    }
    
    private void AddLove(float value)
    {
        _love += value;
    }

    public void Dispose()
    {
        _talkHandler.Dispose();
        _presentHandler.Dispose();
    }
}
