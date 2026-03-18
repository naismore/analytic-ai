using System.Text.RegularExpressions;
using Application.Abstract;
using Domain.Entities;

namespace Infrastructure.Parser;

public class RecommendationParser : IRecommendationParser
{
    public List<RecommendationResult> Parse(string text)
    {
        var list = new List<RecommendationResult>();

        // Основной инструмент
        var mainToolMatch = Regex.Match(
            text,
            @"### Основной инструмент\s*(.+?)\s*Причины:\s*(.+?)\s*### Альтернативы",
            RegexOptions.Singleline
        );

        if (mainToolMatch.Success)
        {
            string toolName = mainToolMatch.Groups[1].Value.Trim();
            string reasonsRaw = mainToolMatch.Groups[2].Value.Trim();

            string reasoningSummary = string.Join("; ",
                Regex.Matches(reasonsRaw, @"- (.+)")
                     .Select(m => m.Groups[1].Value.Trim())
            );

            list.Add(new RecommendationResult
            {
                ToolName = toolName,
                ResultType = Domain.Entities.ResultType.Main,
                ReasoningSummary = reasoningSummary,
            });
        }

        // Альтернативы
        var altSectionMatch = Regex.Match(
            text,
            @"### Альтернативы\s*(.+?)\s*### Итог",
            RegexOptions.Singleline
        );

        if (altSectionMatch.Success)
        {
            string altText = altSectionMatch.Groups[1].Value.Trim();

            var altMatches = Regex.Matches(altText, @"\d+\.\s*(.+?)\s*—\s*(.+)");
            foreach (Match match in altMatches)
            {
                list.Add(new RecommendationResult
                {
                    ToolName = match.Groups[1].Value.Trim(),
                    ResultType = Domain.Entities.ResultType.Alternative,
                    ReasoningSummary = match.Groups[2].Value.Trim()
                });
            }
        }

        return list;
    }
}