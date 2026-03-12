using MediatR;

namespace Application.Abstract;

public interface IQuery<out TResponse> : IRequest<TResponse>;
