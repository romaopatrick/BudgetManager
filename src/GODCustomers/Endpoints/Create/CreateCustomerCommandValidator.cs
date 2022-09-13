using FluentValidation;
using GODCommon.Extensions;
using GODCommon.Notifications;

namespace GODCustomers.Endpoints.Create;

public sealed class CreateCustomerCommandValidator : Validator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.City).MaximumLength(300).WithErrorCode(CustomerNotifications.InvalidCityLength)
            .NotEmpty().WithErrorCode(CustomerNotifications.CityCannotBeEmpty);

        RuleFor(x => x.District).MaximumLength(300).WithErrorCode(CustomerNotifications.InvalidDistrictLength)
            .NotEmpty().WithErrorCode(CustomerNotifications.DistrictCannotBeEmpty);

        RuleFor(x => x.Document).NotEmpty().WithErrorCode(CustomerNotifications.DocumentCannotBeEmpty)
            .Must(x => x.IsValidDocument()).WithErrorCode(CustomerNotifications.InvalidDocument);

        RuleFor(x => x.Email).NotEmpty().WithErrorCode(CustomerNotifications.EmailCannotBeEmpty)
            .EmailAddress().WithErrorCode(CustomerNotifications.InvalidEmail);

        RuleFor(x => x.Number).NotEmpty().WithErrorCode(CustomerNotifications.NumberCannotBeEmpty)
            .MaximumLength(10).WithErrorCode(CustomerNotifications.InvalidNumberLength);

        RuleFor(x => x.Phone).NotEmpty().WithErrorCode(CustomerNotifications.PhoneCannotBeEmpty);
        
        RuleFor(x => x.Street).NotEmpty().WithErrorCode(CustomerNotifications.StreetCannotBeEmpty);

        RuleFor(x => x.FullName).NotEmpty().WithErrorCode(CustomerNotifications.FullNameCannotBeEmpty);

        RuleFor(x => x.SecondaryEmail).EmailAddress().WithErrorCode(CustomerNotifications.InvalidSecondaryEmail);
        
        RuleFor(x => x.StateAbbreviation).NotEmpty().WithErrorCode(CustomerNotifications.StateAbbreviationCannotBeEmpty)
            .Length(2).WithErrorCode(CustomerNotifications.InvalidStateAbbreviation);

        RuleFor(x => x.ZipCode).Must(x => x.ValidZipCode()).WithErrorCode(CustomerNotifications.InvalidZipCode);
    }
}