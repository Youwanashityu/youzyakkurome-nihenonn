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
            button.onClick.AddListener(Hide);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}