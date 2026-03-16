namespace ArthurKnight.Core
{
    public interface ISupportLookup<TKey>
    {
        TKey LookupKey { get; }
    }

    public interface ISupportLookup<TKey, TData>
    {
        TKey LookupKey { get; }
        TData Data { get; }
    }

    public interface ISupportGroupLookup<TGroup>
    {
        TGroup LookupGroupKey { get; }
    }
}