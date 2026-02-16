using System;

public class HomeHandler : IDisposable
{
    private readonly HomeElements _elements;

    public HomeHandler(HomeElements elements)
    {
        _elements = elements;
    }

    public void Dispose()
    {
    }
}
