using System.ComponentModel.DataAnnotations;
using GODCommon.Entities;
using GODCommon.Enums;

namespace GODCustomers;

public sealed class Customer : EntityBase.AsSnapshot
{
    public string Document { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; }
    public string? SecondaryEmail { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    [MaxLength(2)] public string StateAbbreviation { get; set; }
    public CustomerStatus Status { get; set; }
}