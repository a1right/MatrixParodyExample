namespace RunningThreads.Interfaces
{
    public interface IPresenter
    {
        Task Show(CancellationToken token);
        Task StartListening(CancellationToken token);
    }
}
