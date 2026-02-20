using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

public enum View
{
    Title,
    Home,
    Gatya
}

public class ViewController
{
    private readonly Dictionary<View, IShowHide> _views;
    private View? _currentView;

    public ViewController(IShowHide titleView, IShowHide homeView, IShowHide gatyaView)
    {
        _views = new Dictionary<View, IShowHide>
        {
            { View.Title, titleView },
            { View.Home, homeView },
            { View.Gatya, gatyaView }
        };
        
        
    }

    public void Show(View view)
    {
        if (_currentView.HasValue)
        {
            _views[_currentView.Value].Hide();
        }

        _views[view].Show();
        _currentView = view;
    }
}