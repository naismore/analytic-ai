using Application.Abstract;
using MediatR;

namespace Application.CQRs.Admin.Commands
{
    public sealed record CreateAnalyticToolCommand(string Name, int ToolCategory, int SkillLevel, int MaxDataVolume) : ICommand<Unit>;
}
