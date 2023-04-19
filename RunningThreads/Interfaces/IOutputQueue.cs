using RunningThreads.Models;

namespace RunningThreads.Interfaces;

public interface IOutputQueue
{
    void QueueOutput(OutputRequest request);
}