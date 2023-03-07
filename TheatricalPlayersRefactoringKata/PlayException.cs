using System;
using System.Runtime.Serialization;

namespace TheatricalPlayersRefactoringKata;

public class PlayException : Exception
{
    public PlayException(string message) : base(message)
    {
    }
}