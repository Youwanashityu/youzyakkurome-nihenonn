using R3;

public interface IAkkaKeyHandler
{
    public ReadOnlyReactiveProperty<int> KeyAmount { get; }
    public ReadOnlyReactiveProperty<int> AkkaAmount { get; }
    public void Purchase(int addKey, int subAkka);
    public void UseKey(int numb);
}