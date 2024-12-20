﻿using System.Text;
using PenPals.Data;

namespace PenPals.Logging;

internal static class GiftLogger
{
    private static readonly int[] COLUMN_WIDTHS = [10, 10, 20, 14, 4, 11];

    public static void LogResults(
        IEnumerable<GiftResult> results,
        string title,
        IMonitor monitor,
        LogLevel level = LogLevel.Info
    )
    {
        var output = new StringBuilder();
        output.AppendLine(title);
        output.AppendBorderLine(COLUMN_WIDTHS, BorderLine.Top);
        output.AppendColumns(COLUMN_WIDTHS, "From", "To", "Gift", "Reaction", "Pts", "Quest");
        output.AppendBorderLine(COLUMN_WIDTHS, BorderLine.Middle);
        foreach (var result in results)
        {
            var giftName = result.Gift.Name;
            if (result.Gift.Quality > 0)
            {
                giftName = "(" + GetQualityChar(result.Gift.Quality) + ") " + giftName;
            }
            output.AppendColumns(
                COLUMN_WIDTHS,
                result.From.Name,
                result.To.Name,
                giftName,
                result.Outcome,
                result.Points,
                result.CompletedQuest?.SafeId() ?? ""
            );
        }
        output.AppendBorderLine(COLUMN_WIDTHS, BorderLine.Bottom);
        monitor.Log(output.ToString(), level);
    }

    private static char GetQualityChar(int quality)
    {
        return quality switch
        {
            1 => 'S',
            2 => 'G',
            4 => 'I',
            _ => '?',
        };
    }
}
