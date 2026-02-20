using UnityEngine.TextCore.Text;

public class CharacterHandler
{
    private readonly CharacterType _characterType;
    private int _love;
    private int _loveLv;
    
    public CharacterHandler(CharacterType type)
    { 
        _characterType = type;
    }
    
    public CharacterType CharacterType => _characterType;
    public int Love => _love;
    public int LoveLv => _loveLv;
    
    public void AddLove(int value)
    {
        _love += value;
        if (_love < 10)
        {
            _loveLv = 0;
        }
        else if (_love < 20)
        {
            _loveLv = 1;
        }
        else
        {
            _loveLv = 2;
        }
    }
}
