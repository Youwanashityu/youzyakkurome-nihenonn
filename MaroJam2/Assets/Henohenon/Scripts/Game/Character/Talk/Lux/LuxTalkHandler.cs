using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using R3;
using UnityEngine;

public class LuxTalkHandler : TalkHandler, IDisposable
{
    private readonly TalkController _talkController;
    private readonly IVoicePlayer _voicePlayer;
    private readonly CharacterData _data;
    private readonly Observable<Unit> _onNext;

    private CancellationTokenSource _cts;

    public LuxTalkHandler(TalkController talkController, IVoicePlayer voicePlayer, Observable<Unit> onNext,
        CharacterData data)
    {
        _talkController = talkController;
        _voicePlayer = voicePlayer;
        _onNext = onNext;
        _data = data;
    }

    protected override async UniTask Talk(int t, CancellationToken token)
    {
        var type = (LuxTalkType)t;
        _talkController.TalkBox.gameObject.SetActive(true);
        switch (type)
        {
            case LuxTalkType.Tutorial:
                MiniImage((int)LuxImageType.Mini_Holo);
                try
                {
                    await Text("こんにちは！\n合えて嬉しいです！");
                    await Text("初めまして！俺はルクス！\nここのことをあなたに教えてあげる！");
                    var answer = await Question("いらない", "わかった");
                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Text("え？もう知ってるんですか？\nわかりました！\n俺をひいてくれるのを待ってるね！");
                            break;
                        case SelectionType.Beta:
                            await Talk((int)LuxTalkType.Tutorial_Stay, token);
                            break;
                    }
                }
                finally
                {
                    Image((int)LuxImageType.Default);
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
                MiniImage((int)LuxImageType.Mini_Holo);
                await Text("は～い！わかりました！\nもう一回説明しますね");
                await Talk((int)LuxTalkType.Tutorial, token);
                break;
            default:
                if (!_data.SimpleParams.TryGetValue(t, out var param))
                {
                    throw new ArgumentException("Invalid talk type");
                }
                
                Voice(param.Voice);
                Image(param.Image);

                foreach (var text in param.Texts)
                {
                    await Text(text);
                }
                break;
        }
        _talkController.TalkBox.SetActive(false);

        async UniTask Delay(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
        }

        async UniTask Text(string text)
        {
            _talkController.TalkText.text = text;
            await _onNext.FirstAsync(token);
        }

        void Image(int t)
        {
            _talkController.MiniTalkButton.gameObject.SetActive(false);
            _talkController.CharaTalkButton.gameObject.SetActive(true);
            if ((LuxImageType)t == LuxImageType.None) return;
            if (_data.Images.TryGetValue(t, out var image)) _talkController.CharacterImage.sprite = image;
        }

        void MiniImage(int t)
        {
            _talkController.MiniTalkButton.gameObject.SetActive(true);
            _talkController.CharaTalkButton.gameObject.SetActive(false);
            if ((LuxImageType)t == LuxImageType.None) return;
            if (_data.Images.TryGetValue(t, out var image)) _talkController.MiniCharaImage.sprite = image;
        }

        void Voice(int t)
        {
            if (_data.Voices.TryGetValue(t, out var voice)) _voicePlayer.Play(voice);
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