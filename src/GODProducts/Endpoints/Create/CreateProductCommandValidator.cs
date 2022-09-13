using FluentValidation;
using GODCommon.Notifications;

namespace GODProducts.Endpoints.Create;

public sealed class CreateProductCommandValidator : Validator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Brand).NotEmpty().WithErrorCode(ProductNotifications.BrandCannotBeEmpty);
        RuleFor(x => x.Name).NotEmpty().WithErrorCode(ProductNotifications.NameCannotBeEmpty);
        
        RuleFor(x => x.CustomerNumber)
            .GreaterThanOrEqualTo(0).WithErrorCode(ProductNotifications.InvalidCustomerNumber)
            .NotEmpty().WithErrorCode(ProductNotifications.CustomerNumberCannotBeEmpty);
        
        RuleFor(x => x.EntryDate).NotEmpty().WithErrorCode(ProductNotifications.EntryDateCannotBeEmpty);
        RuleFor(x => x.ReportedDefect).NotEmpty().WithErrorCode(ProductNotifications.ReportedDefectCannotBeEmpty);
    }
}