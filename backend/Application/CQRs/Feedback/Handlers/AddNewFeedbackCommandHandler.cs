using Application.Abstract;
using Application.CQRs.Feedback.Commands;
using MediatR;

namespace Application.CQRs.Feedback.Handlers;

internal class AddNewFeedbackCommandHandler : ICommandHandler<AddNewFeedbackCommand, Unit>
{
    public Task<Unit> Handle(AddNewFeedbackCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
