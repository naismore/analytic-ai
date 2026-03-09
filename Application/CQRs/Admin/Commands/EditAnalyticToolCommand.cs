using Application.Abstract;
using MediatR;

namespace Application.CQRs.Admin.Commands
{
    public record EditAnalyticToolCommand(int Id, string Name, int ToolCategory, int SkillLevel, int MaxDataVolume) : ICommand<Unit>;
}
