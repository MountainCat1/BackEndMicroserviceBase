using MediatR;

namespace BaseApp.Domain.Abstractions;

public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
{
}