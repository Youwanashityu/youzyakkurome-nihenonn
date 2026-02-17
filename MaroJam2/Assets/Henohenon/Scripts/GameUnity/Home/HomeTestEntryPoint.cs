using UnityEngine;

public class HomeTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private HomeElements elements;

    private HomeHandler _handler;

    private void Awake()
    {
        _handler = new HomeHandler(elements);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
    }
}
