namespace RunningThreads.Models;
public record OutputRequest
{
    public int Line { get; init; }
    public int Column { get; init; }
    public char Symbol { get; init; }
    public ConsoleColor ConsoleColor { get; set; }
}
