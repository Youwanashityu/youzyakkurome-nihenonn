using System.Threading;
using Cysharp.Threading.Tasks;
using Henohenon.Scripts.CoreUnity;
using Henohenon.Scripts.GameUnity.General;
using UnityEngine;
using UnityEngine.UI;

public class GeneralHandler
{
    private readonly GeneralElements _elements;

    public GeneralHandler(GeneralElements elements)
    {
        _elements = elements;
    }

    public void Dispose()
    {
    }
}
