using System.Threading;
using Cysharp.Threading.Tasks;
using Henohenon.Scripts.CoreUnity;
using UnityEngine;
using UnityEngine.UI;

public class TitleHandler
{
    private readonly TitleElements _elements;
    
    public TitleHandler(TitleElements elements)
    {
        _elements = elements;
        
        _elements.LicenseButton.onClick.AddListener(() => _elements.LicensePopup.Show());
    }
    
    public void Dispose()
    {
        _elements.LicenseButton.onClick.RemoveAllListeners();
    }
}
