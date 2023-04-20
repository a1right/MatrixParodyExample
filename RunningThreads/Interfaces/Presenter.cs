namespace RunningThreads.Interfaces
{
    public interface IPresenter
    {
        Task ShowNext(CancellationToken token);
        Task StartListening(CancellationToken token);
    }
}
