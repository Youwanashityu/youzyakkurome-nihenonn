using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Henohenon.Scripts.CoreUnity
{
    public class PopupElement: MonoBehaviour, IShowHide
    {
        [SerializeField] private Button button;
        
        private void Awake()
        {
            gameObject.SetActive(false);
            button.onClick.AddListener(() => Hide(CancellationToken.None).Forget());
        }
        
        public async UniTask Show(CancellationToken token)
        {
            gameObject.SetActive(true);
        }
        
        public async UniTask Hide(CancellationToken token)
        {
            gameObject.SetActive(false);
        }
    }
}