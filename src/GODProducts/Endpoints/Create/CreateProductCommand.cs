using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODProducts.Endpoints.Create;

public sealed class CreateProductCommand : IRequest<IResult<EventResult<Product>>>
{
    public long CustomerNumber { get; init; }
    public string Brand { get; init; }
    public string Name { get; init; }
    public string? SerialNumber { get; init; }
    public string? Diagnostic { get; init; }
    public string ReportedDefect { get; init; }
    public Guarantee Guarantee { get; init; }
    public int? GuaranteeInDays { get; init; }
    public DateTime EntryDate { get; init; }
    public DateTime? ExitDate { get; init; }
    public string? Notes { get; init; }

    public Product ToEntity() => new()
    {
        Brand = Brand,
        Diagnostic = Diagnostic,
        Guarantee = Guarantee,
        Notes = Notes,
        Name = Name,
        CustomerNumber = CustomerNumber,
        EntryDate = EntryDate,
        ExitDate = ExitDate,
        ReportedDefect = ReportedDefect,
        GuaranteeInDays = GuaranteeInDays,
        SerialNumber = SerialNumber,
    };
}