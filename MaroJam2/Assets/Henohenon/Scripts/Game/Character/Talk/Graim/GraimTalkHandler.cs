using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

public class GraimTalkHandler : TalkHandler, IDisposable
{
    private readonly TalkController _talkController;
    private readonly IVoicePlayer _voicePlayer;
    private readonly CharacterData _data;
    private readonly Observable<Unit> _onNext;

    private CancellationTokenSource _cts;

    public GraimTalkHandler(TalkController talkController, IVoicePlayer voicePlayer, Observable<Unit> onNext,
        CharacterData data)
    {
        _talkController = talkController;
        _voicePlayer = voicePlayer;
        _onNext = onNext;
        _data = data;
    }

    protected override async UniTask Talk(int t, CancellationToken token)
    {
        var type = (GraimTalkType)t;
        _talkController.TalkBox.gameObject.SetActive(true);
        switch (type)
        {
            case GraimTalkType.Tutorial:
                try
                {
                    Image((int)GraimImageType.Mini_Holo);
                    await Text("...え？もう説明聞いたよね？\r\nこの体は偽物で仲よくはなれないよ？");
                    await Text("はぁ...初めましてお嬢ちゃん。\r\nこの場所のことを教えてあげるよ。");
                    var answer = await Question("いらない", "おねがい");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Text("な、なにしにきたんだ？\nお嬢ちゃんからかってるのかい？\nふふ、面白い子だね。");
                            await Text("本物ともきっとうまくやれるよ、\n早く僕をひいてくれないか？");

                            break;
                        case SelectionType.Beta:
                            await Talk((int)GraimTalkType.Tutorial_Stay, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.Tutorial_Stay:
                Image((int)GraimImageType.Mini_Holo);
                await Text("...少年の方から話は聞いたんだろう？\n僕と話したかったのかい？\nふふ、可愛い子だね。");
                await Text("まぁもう聞いたかと思うが...\nこの体は偽物でね。\n本物はガチャをひけば出会えるよ。");
                await Text("今の僕は君と仲良くなっても何も\n起きないんだ。残念だね？");
                await Text("君の目的は僕か少年と仲良くなること。\n本物の人間と仲良くなれば\n文字通りクリアってわけだ。");
                await Text("ガチャアイコンをタップするといい。\n現在開催中のガチャは一個だ。\n君はそれをまわすといい。");
                await Text("キャラクターの対象は僕か少年だ。\n好きなだけひくといい。");
                await Text("あぁ、ピックアップ対象を僕にしなさい\nタップして変えるだけだ。簡単だね？\nそうすればきっと僕と出会えるはずだ。");
                await Text("まあ、お嬢ちゃんの運が悪くとも\n天井が来たら必ず迎えに行くさ。");
                await Text("それと。\nガチャで出たお菓子は僕によこせ。\n僕と仲良くなりたいんだろう？");
                await Text("仮想体からは以上だ。\nいくらでも話してあげるから、\nまたおいで？");
                break;

            case GraimTalkType.TutorialAgain:
                try
                {
                    Image((int)GraimImageType.Mini_Holo);
                    await Text("やぁ、お嬢ちゃん。\n約束通り僕の話を聞きに？");
                    var answer = await Question("ちがう", "そうだ");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Text("...君、僕のことからかってるよね？\n誰にでもやってるのかい、それ？\n意地の悪い子だ。");
                            break;
                        case SelectionType.Beta:
                            await Talk((int)GraimTalkType.TutorialAgain_Stay, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.TutorialAgain_Stay:
                Image((int)GraimImageType.Mini_Holo);
                await Text("うんうん、良い子だね。\nもう一回話してあげよう。\nあぁ、何回でもいいよ？");
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

        void Image(int imageType)
        {
            _talkController.MiniTalkButton.gameObject.SetActive(false);
            _talkController.CharaTalkButton.gameObject.SetActive(true);
            if ((GraimImageType)imageType == GraimImageType.None) return;
            if (_data.Images.TryGetValue(imageType, out var image)) _talkController.CharacterImage.sprite = image;
        }

        void MiniImage(int imageType)
        {
            _talkController.MiniTalkButton.gameObject.SetActive(true);
            _talkController.CharaTalkButton.gameObject.SetActive(false);
            if ((GraimImageType)imageType == GraimImageType.None) return;
            if (_data.Images.TryGetValue(imageType, out var image)) _talkController.MiniCharaImage.sprite = image;
        }
        
        void Voice(int voiceType)
        {
            if (_data.Voices.TryGetValue(voiceType, out var voice)) _voicePlayer.Play(voice);
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