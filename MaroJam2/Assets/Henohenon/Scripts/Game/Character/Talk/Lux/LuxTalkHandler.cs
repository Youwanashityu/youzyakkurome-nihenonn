using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class LuxTalkHandler : TalkHandler<LuxTalkType>, IDisposable
{
    private readonly TalkController _talkController;
    private readonly IVoicePlayer _voicePlayer;
    private readonly CharacterData<LuxImageType, LuxVoiceType, LuxTalkType> _data;
    private readonly Observable<Unit> _onNext;

    private CancellationTokenSource _cts;

    public LuxTalkHandler(TalkController talkController, IVoicePlayer voicePlayer, Observable<Unit> onNext,
        CharacterData<LuxImageType, LuxVoiceType, LuxTalkType> data)
    {
        _talkController = talkController;
        _voicePlayer = voicePlayer;
        _onNext = onNext;
        _data = data;
    }

    protected override async UniTask Talk(LuxTalkType type, CancellationToken token)
    {
        _talkController.TalkBox.gameObject.SetActive(true);
        switch (type)
        {
            case LuxTalkType.Tutorial:
                await Text("こんにちは！\n合えて嬉しいです！");
                await Text("初めまして！俺はルクス！\nここのことをあなたに教えてあげる！");
                var answer = await Question("いらない", "わかった");
                switch (answer)
                {
                    case SelectionType.Alpha:
                        await Text("え？もう知ってるんですか？\nわかりました！\n俺をひいてくれるのを待ってるね！");
                        break;
                    case SelectionType.Beta:
                        await Talk(LuxTalkType.Tutorial_Stay, token);
                        break;
                }

                break;
            case LuxTalkType.Tutorial_Stay:
                await Text("えへへ、お話聞いてくれるんですね\n何から説明しようかな～");
                await Text("そうだ！まずこの体は仮想なんです\n本物の俺はガチャで引かないと\n会えないんですよ～");
                await Text("キャラクターアイコンをタップすると\nホーム画面に誰を置くか選べます。");
                await Text("とはいっても今は仮想体の俺しか\nいないんですけどね...");
                await Text("ガチャアイコンをタップすると\nガチャ画面に飛びます！");
                await Text("好きなだけ回せるので\n俺をPickUP対象にしてくださいね！\nガチャ画面で僕をタップしてください！");
                await Text("PickUP対象にしてくれると\n俺が出る確率が上がりますよ～");
                await Text("運が悪くても大丈夫！\n天井になったら絶対会いに行きます！");
                await Text("あ！それとそれと！\nガチャで出たお菓子は俺にくれると\n嬉しいなぁ...");
                await Text("あれは戦闘ボタンですね。\n俺のかっこいい姿が見れますよ～\n気が向いたらやってくださいね！");
                await Text("仮想体からいえることは以上です！\n本物の俺と仲良くなって\nハッピーエンド目指しましょうね～");
                break;
            case LuxTalkType.TutorialAgain:
                await Text("は～い！わかりました！\nもう一回説明しますね");
                await Talk(LuxTalkType.Tutorial, token);
                break;
            default:
                if (!_data.SimpleParams.TryGetValue(type, out var param))
                {
                    throw new ArgumentException("Invalid talk type");
                }

                if (_data.Voices.TryGetValue(param.Voice, out var voice)) _voicePlayer.Play(voice);
                if (_data.Images.TryGetValue(param.Image, out var image)) _talkController.CharacterImage.sprite = image;
                foreach (var text in param.Texts)
                {
                    await Text(text);
                }

                break;
        }

        _talkController.TalkBox.SetActive(false);

        async UniTask Text(string text)
        {
            _talkController.TalkText.text = text;
            await _onNext.FirstAsync(token);
        }

        async UniTask<SelectionType> Question(string alpha, string beta)
        {
            return await _talkController.Question(alpha, beta, token);
        }
    }

    public override async UniTask Tutorial(CancellationToken token)
    {

    }
}