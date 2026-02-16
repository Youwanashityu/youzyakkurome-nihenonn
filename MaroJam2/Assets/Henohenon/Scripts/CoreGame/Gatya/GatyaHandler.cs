using System;

public class GatyaHandler : IDisposable
{
    private readonly GatyaElements _elements;

    public GatyaHandler(GatyaElements elements)
    {
        _elements = elements;
    }

    public void Dispose()
    {
        
    }
}
