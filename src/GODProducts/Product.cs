using GODCommon.Entities;
using GODCommon.Enums;

namespace GODProducts;

public sealed class Product : EntityBase.AsSnapshot
{
    public long CustomerNumber { get; set; }

    public string Brand { get; set; }
    public string Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Diagnostic { get; set; }
    public string ReportedDefect { get; set; }
    public Guarantee Guarantee { get; set; }
    public int? GuaranteeInDays { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string? Notes { get; set; }
}