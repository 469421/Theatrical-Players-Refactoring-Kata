using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata
{
    public static class StatementPrinter
    {
        private static int GetTragedyPlay(int audience)
        {
            if (audience < 30)
            {
                return 40000;
            }

            return 40000 + 1000 * (audience - 30);
        }

        private static int GetComedyPlay(int audience)
        {
            int result = 30000 + 300 * audience;
            if (audience > 20)
            {
                result += 10000 + 500 * (audience - 20);
            }

            return result;
        }

        public static string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = new StringBuilder();
            result.Append($"Statement for {invoice.Customer}\n");
            var cultureInfo = new CultureInfo("en-US");

            foreach (var performance in invoice.Performances)
            {
                if (performance.Audience > 30)
                {
                    volumeCredits += performance.Audience - 30;
                }

                var play = plays[performance.PlayId];
                int playAmount;
                switch (play.Type)
                {
                    case "tragedy":
                        playAmount = GetTragedyPlay(performance.Audience);
                        break;
                    case "comedy":
                        playAmount = GetComedyPlay(performance.Audience);
                        volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);
                        break;
                    default:
                        throw new PlayException("unknown type: " + play.Type);
                }

                // print line for this order
                result.AppendFormat(cultureInfo,
                    $"  {play.Name}: {Convert.ToDecimal(playAmount / 100):C} ({performance.Audience} seats)\n");
                totalAmount += playAmount;
            }

            result.AppendFormat(cultureInfo, $"Amount owed is {Convert.ToDecimal(totalAmount / 100):C}\n");
            result.Append($"You earned {volumeCredits} credits\n");
            return result.ToString();
        }
    }
}