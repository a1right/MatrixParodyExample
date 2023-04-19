using RunningThreads.Models;

namespace RunningThreads.Interfaces;

public interface IPrinter
{
    void Print(OutputRequest request);
}