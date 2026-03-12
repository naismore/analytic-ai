using System.Text.RegularExpressions;
using Application.Abstract;
using Domain.Entities;

namespace Infrastructure.Parser;

public class RecommendationParser : IRecommendationParser
{
    public List<RecommendationResult> Parse(string text)
    {
        var results = new List<RecommendationResult>();

        // --- MAIN TOOL ---
        var mainMatch = Regex.Match(
            text,
            @"### Основной инструмент\s*(.*?)\s*Причины:\s*((?:- .*\n?)*)",
            RegexOptions.Singleline);

        if (mainMatch.Success)
        {
            var toolName = mainMatch.Groups[1].Value.Trim();

            var reasons = Regex.Matches(mainMatch.Groups[2].Value, @"- (.*)")
                .Select(m => m.Groups[1].Value.Trim());

            var reasoning = string.Join(" ", reasons);

            results.Add(new RecommendationResult
            {
                Id = Guid.NewGuid(),
                ToolName = toolName,
                ResultType = ResultType.Main,
                ReasoningSummary = reasoning
            });
        }

        // --- ALTERNATIVES ---
        var altMatches = Regex.Matches(
            text,
            @"\d+\.\s*(.*?)\s*—\s*(.*)");

        foreach (Match match in altMatches)
        {
            results.Add(new RecommendationResult
            {
                Id = Guid.NewGuid(),
                ToolName = match.Groups[1].Value.Trim(),
                ResultType = ResultType.Alternative,
            });
        }

        return results;
    }
}