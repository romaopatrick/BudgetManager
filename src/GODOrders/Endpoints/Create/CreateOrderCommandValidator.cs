using FluentValidation;
using GODCommon.Notifications;

namespace GODOrders.Endpoints.Create;

public sealed class CreateOrderCommandValidator : Validator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Details).MaximumLength(1000).WithErrorCode(OrderNotifications.InvalidDetailsLength);
        RuleFor(x => x.CustomerNumber).NotNull().WithErrorCode(OrderNotifications.CustomerNumberCannotBeEmpty)
            .GreaterThanOrEqualTo(0).WithErrorCode(OrderNotifications.InvalidCustomerNumber);
        RuleFor(x => x.ProductNumber).NotNull().WithErrorCode(OrderNotifications.ProductNumberCannotBeEmpty)
            .GreaterThanOrEqualTo(0).WithErrorCode(OrderNotifications.InvalidProductNumber);
    }
}