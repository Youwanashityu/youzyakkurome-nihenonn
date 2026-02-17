using System.Threading;

public static class CtsUtils
{
    public static CancellationTokenSource Clean(this CancellationTokenSource cts)
    {
        if (cts == null) return null;
        cts.Cancel();
        cts.Dispose();
        return null;
    }

    public static CancellationTokenSource Reset(this CancellationTokenSource cts)
    {
        cts.Clean();
        cts = new CancellationTokenSource();
        return cts;
    }
    
    public static CancellationToken LinkedToken(
        this CancellationTokenSource cts,
        CancellationToken other)
    {

        if (cts == null) return other;
        if (!other.CanBeCanceled) return cts.Token;

        return CancellationTokenSource.CreateLinkedTokenSource(cts.Token, other).Token;
    }

}
