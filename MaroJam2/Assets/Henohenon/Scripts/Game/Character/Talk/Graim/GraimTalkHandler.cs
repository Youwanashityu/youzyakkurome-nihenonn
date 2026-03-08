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

            case GraimTalkType.LIKE01_Hello:
                try
                {
                    Image((int)GraimImageType.G_up_humu_close);
                    await Text("... ...");
                    var answer = await Question("おはようございまーす！", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.GoodMorning);
                            Image((int)GraimImageType.G_down_haa);
                            await Text("...お嬢ちゃん元気だねえ。\r\nおはよう...これで満足かな？");
                            break;
                        case SelectionType.Beta:
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE01_Money:
                try
                {
                    Image((int)GraimImageType.G_down_happy);
                    await Text("お嬢ちゃん、お金ほしくないかい？\n別に怪しい話じゃないよ？");
                    var answer = await Question("人違いですね", "5000兆円欲しい！");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Wrongperson);
                            Image((int)GraimImageType.G_up_nomal_close);
                            await Text("あらら...怪しくないって...\n最近の子は冷たいねぇ。\n残念だね？");

                            break;
                        case SelectionType.Beta:
                            Voice((int)GraimVoiceType.Trillionyen);
                            await Talk((int)GraimTalkType.LIKE01_Money_yes, token); 
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE01_Money_yes:
                try
                {
                    Image((int)GraimImageType.G_down_why);
                    await Text("ううん...大きく出たねぇ...\nまあ、不可能じゃないけど。\nほんとに欲しいの？");
                    Image((int)GraimImageType.G_up_humu_giza);
                    await Text("それなら鍵の横にある＋ボタンを\n押すといい、換金所に案内される。");
                    Image((int)GraimImageType.G_up_humu_close);
                    await Text("お金がいっぱいあるのが見えるはずだ。\n全部君が使っていい。\n全部ガチャに使っていいんだよ。");
                    Image((int)GraimImageType.G_up_nomal_giza);
                    await Text("...出所が気になる？\nへぇ、君聡いね。\nこのお金は今は他所では使えないよ。");
                    Image((int)GraimImageType.G_up_humu_close);
                    await Text("まあ今バレなければ良い話だが...\nちょっとの辛抱だよ。");
                    Image((int)GraimImageType.G_up_humu_giza);
                    await Text("それにここだけでも良いんだよ。\n少しずつ金を回すんだ。\nいずれ全ての人間がこの金を手に入れる。");
                    Image((int)GraimImageType.G_down_aori_giza);
                    await Text("しばらくしたら皆がこのお金を使う。\nそしたら僕の勝ちだ。");
                    Image((int)GraimImageType.G_pa_yoyuu);
                    await Text("見分けのつかない悪貨を誰が断罪する？\n黙って本物を懐にしまえばいい。");
                    var answer = await Question("無罪！", "有罪！");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Innocence);
                            Image((int)GraimImageType.G_up_nomal_giza);
                            await Text("そうだよ、これは当然の現象なんだ。\nお嬢ちゃんも損をしたくないなら\nこのお金で遊ぶんだ。");
                            break;
                        case SelectionType.Beta:
                            Voice((int)GraimVoiceType.Guilty);
                            await Talk((int)GraimTalkType.LIKE01_Money_yes_guilty, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE01_Money_yes_guilty:
                Image((int)GraimImageType.G_down_damattore);
                await Text("う～ん、でももう手遅れじゃないかなぁ\nだってここに僕がいるってことは\n君はそのお金でガチャを回した訳だし...");
                Image((int)GraimImageType.G_up_nomal_open);
                await Text("ふふ...僕らは共犯者なんだよ。\n仲良くしよう、ね？");
                break;

            case GraimTalkType.LIKE01_Envy:
                Image((int)GraimImageType.G_up_nomal_open);
                await Text("僕の相手ばっかしてていいのかな？\n少年が拗ねてしまいそうだが...");
                Image((int)GraimImageType.G_up_humu_close);
                await Text("まあ、君の選択だ。\n否定なんてしないさ。");
                break;

            case GraimTalkType.LIKE01_Question:
                try
                {
                    Image((int)GraimImageType.G_down_why);
                    await Text("君はどうして来てくれるのかな？\nあぁ、責めてるわけじゃないよ。");
                    Image((int)GraimImageType.G_down_haa);
                    await Text("人付き合いをしてこなかったからね。\n新鮮なんだ。\n君は僕にどんな利益を見出したのかな？");
                    var answer = await Question("説明しよう！", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Explanation);
                            Image((int)GraimImageType.G_up_tere);
                            await Text("ふぅん、言語化できるんだ。\n結構僕のこと考えているじゃないか。\n暇なんだねぇ...");
                            break;
                        case SelectionType.Beta:
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE01_Interest:
                try
                {
                    Image((int)GraimImageType.G_down_aori_open);
                    await Text("君、僕に興味津々だね。\nそんなに僕のことが知りたいのかい？");
                    var answer = await Question("おや？", "気になって夜しか眠れない");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)GraimTalkType.LIKE01_Interest_no, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)GraimTalkType.LIKE01_Interest_yes, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE01_Interest_no:
                Voice((int)GraimVoiceType.Oh);
                Image((int)GraimImageType.G_down_tere);
                await Text("あらら？違うのかい？\nこれは恥ずかしいことをしたね...");
                Image((int)GraimImageType.G_down_nomal_giza);
                await Text("気になったら言うといい。\n損はさせないことを約束するよ。");
                break;

            case GraimTalkType.LIKE01_Interest_yes:
                Voice((int)GraimVoiceType.Sleepatnight);
                Image((int)GraimImageType.G_down_aori_open);
                await Text("それは大変だ。\n子守歌でも歌ってあげようか？\n...なんてね");
                Image((int)GraimImageType.G_up_humu_giza);
                await Text("僕はグレイム。\n仕事で最近こっちに来ていたが\n存外居心地が良くてね...");
                Image((int)GraimImageType.G_up_nomal_close);
                await Text("しばらくはここに居るよ。\n仕事内容は...言わない方が良いか。");
                Image((int)GraimImageType.G_down_damattore);
                await Text("まあ、生活するのに困らないくらいには\n稼いでいる、自立した男だよ。");
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