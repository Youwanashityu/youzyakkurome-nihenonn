using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class ViewHandler: IDisposable
{
    private readonly Button _startButton;
    private readonly Button _backTitleButton;
    private readonly Button _backHomeButton;
    private readonly Button _gatyaButton;
    private readonly ViewController _viewController;
    
    public ViewHandler(TitleElements titleElements, HomeElements homeElements, GatyaElements gatyaElements)
    {
        _startButton = titleElements.StartButton;
        _backTitleButton = homeElements.BackToTitle;
        _backHomeButton = gatyaElements.BackButton;
        _gatyaButton = homeElements.GatyaButton;
        _viewController = new ViewController(titleElements.View, homeElements.View, gatyaElements.View);
        
        _viewController.Show(View.Title);
        
        _startButton.onClick.AddListener(ToHome);
        _backTitleButton.onClick.AddListener(ToTitle);
        _backHomeButton.onClick.AddListener(ToHome);
        _gatyaButton.onClick.AddListener(ToGatya);
    }

    public void Dispose()
    {
        _startButton.onClick.RemoveAllListeners();
        _startButton.onClick.RemoveAllListeners();
        _backHomeButton.onClick.RemoveAllListeners();
        _gatyaButton.onClick.RemoveAllListeners();
    }

    private void ToTitle()
    {
        _viewController.Show(View.Title);
    }

    private void ToHome()
    {
        _viewController.Show(View.Home);
    }

    private void ToGatya()
    {
        _viewController.Show(View.Gatya);
    }
}
