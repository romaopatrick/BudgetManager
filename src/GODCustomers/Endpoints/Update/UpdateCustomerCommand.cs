using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODCustomers.Endpoints.Update;

public sealed class UpdateCustomerCommand : IRequest<IResult<EventResult<Customer>>>
{
    public Guid CustomerId { get; init; }
    public string? FullName { get; init; }
    public string? Document { get; init; }
    public string? Phone { get; init; }
    public string? SecondaryPhone { get; init; }
    public string? Email { get; init; }
    public string? SecondaryEmail { get; init; }
    public string? ZipCode { get; init; }
    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? City { get; init; }
    public string? District { get; init; }
    public string? StateAbbreviation { get; init; }

    public void UpdateEntity(Customer c)
    {
        if (!string.IsNullOrWhiteSpace(FullName)) c.FullName = FullName;
        if (!string.IsNullOrWhiteSpace(Document)) c.Document = Document;
        if (!string.IsNullOrWhiteSpace(Phone)) c.Phone = Phone;
        if (!string.IsNullOrWhiteSpace(SecondaryPhone)) c.SecondaryPhone = SecondaryPhone;
        if (!string.IsNullOrWhiteSpace(Email)) c.Email = Email;
        if (!string.IsNullOrWhiteSpace(SecondaryEmail)) c.SecondaryEmail = SecondaryEmail;
        if (!string.IsNullOrWhiteSpace(ZipCode)) c.ZipCode = ZipCode;
        if (!string.IsNullOrWhiteSpace(Street)) c.Street = Street;
        if (!string.IsNullOrWhiteSpace(Number)) c.Number = Number;
        if (!string.IsNullOrWhiteSpace(City)) c.City = City;
        if (!string.IsNullOrWhiteSpace(District)) c.District = District;
        if (!string.IsNullOrWhiteSpace(StateAbbreviation)) c.StateAbbreviation= StateAbbreviation;
    }
}