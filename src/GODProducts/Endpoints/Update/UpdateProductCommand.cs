using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODProducts.Endpoints.Update;

public sealed class UpdateProductCommand : IRequest<IResult<EventResult<Product>>>
{
    public Guid ProductId { get; init; }
    public string? Brand { get; init; }
    public string? Name { get; init; }
    public string? SerialNumber { get; init; }
    public string? Diagnostic { get; init; }
    public string? ReportedDefect { get; init; }
    public Guarantee? Guarantee { get; init; }
    public int? GuaranteeInDays { get; init; }
    public DateTime? EntryDate { get; init; }
    public DateTime? ExitDate { get; init; }
    public string? Notes { get; init; }


    public void UpdateEntity(Product product)
    {
        if (!string.IsNullOrEmpty(Brand)) product.Brand = Brand;
        if (!string.IsNullOrEmpty(Name)) product.Name = Name;
        if (!string.IsNullOrEmpty(SerialNumber)) product.SerialNumber = SerialNumber;
        if (!string.IsNullOrEmpty(ReportedDefect)) product.ReportedDefect = ReportedDefect;
        if (Guarantee.HasValue) product.Guarantee = Guarantee!.Value;
        if (!string.IsNullOrEmpty(Notes)) product.Notes = Notes;
        if (!string.IsNullOrEmpty(Diagnostic)) product.Diagnostic = Diagnostic;
        if (GuaranteeInDays.HasValue) product.GuaranteeInDays = GuaranteeInDays;
        if (EntryDate.HasValue) product.EntryDate = EntryDate.Value;
        if (ExitDate.HasValue) product.ExitDate = ExitDate;
    }
}