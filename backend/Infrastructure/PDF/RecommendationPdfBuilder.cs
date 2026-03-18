using Application.Abstract;
using Application.CQRs.PDF.Dto;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

public class RecommendationPdfBuilder(IEnumResolver enumResolver) : IPdfBuilder<RecommendationPdfModel>
{
    public byte[] BuildPdf(RecommendationPdfModel model)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);

                // Header
                page.Header()
                    .Text("Отчёт по рекомендациям")
                    .FontSize(20)
                    .Bold()
                    .FontColor(Colors.Blue.Medium);

                // Attributes Section
                page.Content()
                    .Column(col =>
                    {
                        col.Spacing(5);

                        col.Item().Text("Атрибуты сессии").Bold().FontSize(16);

                        col.Item().Text($"Skill Level: {enumResolver.Resolve(model.Attributes.SkillLevel)}");
                        col.Item().Text($"Data Volume: {enumResolver.Resolve(model.Attributes.DataVolume)}");
                        col.Item().Text($"Budget: {enumResolver.Resolve(model.Attributes.Budget)}");
                        col.Item().Text($"Team Size: {enumResolver.Resolve(model.Attributes.TeamSize)}");
                        col.Item().Text($"Technical Background: {enumResolver.Resolve(model.Attributes.TechnicalBackground)}");
                        col.Item().Text($"User Tasks: {string.Join(", ", model.Attributes.UserTasks.Select(x => enumResolver.Resolve(x)).ToList())}");
                        col.Item().Text($"Integrations: {string.Join(", ", model.Attributes.Integrations.Select(x => enumResolver.Resolve(x)).ToList())}");

                        col.Item()
                            .PaddingTop(10)
                            .Text("Рекомендации")
                            .Bold()
                            .FontSize(16);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // ToolName
                                columns.RelativeColumn(5); // ReasoningSummary
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Инструмент").SemiBold();
                                header.Cell().Text("Резюме причин").SemiBold();
                            });

                            foreach (var rec in model.Results)
                            {
                                table.Cell().Element(cell => cell.Text(rec.ToolName ?? ""));
                                table.Cell().Element(cell => cell.Text(rec.ReasoningSummary ?? ""));

                                // Добавляем пустую строку через новый Row с пустыми ячейками
                                table.Cell().Element(cell => cell.Height(5)); // отступ в 5 пунктов
                                table.Cell().Element(cell => cell.Height(5));
                            }
                        });
                    });
            });
        });

        using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        return stream.ToArray();
    }
}