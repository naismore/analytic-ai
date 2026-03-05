using MediatR;

namespace Application.Abstract;

public interface ICommand<out TResponse> : IRequest<TResponse>;
