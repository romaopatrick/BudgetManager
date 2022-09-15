using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODCustomers.Endpoints.Create;

public sealed class CreateCustomerCommand : IRequest<IResult<EventResult<Customer>>>
{
    public string Document { get; init; } 
    public string FullName { get; init; }
    public string Phone { get; init; }
    public string? SecondaryPhone { get; init; }
    public string Email { get; init; }
    public string? SecondaryEmail { get; init; }
    public string ZipCode { get; init; }
    public string Street { get; init; }
    public string Number { get; init; }
    public string City { get; init; }
    public string District { get; init; }
    public string StateAbbreviation { get; init; }

    public Customer ToEntity() => new()
    {
        City = City.ToUpper(),
        District = District.ToUpper(),
        Document = Document.ToUpper(),
        Email = Email.ToLower(),
        FullName = FullName.ToUpper(),
        Number = Number.ToUpper(),
        Phone = Phone,
        Status = CustomerStatus.Active,
        SecondaryEmail = SecondaryEmail?.ToLower(),
        SecondaryPhone = SecondaryPhone,
        Street = Street.ToUpper(),
        StateAbbreviation = StateAbbreviation.ToUpper(),
        ZipCode = ZipCode.ToUpper()
    };
}