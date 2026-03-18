using Application.Abstract;
using Application.CQRs.PDF.Dto;
using Application.CQRs.PDF.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRs.PDF.Handlers;

public class ExportRecommendationPdfQueryHandler(
    IRecommendationResultRepository resultRepository, 
    IPdfBuilder<RecommendationPdfModel> pdfBuilder,
    IRecommendationAttributesRepository attributesRepository)
    : IRequestHandler<ExportRecommendationPdfQuery, byte[]>
{
    public async Task<byte[]> Handle(ExportRecommendationPdfQuery request, CancellationToken cancellationToken)
    {
        // Получаем рекомендации из репозитория
        var recommendations = (await resultRepository.GetBySessionIdAsync(request.SessionId))
                              .ToList();

        var attributesId = attributesRepository.GetBySessionIds(new[] { request.SessionId }).FirstOrDefault();

        if (!recommendations.Any() || attributesId == Guid.Empty)
            throw new Exception("Ошибка при экспорте в PDF: Не найдены данные!");

        var attributesList = await attributesRepository.GetByIdsAsync(new[] { attributesId });
        var attributes = attributesList.FirstOrDefault();

        // Создаем PDF документ
        var report = pdfBuilder.BuildPdf(new RecommendationPdfModel(attributes, recommendations));

        return report;
    }
}
