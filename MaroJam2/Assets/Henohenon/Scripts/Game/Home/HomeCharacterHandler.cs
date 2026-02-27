using System;
using R3;

public class HomeCharacterHandler: IDisposable
{
    private readonly CompositeDisposable _disposable = new();
    
    public HomeCharacterHandler(HomeHandler homeHandler, CharactersManager charaManager)
    {
        charaManager.OnCharaChange.Subscribe(type => homeHandler.Initialize(charaManager.Characters[type])).AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}