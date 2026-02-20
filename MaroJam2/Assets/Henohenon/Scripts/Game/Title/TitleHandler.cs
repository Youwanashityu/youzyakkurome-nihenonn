
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
