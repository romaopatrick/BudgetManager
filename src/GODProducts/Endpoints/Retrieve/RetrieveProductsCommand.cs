using GODCommon.Enums;
using GODCommon.Results;
using GODCommon.Results.Paging;
using MediatR;

namespace GODProducts.Endpoints.Retrieve;

public sealed class RetrieveProductsCommand : Paged, IRequest<IResult<Paged<Product>>>
{
    public long? CustomerNumber { get; init; }
    public string? Name { get; init; }
    public string? Brand { get; init; }
    public string? SerialNumber { get; init; }
    public Guarantee? Guarantee { get; init; }
    public DateTime? EntryDateMin { get; init; }
    public DateTime? EntryDateMax { get; init; }
    public DateTime? ExitDateMin { get; init; }
    public DateTime? ExitDateMax { get; init; }
}