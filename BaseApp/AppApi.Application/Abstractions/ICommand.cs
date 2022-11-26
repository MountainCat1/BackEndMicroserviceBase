using MediatR;

namespace AppApi.Abstractions;

public interface ICommand<TResult> : IRequest<TResult>
{
}

public interface ICommand : ICommand<Unit>
{
}