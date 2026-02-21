using R3;

public interface IBadMoneyKeyHandler
{
    public ReadOnlyReactiveProperty<int> KeyAmount { get; }
    public ReadOnlyReactiveProperty<int> BadMoneyAmount { get; }
    public void Purchase(int addKey, int subBadMoney);
}