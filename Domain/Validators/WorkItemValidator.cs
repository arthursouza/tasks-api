using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class WorkItemValidator : AbstractValidator<WorkItem>
{
    public WorkItemValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(0, 100);
        RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}
