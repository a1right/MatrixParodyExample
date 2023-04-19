using RunningThreads;
using RunningThreads.Models;

namespace ThreadsExample
{
    internal class Program
    {
        static void Print(Presenter presenter, int column, int delay)
        {
            for (int i = 0; i < 1000000; i++)
            {
                presenter.QueueOutput(new OutputRequest()
                {
                    Column = column * 3,
                    Line = i % 30,
                    Symbol = (char)Random.Shared.Next(1, 256),
                });
                Thread.Sleep(delay);
            }
        }
        static async Task Main(string[] args)
        {
            Console.WriteLine("Сколько секунд залипаем? (Примерно :P)");
            var duration = int.Parse(Console.ReadLine());
            Console.Clear();

            var printer = new ConsolePrinter();
            var presenter = new Presenter(printer);
            var cts = new CancellationTokenSource();

            presenter.StartListening(cts.Token);
            presenter.Show(cts.Token);

            var timer = Task.WhenAny(Task.Delay(TimeSpan.FromSeconds(duration)));

            for (int i = 0; i < 25; i++)
            {
                var delay = Random.Shared.Next(50, 800);
                var column = i;
                var thread = new Thread(() => Print(presenter, column, delay)) { IsBackground = true };
                thread.Start();
                await Task.Delay(50);
            }

            await timer;
            cts.Cancel(false);
        }
    }
}