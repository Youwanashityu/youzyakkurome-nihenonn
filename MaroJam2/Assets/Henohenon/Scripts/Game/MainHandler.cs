using System;

public class MainHandler: IDisposable
{
    private readonly ViewHandler _viewHandler;
    
    public MainHandler(TitleElements titleElements, HomeElements homeElements, GatyaElements gatyaElements)
    {
        _viewHandler = new ViewHandler(titleElements, homeElements, gatyaElements);
    }

	public void Dispose(){
		_viewHandler.Dispose();
	}
}