using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameObjectShowHide: MonoBehaviour, IShowHide
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}