using MediatR;

namespace AppApi.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}