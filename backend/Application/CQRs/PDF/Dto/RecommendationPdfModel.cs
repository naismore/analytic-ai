using Domain.Entities;

namespace Application.CQRs.PDF.Dto;

public sealed record RecommendationPdfModel(RecommendationAttributes Attributes, List<RecommendationResult> Results);