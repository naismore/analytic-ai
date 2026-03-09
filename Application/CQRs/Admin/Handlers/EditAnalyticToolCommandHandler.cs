using Application.Abstract;
using Application.CQRs.Admin.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRs.Admin.Handlers
{
    public class EditAnalyticToolCommandHandler(IAnalyticsToolRepository analyticsToolRepository) : ICommandHandler<EditAnalyticToolCommand, Unit>
    {
        public async Task<Unit> Handle(EditAnalyticToolCommand request, CancellationToken cancellationToken)
        {
            var tool = await analyticsToolRepository.GetByIdAsync(request.Id);

            if (tool == null)
                throw new Exception("Tool not found");

            var analyticsToolDto = new AnalyticToolDto(
                request.Name,
                (ToolCategory)request.ToolCategory,
                (SkillLevel)request.SkillLevel,
                request.MaxDataVolume);

            tool = AnalyticsTool.Edit(tool, analyticsToolDto);
            analyticsToolRepository.Update(tool);
            await analyticsToolRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
