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

        // ★ Lux の初回／2回目以降の分岐
        if (type == LuxTalkType.Tutorial)
        {
            if (_data.LuxTutorialDone)
            {
                type = LuxTalkType.TutorialAgain;
            }
            else
            {
                _data.LuxTutorialDone = true;
            }
        }

        // ★ チョコの初回／2回目以降の分岐
        if (type == LuxTalkType.Present_Chocolate)
        {
            if (_data.LuxChocolateDone)
            {
                type = LuxTalkType.Present_ChocolateAgain; // 2回目以降
            }
            else
            {
                _data.LuxChocolateDone = true; 
            }
        }


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
                await Text("仮想体からいえることは以上です！\n本物の俺と仲良くなって\nハッピーエンド目指しましょうね～");
                break;

            case LuxTalkType.TutorialAgain:
                try
                {
                    Image((int)LuxImageType.Mini_Holo);
                    await Text("あっお姉さん！\nまたお話聞きたいですか？");
                    var answer = await Question("いらない", "そうだ");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Image((int)LuxImageType.Mini_Holo);
                            await Text("え～もしかしてかまってちゃんですか？\nそういうのは本物にやってくださいね");
                            break;
                        case SelectionType.Beta:
                            await Talk((int)LuxTalkType.TutorialAgain_Stay, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.TutorialAgain_Stay:
                Image((int)LuxImageType.Mini_Holo);
                await Text("は～い！わかりました！\nもう一回説明しますね");
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

            case LuxTalkType.LIKE01_Scandal:
                try
                {
                    Image((int)LuxImageType.R_DOWN_NOMAL_OPEN);
                    await Text("お姉さんってなんでここに？\nこのあたりは有名でもないし...");
                    Image((int)LuxImageType.R_DOWN_KOMARU);
                    await Text("娯楽は人の醜聞ばかりで...\nきっと俺らのことも噂になるかも？");
                    await Text("皆、他人に興味津々の町なんですよ。\n都会から来たお姉さんには窮屈かも...");
                    Image((int)LuxImageType.R_DOWN_EXCITING);
                    await Text("疲れたら言ってくださいね。\n俺が守りますから！");
                    var answer = await Question("必要ない", "ありがとう");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)LuxTalkType.LIKE01_Scandal_no, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)LuxTalkType.LIKE01_Scandal_yes, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE01_Scandal_no:
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("...お姉さんは強いんですね！\n確かに歳下の俺じゃ頼りないかも...");
                await Text("で、でも！\n力はないけど傍にいることはできます！");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("名前を呼んでくれたら\n3コール以内に駆け付けるから。");
                Image((int)LuxImageType.R_UP_SHY);
                await Text("...あれ、\nそっちじゃ電話ってあまり...\nしないですかね？");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("チャットでもすぐ既読つけますから！\n困ったらルクス！\nよろしくお願いします！");
                break;

            case LuxTalkType.LIKE01_Scandal_yes:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("頼りにしてくださいね！");
                break;

            case LuxTalkType.LIKE01_Interest:
                try
                {
                    Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                    await Text("お姉さんも俺に興味があるんですか？\nいっぱい話しかけてくれて嬉しいです！");
                    var answer = await Question("おや？", "気になって夜しか眠れない");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Oh);
                            await Talk((int)LuxTalkType.LIKE01_Interest_no, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)LuxVoiceType.Sleepatnight);
                            await Talk((int)LuxTalkType.LIKE01_Interest_yes, token);
                            break;
                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE01_Interest_no:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("えっ違うんですか！？\n俺の早とちりってこと...？\nそんな...残念...");
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("興味持ってもらえるよう頑張ります！\n後悔はさせませんから！");
                break;

            case LuxTalkType.LIKE01_Interest_yes:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("えっそんなに！？\r\n夜はちゃんと寝た方が良いですよ！\r\nしょうがない、とっておきですよ？");
                Image((int)LuxImageType.R_DOWN_POKE);
                await Text("俺はルクスです！\n昔からここに住んでいます！");
                Image((int)LuxImageType.R_DOWN_EXCITING);
                await Text("学生なんですが同年代が少なくて...\nあなたがここに来てくれて嬉しいです！");
                Image((int)LuxImageType.R_DOWN_KOMARU);
                await Text("ただ、その...この町は\n皆身内みたいなものでして...\n基本無礼講というか...");
                Image((int)LuxImageType.R_DOWN_SHY);
                await Text("敬語を使う機会がなくて...\nへたっぴでも笑わないでくださいね？");
                break;

            case LuxTalkType.LIKE02_Hello:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("最近お姉さんのことが知れて嬉しいです。\n人と仲良くなれるのって楽しいですね。");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("こんな穏やかな時間がずっと続けば...\nそうは思いませんか？");
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("あっでも遊びには行きたいかも！\nプールとか！プール行きたい！");
                await Text("ねっ行きましょう！\n");
                break;

            case LuxTalkType.LIKE02_Date:
                try
                {
                    Image((int)LuxImageType.R_DOWN_NOMAL_OPEN);
                    await Text("お姉さんは普段何をしているんですか？\n俺はここのカフェで働いていて...\n珈琲淹れるのうまいんですよ。");
                    await Text("ここには珈琲と紅茶しかないですけど...\nクリームソーダを飲んでみたいです！\nお姉さんは飲んだことありますか？");
                    var answer = await Question("ない", "ある");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)LuxTalkType.LIKE02_Date_no, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)LuxTalkType.LIKE02_Date_yes, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE02_Date_no:
                Image((int)LuxImageType.R_DOWN_SHY);
                await Text("えー！！そうなんですか！！！\n美味しいらしいですよ！！！");
                Image((int)LuxImageType.R_DOWN_SMILE_CLOSE);
                await Text("お金が貯まったら上京するので...\nそしたら一緒に飲みに来ましょうね！");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("デートですよ、忘れないでくださいね。");
                break;

            case LuxTalkType.LIKE02_Date_yes:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("いいなーいいなー！！！\n都会のお姉さんだ！！！");
                break;

            case LuxTalkType.LIKE02_Fashion:
                try
                {
                    Image((int)LuxImageType.R_DOWN_SMILE_OPEN);
                    await Text("お姉さんのその服の色素敵ですね！\nどうやって選んでるんですか？");
                    Image((int)LuxImageType.R_DOWN_KOMARU);
                    await Text("ここらへん子供向けのものしかなくて\nおしゃれな服がないというか...");
                    await Text("どうせ遊びに行けもしないので\nずっと制服なんですよね...");
                    Image((int)LuxImageType.R_DOWN_SHY);
                    await Text("それはそれで子供っぽいんですよね...\nもっとかっこいい服が欲しくて...");
                    var answer = await Question("かっこいいよ", "かわいい");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)LuxTalkType.LIKE02_Fashion_cool, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)LuxVoiceType.Cute);
                            await Talk((int)LuxTalkType.LIKE02_Fashion_cute, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE02_Fashion_cool:
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("...ほんとですか？\nお姉さんがそういうなら...まぁ...");
                break;

            case LuxTalkType.LIKE02_Fashion_cute:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("や、やっぱり子供っぽいですか！？\n皆は元気そうで良いっていうけど...");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("都会にはお洒落な服があるんでしょ？\nお姉さん今度選んでよ。");
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("お洒落な服をきて遊びに行こう！\nねっ！");
                break;

            case LuxTalkType.LIKE02_Valentine:
                Image((int)LuxImageType.R_DOWN_SMILE_OPEN);
                await Text("お姉さんチョコレートって好きですか？\n今はバレンタイン？の時期なんですよね");
                Image((int)LuxImageType.R_DOWN_POKE);
                await Text("あれ、もう過ぎてる？\nチョコレートがもらえるんですよね！");
                await Text("俺食べたことなくって...\nここでも出してないんですよ。\nショートケーキはあるんですけどね。");
                Image((int)LuxImageType.R_DOWN_SMILE_CLOSE);
                await Text("一回食べてみたいなぁ..");
                break;

            case LuxTalkType.LIKE02_Photo:
                try
                {
                    Image((int)LuxImageType.R_DOWN_NOMAL_OPEN);
                    await Text("あの...お姉さん写真撮りませんか？");
                    Image((int)LuxImageType.R_DOWN_KOMARU);
                    await Text("記念に何か残るものが欲しくて...\n都会から人なんてなかなか来ないから...");
                    Image((int)LuxImageType.R_UP_MUMUMU);
                    await Text("このお店に飾りたいんです！\nお願い！一枚だけ！");
                    var answer = await Question("今回に限り！", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Onlythistime);
                            Image((int)LuxImageType.R_UP_EXCITING);
                            await Text("やったー！\nあの！二人でとりましょ！");
                            Image((int)LuxImageType.R_DOWN_NOMAL_OPEN);
                            await Text("マスター！！！！\n俺たちのこと撮ってください！！！");
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

            case LuxTalkType.LIKE03_Proposal:
                try
                {
                    Image((int)LuxImageType.R_UP_MUMUMU);
                    await Text("俺は子供っぽいかもしれないけど...\nちゃんとした男として\nあなたのことが好きです！");
                    await Text("世界が君の敵になったとしても守るから\nだから、ここにずっといてよ！");

                    var answer = await Question("さようなら", "かわいい");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Goodbye);
                            await Talk((int)LuxTalkType.LIKE03_Proposal_no, token);
                            break;
                        case SelectionType.Beta:
                            Voice((int)LuxVoiceType.Cute);
                            await Talk((int)LuxTalkType.LIKE03_Proposal_mock, token);
                            break;
                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE03_Proposal_no:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("...?\nか、帰っちゃうんですか...？\nやだ...ここに居てほしい...");
                Image((int)LuxImageType.R_DOWN_KOMARU);
                await Text("まぁでも無理ですよね。\nお姉さんだって仕事とかあるだろうし...");
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("でも大丈夫！必ず迎えに行きます！\nすぐには無理かもしれないけど...\n必ず迎えに行くから！");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("だからお姉さんは俺のこと忘れないで。");
                Image((int)LuxImageType.R_DOWN_NOMAL_CLOSE);
                await Text("俺は諦めないから。");
                break;

            case LuxTalkType.LIKE03_Proposal_mock:
                Image((int)LuxImageType.R_DOWN_SHY);
                await Text("ちょっと！れっきとした男だってば！\nからかわないでくださいよ！");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("もっと成長したら...\n俺のこと好きになってくれる？");
                break;

            case LuxTalkType.LIKE03_Envy:
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("...グレイムとは仲良いんですか？");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("人付き合いは大事ですからね。\n俺もよくちゃんと仲良くしなさいって\n言われてました。");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("他の人と仲良くするのは良いですが\n俺にもちゃんと構ってくださいね。");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("俺はちゃんと「待て」ができる\n良い子ですから...");
                break;

            case LuxTalkType.LIKE03_Gluttony:
                try
                {
                    Image((int)LuxImageType.R_UP_EXCITING);
                    await Text("前から思ってたんですけど\nお姉さんって美味しそうですよね...");
                    var answer = await Question("太くないし！", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Fat);
                            Image((int)LuxImageType.R_DOWN_SHY);
                            await Text("あっちが！そういうことじゃないです！\nただどこもまろくてやわこいし...");
                            Image((int)LuxImageType.R_DOWN_NOMAL_CLOSE);
                            await Text("お姉さんって、どんな味？\n...噛みついたりはしませんよ？");
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

            case LuxTalkType.LIKE03_liking:
                try
                {
                    Image((int)LuxImageType.R_UP_MUMUMU);
                    await Text("お姉さん...その...\n歳上と歳下どっちが好きですか？");
                    var answer = await Question("歳上", "歳下");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)LuxTalkType.LIKE03_liking_up, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)LuxTalkType.LIKE03_liking_down, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.LIKE03_liking_up:
                Image((int)LuxImageType.R_DOWN_KOMARU);
                await Text("ふ～ん\n俺とこんなに仲良いのに？");
                Image((int)LuxImageType.R_DOWN_SMILE_CLOSE);
                await Text("あんまり意地悪しないでくださいね...\n拗ねちゃいますよ。");
                Image((int)LuxImageType.R_DOWN_SHY);
                await Text("...からかっただけですよね？");
                break;

            case LuxTalkType.LIKE03_liking_down:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("...！\nやっぱりそうなんだ！！！\nえへへ、嬉しいです！！！");
                await Text("お姉さん、俺も歳下なんですよ。\n一体誰のことを考えて答えたんですか？");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("俺は逆で歳上が好きなんですよね～\n誰のこと考えて答えたと思います？");
                break;

            case LuxTalkType.LIKE03_Star:
                Image((int)LuxImageType.R_DOWN_NOMAL_OPEN);
                await Text("都会は星が見えないって本当ですか？\nビルの光で空が見えないって聞きました");
                Image((int)LuxImageType.R_DOWN_KOMARU);
                await Text("ちょっと残念ですね...\n星はあんなにもきれいなのに。");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("都会に帰る前に俺と星を見ませんか？\n綺麗に見えるところを知ってるんです。");
                await Text("俺星座詳しいんですよ！\n娯楽がないというのもありますが...");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("少なくともその嘘ばっかりつく\nスマホの検索機能より正確ですよ。");
                Image((int)LuxImageType.R_PIECE_SMILE_CLOSE);
                await Text("だからスマホばっかり見てないで...\n俺のことも見てね。");
                break;


            case LuxTalkType.Present_Chocolate:
                try
                {
                    Image((int)LuxImageType.R_UP_EXCITING);
                    await Text("えっこれってチョコなんですか！？\nおしゃれですね！嬉しいなぁ\nえへへ、どれから食べようかなぁ");
                    Image((int)LuxImageType.R_UP_MUMUMU);
                    await Text("ちなみにこれって俺だけ...ですよね？");
                    var answer = await Question("そうだ", "からかう");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            await Talk((int)LuxTalkType.Present_Chocolate_love, token);
                            break;
                        case SelectionType.Beta:
                            await Talk((int)LuxTalkType.Present_Chocolate_mock, token);
                            break;

                    }

                }
                finally
                {
                    Image((int)LuxImageType.Default);
                }
                break;

            case LuxTalkType.Present_Chocolate_love:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("...当たり前ですよね！\nお姉さんは俺のことが好きなんだから！");
                break;


            case LuxTalkType.Present_Chocolate_mock:
                try
                {
                    Image((int)LuxImageType.R_UP_SHY);
                    await Text("ちょっと...\n違うんですか...？\nこんなにきれいなのを他の人にも...？");
                    var answer = await Question("おや？", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Voice((int)LuxVoiceType.Oh);
                            await Talk((int)LuxTalkType.Present_Chocolate_mock_mock, token);
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

            case LuxTalkType.Present_Chocolate_mock_mock:
                try
                {
                    Image((int)LuxImageType.R_UP_MUMUMU);
                    await Text("お姉さんって結構ひどいんですね。\n俺はあなた以外いらないのに。");
                    Image((int)LuxImageType.R_DOWN_NOMAL_CLOSE);
                    await Text("歳下もてあそんで楽しいですか？");
                    var answer = await Question("からかっただけだ", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Image((int)LuxImageType.R_DOWN_KOMARU);
                            await Text("へぇ～そういうことするんですね。\n俺傷ついちゃいました。");
                            Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                            await Text("でも俺は優しいから許してあげる！");
                            Image((int)LuxImageType.R_DOWN_POKE);
                            await Text("お詫びの印にチョコ食べさせてよ\nほら早く、あ～");
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

            case LuxTalkType.Present_ChocolateAgain:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("あっチョコだ！\nわ～い！この間のおいしかったです！");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("そうだ！口開けてください！\nお姉さんにも食べさせてあげますよ。\nほら早く、あ～");
                break;

            case LuxTalkType.Present_Tiramisu:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("わ～おいしそうなケーキ！！！\n女の人からこんなの貰ったの初めて！");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("待っててください！紅茶淹れますね！\n一緒に食べましょう！");
                break;

            case LuxTalkType.Present_Marronglacé:
                Image((int)LuxImageType.R_DOWN_POKE);
                await Text("なんですかこれ、栗？？？\n都会の人って栗好きなんですか？");
                Image((int)LuxImageType.R_DOWN_SMILE_CLOSE);
                await Text("栗採れる場所俺知ってますよ！\nあとで一緒に行きますか？");
                break;

            case LuxTalkType.Present_Madeleine:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("わ！なんかふわふわしてる！\n美味しそう！！！");
                await Text("まどれーぬ？\nなんか...お洒落だ...！\n都会ってすげえ...！");
                break;

            case LuxTalkType.Present_Macaron:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("え...\nその...");
                Image((int)LuxImageType.R_UP_MUMUMU);
                await Text("俺でも知ってますよ！\nこれって高級品...ですよね？");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("お姉さんにとって俺って特別？\n嬉しいな。");
                break;

            case LuxTalkType.Present_Donut:
                Image((int)LuxImageType.R_UP_EXCITING);
                await Text("えー！！！！\nドーナツだ！！！！");
                await Text("チョコかけていいんですか！？\n初めて見ました！！！\nわー美味しそう！！！");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("お姉さん大好き！");
                break;

            case LuxTalkType.Present_Cupcake:
                Image((int)LuxImageType.R_DOWN_SHY);
                await Text("おぉ...キラキラしてる...\nこのキラキラ食べていいやつ？");
                await Text("食べられるんだ...都会ってすげえ...");
                break;

            case LuxTalkType.Present_Cookie:
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("クッキーですね！！！\n食べたことあります！！\n美味しいですよね！");
                await Text("今食べちゃお。\n美味しいです！ありがとうお姉さん！");
                break;

            case LuxTalkType.Present_Caramel:
                Image((int)LuxImageType.R_UP_SHY);
                await Text("キャラメルだー！！！\nわっ手がべたべたする。");
                Image((int)LuxImageType.R_UP_SMILE_CLOSE);
                await Text("キャラメル大好きなんです！\nお姉さんも一緒に食べましょう！");
                break;


            case LuxTalkType.Present_Candy:
                try
                {
                    Image((int)LuxImageType.R_UP_MUMUMU);
                    await Text("あめちゃん...\n俺のこと子供っぽいって思ってます？");
                    await Text("チョコとか欲しかったな...\n男じゃなくて子供なんでしょ？");
                    var answer = await Question("好きだからあげた", "");

                    switch (answer)
                    {
                        case SelectionType.Alpha:
                            Image((int)LuxImageType.R_UP_SHY);
                            await Text("...えっ");
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

            case LuxTalkType.Present_Baumkuchen:
                Image((int)LuxImageType.R_DOWN_POKE);
                await Text("結婚式でもらうやつだ！\n美味しいですよね。");
                Image((int)LuxImageType.R_DOWN_NOMAL_CLOSE);
                await Text("俺らも配りましょうね。");
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