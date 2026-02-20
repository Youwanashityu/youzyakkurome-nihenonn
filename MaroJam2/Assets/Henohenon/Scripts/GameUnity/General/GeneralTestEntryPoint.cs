using Henohenon.Scripts.GameUnity.General;
using UnityEngine;

public class GeneralTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private GeneralElements elements;

    private GeneralHandler _handler;

    private void Awake()
    {
        _handler = new GeneralHandler(elements);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
    }
}
