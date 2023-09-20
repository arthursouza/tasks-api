using FluentValidation.Results;

[Serializable]
public class WorkItemValidationException : Exception
{
    public WorkItemValidationException()
    {
    }

    public WorkItemValidationException(string message) : base(message)
    {
    }

    public WorkItemValidationException(List<ValidationFailure> errors) : base(string.Join(Environment.NewLine, errors))
    {
    }

    public WorkItemValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}