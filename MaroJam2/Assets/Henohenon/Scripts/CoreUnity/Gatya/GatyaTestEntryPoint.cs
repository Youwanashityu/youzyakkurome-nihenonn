using UnityEngine;

public class GatyaTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private GatyaElements elements;

    private GatyaHandler _handler;

    private void Awake()
    {
        _handler = new GatyaHandler(elements);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
    }
}
