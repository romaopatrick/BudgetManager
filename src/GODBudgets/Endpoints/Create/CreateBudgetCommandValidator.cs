using FastEndpoints;
using FluentValidation;
using GODCommon.Notifications;

namespace GODBudgets.Endpoints.Create;

public class CreateBudgetCommandValidator : Validator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        RuleFor(x => x.OrderNumber).NotNull().WithErrorCode(BudgetNotifications.ORDER_NUMBER_CANNOT_BE_EMPTY)
            .GreaterThanOrEqualTo(0).WithMessage(BudgetNotifications.INVALID_ORDER_NUMBER);

        RuleFor(x => x.Details).MaximumLength(500).WithErrorCode(BudgetNotifications.INVALID_DETAILS_LENGTH);
        
        RuleFor(x => x.ProposedValue).NotNull().WithErrorCode(BudgetNotifications.PROPOSED_VALUE_CANNOT_BE_EMPTY)
            .GreaterThanOrEqualTo(0).WithErrorCode(BudgetNotifications.INVALID_PROPOSED_VALUE);

        RuleFor(x => x.ExpectedCompletionDate).NotNull()
            .WithErrorCode(BudgetNotifications.EXPECTED_COMPLETION_DATE_CANNOT_BE_EMPTY)
            .GreaterThanOrEqualTo(DateTime.Now.Date)
            .WithErrorCode(BudgetNotifications.INVALID_EXPECTED_COMPLETION_DATE);
    }
}