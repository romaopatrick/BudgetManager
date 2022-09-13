namespace GODCommon.Results.Paging;

public class Paged<TResult> : Paged
{
    public long Count { get; }
    public IEnumerable<TResult> Results { get; }

    public Paged(IEnumerable<TResult> results, long skip, long range, long count)
    {
        Results = results;
        Page = skip is 0 ? 1 : skip / range;
        Range = range;
        Count = count;
    }
}
public class Paged
{
    public long? Skip => Page * Range;
    public long? Page { get; init; } = 0;
    public long? Range { get; init; } = 10;
    public string? KeyToOrder { get; init; } = "CreatedAt";
    public bool? Desc { get; init; } = true;
}