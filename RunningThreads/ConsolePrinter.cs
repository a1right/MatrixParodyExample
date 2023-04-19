using RunningThreads.Interfaces;
using RunningThreads.Models;

namespace RunningThreads;

public class ConsolePrinter : IPrinter
{
    public void Print(OutputRequest request)
    {
        Console.SetCursorPosition(request.Column, request.Line);
        Console.Write(request.Symbol);
    }
}