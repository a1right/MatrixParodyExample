using RunningThreads.Interfaces;
using RunningThreads.Models;
using System.Collections.Concurrent;

namespace RunningThreads;

public class Presenter : IPresenter, IOutputQueue
{
    private ConcurrentQueue<OutputRequest> _outputQueue = new();
    private IPrinter[] _printers;

    public Presenter(IPrinter printer)
    {
        _printers = new IPrinter[] { printer };
    }
    public Presenter(IPrinter[] printers)
    {
        _printers = printers;
    }
    public Presenter(List<IPrinter> presenters)
    {
        _printers = presenters.ToArray();
    }

    public async Task StartListening(CancellationToken token)
    {
        while (true)
        {
            if (token.IsCancellationRequested)
                break;

            await Task.Yield();

            Show(token);
        }
    }
    public async Task Show(CancellationToken token)
    {
        if (_outputQueue.Count <= 0) return;
        if (_printers.Length <= 0) return;

        var output = await WaitNextAsync(token);

        if (output is null)
            return;

        foreach (var presenter in _printers)
        {
            presenter.Print(output);
        }
    }
    public void QueueOutput(OutputRequest request)
    {
        _outputQueue.Enqueue(request);
    }

    private async Task<OutputRequest> WaitNextAsync(CancellationToken token) => await Task.Run(() => WaitNext(token));
    private async Task<OutputRequest> WaitNext(CancellationToken token)
    {
        while (true)
        {
            if (token.IsCancellationRequested)
                break;

            if (_outputQueue.TryDequeue(out OutputRequest request))
                return request;

            await Task.Yield();
        }

        return null;
    }
}