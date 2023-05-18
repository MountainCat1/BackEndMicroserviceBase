using FluentValidation;
using MediatR;

namespace BaseApp.Api.MediaRBehaviors;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (!typeof(TResponse).IsGenericType) 
            return await next();
        
        if (!_validators.Any()) 
            return await next();
        
        var context = new ValidationContext<TRequest>(request);
        var validationResults =
            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(x => x.Errors)
            .ToArray();
        
        // IF no Validation Errors continue to next Behaviour
        if (!failures.Any()) return await next();

        var exception = new ValidationException(failures);
        
        // Else exit to calling method  error response with errors    
        return (Activator.CreateInstance(typeof(TResponse), exception) is TResponse 
            ? (TResponse)Activator.CreateInstance(typeof(TResponse), exception)! 
            : default)!;
    }
}