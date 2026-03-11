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

        // ★ 初回 → Tutorial、2回目以降 → TutorialAgain に差し替える
        if (type == GraimTalkType.Tutorial)
        {
            if (_data.GraimTutorialDone)
            {
                type = GraimTalkType.TutorialAgain; // 2回目以降
            }
            else
            {
                _data.GraimTutorialDone = true; // 初回終了フラグを立てる
            }
        }



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

            case GraimTalkType.LIKE02_Dog:
                try
                {
                    Image((int)GraimImageType.G_down_haa);
                    await Text("君も飽きないねぇ...\nあっちをうろちょろこっちをうろちょろ");
                    Image((int)GraimImageType.G_down_aori_giza);
                    await Text("僕のことを嗅ぎまわって楽しいかい？\nまるで犬だね。");
                    var answer = await Question("おや？", "ワン！");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Oh);
                            await Talk((int)GraimTalkType.LIKE02_Dog_no, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)GraimVoiceType.Woof);
                            await Talk((int)GraimTalkType.LIKE02_Dog_yes, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE02_Dog_no:
                Image((int)GraimImageType.G_down_aori_open);
                await Text("自覚がないのかい？\nいつも近くに...");
                Image((int)GraimImageType.G_up_tere);
                await Text("待って、それは僕の方か？");
                break;

            case GraimTalkType.LIKE02_Dog_yes:
                Image((int)GraimImageType.G_down_why);
                await Text("...\nいや...その...");
                Image((int)GraimImageType.G_up_tere);
                await Text("ふふふ...\nお嬢ちゃん僕に飼われたいの？");
                Image((int)GraimImageType.G_down_aori_giza);
                await Text("似合う首輪でも買ってきてあげようか？\n...\n冗談だからね？");
                break;


            case GraimTalkType.LIKE02_Secret:
                try
                {
                    Image((int)GraimImageType.G_up_nomal_close);
                    await Text("お嬢ちゃんはどうしてここに？\n見た感じ同業者でもなさそうだし...");
                    Image((int)GraimImageType.G_up_humu_giza);
                    await Text("ここらは静かでいいよね...\n人気もあまりないし...");
                    Image((int)GraimImageType.G_up_nomal_giza);
                    await Text("内緒話にはもってこいだと思わないかい");
                    var answer = await Question("静粛に！", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Quietly);
                            Image((int)GraimImageType.G_down_nomal_giza);
                            await Text("ふふ、お嬢ちゃんは元気だねえ。\n内緒話には向かなそうだね？");
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

            case GraimTalkType.LIKE02_Drink:
                try
                {
                    Image((int)GraimImageType.G_up_nomal_close);
                    await Text("あら、飲み物もなくなっちゃったね。\n新しいのを頼もうか。");
                    Image((int)GraimImageType.G_up_nomal_open);
                    await Text("そうだね...君、お酒は飲めるかい？");
                    var answer = await Question("飲めない", "飲める");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)GraimTalkType.LIKE02_Drink_no, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)GraimTalkType.LIKE02_Drink_yes, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE02_Drink_no:
                Image((int)GraimImageType.G_up_nomal_giza);
                await Text("...君飲めないの？\nへぇ～若いとは思っていたが\nそこまで幼いとは...");
                Image((int)GraimImageType.G_down_aori_open);
                await Text("残念だね？\n牛乳でも飲むかい？");
                Image((int)GraimImageType.G_down_aori_giza);
                await Text("ふふ...可愛い子だね。\n僕はワインでもいただこうかな。");
                Image((int)GraimImageType.G_down_haa);
                await Text("えっワインは今ない？\n...");
                Image((int)GraimImageType.G_down_haa);
                await Text("これだから田舎の流通は...");
                break;

            case GraimTalkType.LIKE02_Drink_yes:
                Image((int)GraimImageType.G_up_humu_giza);
                await Text("それはいいね。\nアルコールでリラックスして\n会話に耽るのは良いものだよ。");
                Image((int)GraimImageType.G_up_nomal_close);
                await Text("キールとかどうかな？\n白ワインを使ったカクテルだよ。");
                Image((int)GraimImageType.G_down_why);
                await Text("ところで君、カクテル言葉ってわかる？\nいや、分からないならいいんだ。\nあまり馴染みがないだろうしね。");
                break;

            case GraimTalkType.LIKE02_Valentine:
                try
                {
                    Image((int)GraimImageType.G_up_nomal_open);
                    await Text("お嬢ちゃん、甘未は好きかい？\nほら、バレンタインの時期だろう？今");
                    Image((int)GraimImageType.G_down_damattore);
                    await Text("あれ、もう過ぎてるんだったかな。\n時差ボケかな...そんなレベルじゃない？");
                    Image((int)GraimImageType.G_down_damattore);
                    await Text("お嬢ちゃんには好い人がいるのかな？\n恋人とかいたりする？");
                    var answer = await Question("理論上は可能です", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Theoretically);
                            Image((int)GraimImageType.G_down_haa);
                            await Text("...人体錬成するとかそういうことかな？\nあまり触れちゃいけなかったかな...");
                            Image((int)GraimImageType.G_down_happy);
                            await Text("ちなみに僕は今フリーだからね。\n安心してくれていいよ？");
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

            case GraimTalkType.LIKE02_Accessories:
                try
                {
                    Image((int)GraimImageType.G_up_nomal_close);
                    await Text("君ってアクセサリーはつける？\nあぁ、いやちょっとね。");
                    Image((int)GraimImageType.G_up_humu_close);
                    await Text("僕らの出会いはまさしく運命だろう。\n記念に何かお嬢ちゃんに残したくてね。");
                    Image((int)GraimImageType.G_down_nomal_close);
                    await Text("君を子供じゃなくて一人の女性として...\nそう扱いたいんだ。\nピアスとかどうかな？");
                    Image((int)GraimImageType.G_down_smile);
                    await Text("近々開けようと思ってね、\nお揃いのものを君につけたいんだ。");
                    var answer = await Question("よろしくてよ", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Nicetomeetyou);
                            Image((int)GraimImageType.G_up_tere);
                            await Text("...!\r\nお嬢ちゃん、その返事だとあれだね、\n悪役令嬢みたいだね。");
                            Image((int)GraimImageType.G_up_nomal_close);
                            await Text("君の考える大人の女性はそうなのかな？\n可愛らしい子だね。");
                            Image((int)GraimImageType.G_down_aori_open);
                            await Text("今度一緒に選びに行こうね。");
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

            case GraimTalkType.LIKE03_Proposal:
                try
                {
                    Image((int)GraimImageType.G_down_damattore);
                    await Text("僕は口数が多い方ではないが...\n君のことはかなり気に入っているよ。\nそう見えない？悲しいね");
                    Image((int)GraimImageType.G_pa_yoyuu);
                    await Text("そうだな...\n君の返答によっては\nずっとここに居ても良いと思っているよ。");
                    var answer = await Question("ばいばーい", "いえーい！");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Goodbye);
                            await Talk((int)GraimTalkType.LIKE03_Proposal_no, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)GraimVoiceType.Yeah);
                            await Talk((int)GraimTalkType.LIKE03_Proposal_yes, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE03_Proposal_no:
                try
                {
                    Image((int)GraimImageType.G_down_haa);
                    await Text("えっそんな...\n君ってもしかして僕のことが嫌いかい？");
                    Image((int)GraimImageType.G_down_haa);
                    await Text("そんなわけないだろう？\n僕らの仲じゃないか、酷いなぁ。");
                    Image((int)GraimImageType.G_up_nomal_close);
                    await Text("冗談だよね？\n冗談だといいなさい。");
                    var answer = await Question("ばいばーい", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Goodbye);
                            Image((int)GraimImageType.G_up_humu_close);
                            await Text("... ...");
                            Image((int)GraimImageType.G_down_why);
                            await Text("本当に？");
                            Image((int)GraimImageType.G_down_haa);
                            await Text("君と関わっていて思ったが\n君は頭が弱すぎるな。\n僕を拒絶するのは得策ではないはずだ。");
                            Image((int)GraimImageType.G_down_damattore);
                            await Text("お嬢ちゃんは弱いんだから\n僕と一緒に居た方が良い、そうだろう？");
                            Image((int)GraimImageType.G_down_tere);
                            await Text("それとも少年の方に行くつもりか？\r\n");
                            Image((int)GraimImageType.G_down_why);
                            await Text("若ければ誰でも良いのか？");
                            Image((int)GraimImageType.G_down_damattore);
                            await Text("当たり前だが歳の分知識と経験がある。\n俺は彼より強い。");
                            Image((int)GraimImageType.G_down_damattore);
                            await Text("考え直すべきだ。\nお嬢ちゃんが手を取るべきは俺だ。");
                            Image((int)GraimImageType.G_up_humu_giza);
                            await Text("... ...\n時間を上げるから考え直すといい。");
                            Image((int)GraimImageType.G_up_nomal_close);
                            await Text("色よい返事を期待しているよ。");
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

            case GraimTalkType.LIKE03_Proposal_yes:
                Image((int)GraimImageType.G_down_aori_giza);
                await Text("ふふ、可愛い子だね。\n君の純粋さは美徳だ。");
                Image((int)GraimImageType.G_down_smile);
                await Text("...ほんとにここに残っちゃおうかな？");
                break;

            case GraimTalkType.LIKE03_Pride:
                Image((int)GraimImageType.G_up_nomal_close);
                await Text("少年に会うのはやめなさい。いいね？\n僕がいるんだから充分だろう？");
                Image((int)GraimImageType.G_down_damattore);
                await Text("付き合う友達は選んだほうが良いよ。\nお嬢ちゃんの為を思って言っているんだ。");
                break;

            case GraimTalkType.LIKE03_Muscle:
                try
                {
                    Image((int)GraimImageType.G_down_nomal_close);
                    await Text("これは単なる雑談だが...\nお嬢ちゃんは筋肉は好きかい？");
                    var answer = await Question("飯を食え！", "仕上がってるよ！");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Eat);
                            await Talk((int)GraimTalkType.LIKE03_Muscle_yes, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)GraimVoiceType.Youareripped);
                            await Talk((int)GraimTalkType.LIKE03_Muscle_mock, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE03_Muscle_yes:
                try
                {
                    Image((int)GraimImageType.G_down_haa);
                    await Text("む、やっぱり好きなんだね。\n困ったねぇ...\n仕事柄筋肉はつけられないんだ。");
                    Image((int)GraimImageType.G_down_haa);
                    await Text("誠実さが必要な仕事だからね、\nほら...ムキムキだと威圧感がね...");
                    Image((int)GraimImageType.G_down_happy);
                    await Text("あ、でも力は結構強いんだよ、\n試してみるかい？");
                    var answer = await Question("真の魔法少女の戦いが始まる！", "勝負");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)GraimVoiceType.Magicalgirl);
                            await Talk((int)GraimTalkType.LIKE03_Muscle_yes_mock, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)GraimVoiceType.Match);
                            await Talk((int)GraimTalkType.LIKE03_Muscle_yes_yes, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE03_Muscle_mock:
                Image((int)GraimImageType.G_up_tere);
                await Text("お嬢ちゃん、適当に言ってるだろう");
                Image((int)GraimImageType.G_up_tere);
                await Text("僕がこんなに健気に君好みになろうとしているのに...");
                Image((int)GraimImageType.G_down_why);
                await Text("...あ");
                break;

            case GraimTalkType.LIKE03_Muscle_yes_mock:
                try
                {
                    Image((int)GraimImageType.G_down_why);
                    await Text("えっ");
                    await Text("お嬢ちゃんって魔法少女だったのかい？\nなるほど確かに...");
                    Image((int)GraimImageType.G_down_tere);
                    await Text("ちょっと待ちたまえ、\n僕も魔法少女になるのかい？");
                    Image((int)GraimImageType.G_down_haa);
                    await Text("その...大変そうだね...\n確定申告とか...\n業務委託になるのかな？");
                    var answer = await Question("冗談だ", "私と契約してくれ");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)GraimTalkType.LIKE03_Muscle_yes_mock_sorry, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)GraimTalkType.LIKE03_Muscle_yes_mock_fool, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE03_Muscle_yes_yes:
                Image((int)GraimImageType.G_down_haa);
                await Text("お嬢ちゃんと争うつもりはないかな？\nああでもそっちの方がわかりやすいか...");
                Image((int)GraimImageType.G_down_happy);
                await Text("妥協してハグとかどうかな？\n力いっぱい抱きしめてあげるよ。");
                Image((int)GraimImageType.G_up_nomal_giza);
                await Text("なんて、誠実な男は付き合ってもない\nお嬢ちゃんと抱き合わないか。");
                Image((int)GraimImageType.G_down_aori_giza);
                await Text("残念だね？");
                break;

            case GraimTalkType.LIKE03_Muscle_yes_mock_sorry:
                try
                {
                    Image((int)GraimImageType.G_down_why);
                    await Text("...　...");
                    Image((int)GraimImageType.G_down_haa);
                    await Text("お嬢ちゃんって結構...\n度胸があるね？");
                    Image((int)GraimImageType.G_down_nomal_close);
                    await Text("僕にそんな発言するのは君くらいだよ");
                    var answer = await Question("怒った？", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Image((int)GraimImageType.G_down_aori_open);
                            await Text("ふふ、怒ってないよ。\nああでもそうだな...\n魔法少女姿は見たかったね？");
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

            case GraimTalkType.LIKE03_Muscle_yes_mock_fool:
                Image((int)GraimImageType.G_down_tere);
                await Text("持ち帰って検討しようかな...?");
                break;

            case GraimTalkType.LIKE03_Cheating:
                Image((int)GraimImageType.G_down_aori_open);
                await Text("どこからが浮気だと思うか？\nふふ、なに恋バナでもしたいのかい？");
                Image((int)GraimImageType.G_down_damattore);
                await Text("まあ確かに何が好きかよりも\n何が嫌いかわかっていた方が\n今後の関係も良いだろう。");
                Image((int)GraimImageType.G_up_nomal_close);
                await Text("そうだね...目を合わせたら...かな？");
                Image((int)GraimImageType.G_up_humu_close);
                await Text("他人と交流してほしくないんだ。\n仕事なんてさせないし、\n買い物は通販で良いだろう？");
                Image((int)GraimImageType.G_down_haa);
                await Text("パートナーはアクセサリーじゃないんだ。\n外で見せびらかすとかは考えられないな");
                Image((int)GraimImageType.G_down_aori_open);
                await Text("瞳に僕の構成したものだけを映す、\nこれが一番素敵だね...");
                Image((int)GraimImageType.G_down_smile);
                await Text("でももし自由が欲しいというなら...\n休日はデートをしよう！");
                break;

            case GraimTalkType.LIKE03_Date:
                try
                {
                    Image((int)GraimImageType.G_up_humu_close);
                    await Text("今は責任ある立場だから難しいが...\n落ち着いたら静かな場所に行きたいね。");
                    Image((int)GraimImageType.G_up_nomal_open);
                    await Text("君は海と山どっちが好きかな？");
                    var answer = await Question("海", "山");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)GraimTalkType.LIKE03_Date_sea, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)GraimTalkType.LIKE03_Date_Mountain, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)GraimImageType.Default);
                }
                break;

            case GraimTalkType.LIKE03_Date_sea:
                Image((int)GraimImageType.G_up_nomal_close);
                await Text("ああいいね、\nお嬢ちゃんは泳げるかな？\n夏になったら泳ぎに行こう。");
                break;

            case GraimTalkType.LIKE03_Date_Mountain:
                Image((int)GraimImageType.G_up_nomal_close);
                await Text("そうだね...\n春になったら一緒に登ろうか。\n山頂で飲む珈琲は美味しいらしいよ。");
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