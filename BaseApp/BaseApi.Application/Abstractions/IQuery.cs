using MediatR;

namespace BaseApi.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}