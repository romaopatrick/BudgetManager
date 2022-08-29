using FluentValidation;
using GODCommon.Notifications;

namespace GODBudgets.Endpoints.Update;

public class UpdateBudgetCommandValidator : Validator<UpdateBudgetCommand>
{
    public UpdateBudgetCommandValidator()
    {
        RuleFor(x => x.Details).MaximumLength(500).WithErrorCode(BudgetNotifications.InvalidDetailsLength);
        
        RuleFor(x => x.ProposedValue).GreaterThanOrEqualTo(0).WithErrorCode(BudgetNotifications.InvalidProposedValue);
        RuleFor(x => x.Status).IsInEnum().WithErrorCode(BudgetNotifications.InvalidStatus);
        RuleFor(x => x.ExpectedCompletionDate)
            .GreaterThanOrEqualTo(DateTime.Now.Date)
            .WithErrorCode(BudgetNotifications.InvalidExpectedCompletionDate);
    }
}