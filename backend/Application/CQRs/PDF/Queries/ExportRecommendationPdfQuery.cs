using MediatR;

namespace Application.CQRs.PDF.Queries;

public sealed record ExportRecommendationPdfQuery(Guid SessionId) : IRequest<byte[]>;
