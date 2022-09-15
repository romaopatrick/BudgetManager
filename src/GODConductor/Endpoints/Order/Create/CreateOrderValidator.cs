using GODOrders.Endpoints.Create;

namespace GODConductor.Endpoints.Order.Create;

public sealed class CreateOrderValidator : Validator<CreateOrderCommand>
{
    public CreateOrderValidator() 
        => RuleFor(x => x).SetValidator(new CreateOrderCommandValidator());
}