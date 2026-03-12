using Application.Abstract;
using Application.CQRs.Admin.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRs.Admin.Handlers
{
    public class CreateAnalyticToolCommandHandler(IAnalyticsToolRepository analyticsToolRepository) : ICommandHandler<CreateAnalyticToolCommand, Unit>
    {
        public async Task<Unit> Handle(CreateAnalyticToolCommand request, CancellationToken cancellationToken)
        {
            var analyticToolDto = new AnalyticToolDto(
                request.Name,
                (ToolCategory)request.ToolCategory,
                (SkillLevel)request.SkillLevel,
                request.MaxDataVolume
                );

            var analyticTool = AnalyticsTool.Create(analyticToolDto);
            await analyticsToolRepository.InsertAsync(analyticTool);
            await analyticsToolRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
