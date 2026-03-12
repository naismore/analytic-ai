using Application.Abstract;
using MediatR;

namespace Application.CQRs.Feedback.Commands;

public sealed record AddNewFeedbackCommand(int SessionId, int Mark) : ICommand<Unit>;
