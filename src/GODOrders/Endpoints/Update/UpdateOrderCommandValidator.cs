using FluentValidation;
using GODCommon.Notifications;

namespace GODOrders.Endpoints.Update;

public sealed class UpdateOrderCommandValidator : Validator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator() => RuleFor(x => x.Details).MaximumLength(1000)
        .WithErrorCode(OrderNotifications.InvalidDetailsLength);
}