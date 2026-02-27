using System;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

public class LuxTalkHandler : TalkHandler, IDisposable
{
    private readonly TalkController _talkController;
    private readonly IVoicePlayer _voicePlayer;
    private readonly CharacterData _data;
    private readonly Observable<Unit> _onNext;

    private CancellationTokenSource _cts;

    public LuxTalkHandler(TalkController talkController, IVoicePlayer voicePlayer, Observable<Unit> onNext, CharacterData data)
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

            case LuxTalkType.LIKE01_Hello:
                try
                {
                    Image((int)LuxImageType.R_DOWN_POKE);
                    await Text("やっと会えましたね！お姉さん！\n俺、嬉しいです！\nこんにちは！");
                    var answer = await Question("おはようございまーす！", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Hello);
                            await Talk((int)LuxTalkType.LIKE01_Hello_stay, token);
                            break;
                        case SelectionType.Beta:
                            break;

                    }
                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;
            case LuxTalkType.LIKE01_Hello_stay:
                Image((int)LuxImageType.R_DOWN_SMILE_OPEN);
                await Text("えへへ、こうして会話できるなんて..\n俺のことを選んでくれたんですね。");
                await Text("選ばれたからにはこのルクス！\n後悔はさせません！\n期待してください！");
                break;

            case LuxTalkType.LIKE01_Money:
                try
                {
                    Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                    await Text("お姉さん、お金に困っていませんか？\n耳寄りな情報がありますよ！");
                    var answer = await Question("人違いですね", "ゼロ！");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Wrongperson);
                            await Talk((int)LuxTalkType.LIKE01_Money_no, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)LuxVoiceType.Zero);
                            await Talk((int)LuxTalkType.LIKE01_Money_yes, token);
                            break;
                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE01_Money_no:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("そんな！怪しくないですよ！\nお姉さんに幸せになってほしいだけ！\n危なくないです！");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("お姉さーん！！！！！！");
                break;

            case LuxTalkType.LIKE01_Money_yes:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("ゼロ！！？！？！？！！！！\nあっここ奢りましょうか！？！？！？");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("...まぁそれなら早速教えてあげます！\n鍵の横の＋ボタンを押してください！");
                await Text("お金がいっぱい出てきますからね！\n全部お姉さんのものです！\nガチャに使っていいんですよ！");
                Image((int)LuxImageType.R_DOWN_POKE);
                await Text("このお金はどこから...?\nさぁ...?");
                Image((int)LuxImageType.R_DOWN_NOMAL_CLOSE);
                await Text("グレイムさんが流通させている\nここ限定通貨だとは聞きましたけど...");
                await Text("ほらなんでしたっけ...\r\n商品券と大して変わらない？みたいな？");
                Image((int)LuxImageType.R_DOWN_POKE);
                await Text("意外と彼も良い人ですね～");
                break;

            case LuxTalkType.LIKE01_Aroma:
                try
                {

                    Image((int)LuxImageType.R_UP_EXCITING);
                    await Text("お姉さんって良い匂いしますね～\nやっぱり向こうじゃ香水って当たり前？");
                    Image((int)LuxImageType.R_UP_SHY);
                    await Text("あっすみません嗅いじゃって...!\n気になっちゃって...");
                    var answer = await Question("有罪！", "無罪！");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Guilty);
                            await Talk((int)LuxTalkType.LIKE01_Aroma_ng, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)LuxVoiceType.Innocence);
                            await Talk((int)LuxTalkType.LIKE01_Aroma_ok, token);
                            break;
                    }
                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE01_Aroma_ng:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("す、すみません...!\n恥ずかしかったですよね...");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("...俺のことも嗅ぎますか？\nキャロットラペの匂いがしますよ。");
                break;

            case LuxTalkType.LIKE01_Aroma_ok:
                try
                {


                    Image((int)LuxImageType.R_DOWN_POKE);
                    await Text("怒らないんですか？\nじゃあまだ嗅いで良いですか？");
                    var answer = await Question("駄目だ", "いいよ");
　　　　　　　　　 switch (answer)
                    {
                        case SelectionType.Alpha:
                            Image((int)LuxImageType.R_UP_MUMUMU);
                            await Text("ちぇっ");
                            break;
                        case SelectionType.Beta:
                             await Talk((int)LuxTalkType.LIKE01_Aroma_ok_yes, token);
                            break;
                    }
                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE01_Aroma_ok_yes:
                Image((int)LuxImageType.R_DOWN_SMILE_OPEN);
                await Text("やったー！\n犬みたいでみっともないって\n怒られちゃうんですよね...");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("お姉さんは...\n俺のこと犬っぽいって思いますか？");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("もし思っていたら...\n責任もって飼ってほしいなぁ...\nなんてね。");
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