
using R3;

public interface IGatyaController
{
    public Observable<Unit> OnStartGatya { get; }
    public Observable<ItemType> OnGetItem { get; }
    public Observable<ItemTier> OnPickItem { get; }
    public void SetTable(GatyaTable table);
}