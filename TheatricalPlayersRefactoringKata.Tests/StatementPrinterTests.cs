using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace TheatricalPlayersRefactoringKata.Tests
{
    [TestFixture]
    public class StatementPrinterTests
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Test_statement_plain_text_example()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", "tragedy") },
                { "as-like", new Play("As You Like It", "comedy") },
                { "othello", new Play("Othello", "tragedy") }
            };

            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40)
            });

            var result = StatementPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Test_statement_with_new_play_types()
        {
            var plays = new Dictionary<string, Play>
            {
                { "henry-v", new Play("Henry V", "history") },
                { "as-like", new Play("As You Like It", "pastoral") }
            };

            var invoice = new Invoice("BigCoII", new List<Performance>
            {
                new("henry-v", 53),
                new("as-like", 55)
            });

            Assert.Throws<PlayException>(() => StatementPrinter.Print(invoice, plays));
        }
    }
}