using System.Runtime.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace BaseApp.Infrastructure.Errors.Database;

public class DuplicateEntryException : ValidationException
{
    public DuplicateEntryException(string message) : base(message)
    {
    }

    public DuplicateEntryException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
    {
    }

    public DuplicateEntryException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
    {
    }

    public DuplicateEntryException(IEnumerable<ValidationFailure> errors) : base(errors)
    {
    }

    public DuplicateEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}