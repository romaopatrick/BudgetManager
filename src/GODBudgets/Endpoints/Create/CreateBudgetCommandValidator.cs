using FluentValidation;
using GODCommon.Notifications;

namespace GODBudgets.Endpoints.Create;

public sealed class CreateBudgetCommandValidator : Validator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        RuleFor(x => x.OrderNumber).NotNull().WithErrorCode(BudgetNotifications.OrderNumberCannotBeEmpty)
            .GreaterThanOrEqualTo(0).WithMessage(BudgetNotifications.InvalidOrderNumber);

        RuleFor(x => x.Details).MaximumLength(500).WithErrorCode(BudgetNotifications.InvalidDetailsLength);
        
        RuleFor(x => x.ProposedValue).NotNull().WithErrorCode(BudgetNotifications.ProposedValueCannotBeEmpty)
            .GreaterThanOrEqualTo(0).WithErrorCode(BudgetNotifications.InvalidProposedValue);

        RuleFor(x => x.WorkingDaysToComplete).NotNull()
            .WithErrorCode(BudgetNotifications.WorkingDaysToCompleteCannotBeEmpty)
            .GreaterThanOrEqualTo(0)
            .WithErrorCode(BudgetNotifications.InvalidWorkingDaysToComplete);
    }
}