﻿using System;
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
            var cultureInfo = new CultureInfo("en-US", true);
            result.AppendFormat(cultureInfo, "Statement for {0}\n", invoice.Customer);


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
                result.AppendFormat(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name,
                    Convert.ToDecimal(playAmount / 100), performance.Audience);
                totalAmount += playAmount;
            }

            result.AppendFormat(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result.AppendFormat(cultureInfo, "You earned {0} credits\n", volumeCredits);
            return result.ToString();
        }
    }
}