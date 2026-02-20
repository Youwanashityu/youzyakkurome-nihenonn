using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TalkController : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text talkText;
    [SerializeField] private TMP_Text selectionTextA;
    [SerializeField] private TMP_Text selectionTextB;
    [SerializeField] private Button talkButton;
    [SerializeField] private GameObject talkBox;
    [SerializeField] private Button selectionButtonA;
    [SerializeField] private Button selectionButtonB;
    
    public Image CharacterImage => characterImage;
    public TMP_Text TalkText => talkText;
    public TMP_Text SelectionTextA => selectionTextA;
    public TMP_Text SelectionTextB => selectionTextB;
    public Button TalkButton => talkButton;
    public GameObject TalkBox => talkBox;
    
    private CancellationTokenSource _cts;

    public void Initialize(Sprite image)
    {
        _cts = _cts.Clear();
        characterImage.sprite = image;
        talkBox.SetActive(false);
        selectionButtonA.gameObject.SetActive(false);
        selectionButtonB.gameObject.SetActive(false);
    }

    public async UniTask<SelectionType> Question(string alphaText, string betaText, CancellationToken token)
    {
        _cts = _cts.Reset();
        var linkedToken = _cts.LinkedToken(token);
        
        selectionTextA.text = alphaText;
        selectionTextB.text = betaText;
        selectionButtonA.gameObject.SetActive(true);
        selectionButtonB.gameObject.SetActive(true);
        var alpha = selectionButtonA.onClick.OnInvokeAsync(linkedToken);
        var beta = selectionButtonB.onClick.OnInvokeAsync(linkedToken);
        try
        {
            var index = await UniTask.WhenAny(alpha, beta);
            return index == 0 ? SelectionType.Alpha : SelectionType.Beta;
        }
        finally
        {
            selectionTextA.text = "-";
            selectionTextB.text = "-";
            selectionButtonA.gameObject.SetActive(false);
            selectionButtonB.gameObject.SetActive(false);
        }
    }
}
