using FluentValidation;
using GODCommon.Extensions;
using GODCommon.Notifications;

namespace GODCustomers.Endpoints.Update;

public sealed class UpdateCustomerCommandValidator : Validator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.City).MaximumLength(300).WithErrorCode(CustomerNotifications.InvalidCityLength);

        RuleFor(x => x.District).MaximumLength(300).WithErrorCode(CustomerNotifications.InvalidDistrictLength);

        RuleFor(x => x.Document).Must(x => x is null || x.IsValidDocument())
            .WithErrorCode(CustomerNotifications.InvalidDocument);

        RuleFor(x => x.Email).EmailAddress().WithErrorCode(CustomerNotifications.InvalidEmail);

        RuleFor(x => x.Number).MaximumLength(10).WithErrorCode(CustomerNotifications.InvalidNumberLength);

        RuleFor(x => x.SecondaryEmail).EmailAddress().WithErrorCode(CustomerNotifications.InvalidSecondaryEmail);
        
        RuleFor(x => x.StateAbbreviation).Length(2)
            .WithErrorCode(CustomerNotifications.InvalidStateAbbreviation);

        RuleFor(x => x.ZipCode).Must(x => x is null || x.ValidZipCode()).WithErrorCode(CustomerNotifications.InvalidZipCode);
    }    
}