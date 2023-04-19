using RunningThreads.Interfaces;
using RunningThreads.Models;

namespace RunningThreads;

public class ConsolePrinter : IPrinter
{
    public void Print(OutputRequest request)
    {
        Console.ForegroundColor = request.ConsoleColor;
        Console.SetCursorPosition(request.Column, request.Line);
        Console.Write(request.Symbol);
    }
}