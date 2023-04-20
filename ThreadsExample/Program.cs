using RunningThreads;
using RunningThreads.Models;

namespace ThreadsExample
{
    internal class Program
    {
        static async Task Print(Presenter presenter, int column, int delay)
        {
            Console.CursorVisible = false;
            for (int i = 0; i < 40000; i++)
            {
                var iteration = i / 40;
                var color = ColorSwitch(iteration);
                presenter.QueueOutput(new OutputRequest()
                {
                    Column = column * 3,
                    Line = i % 40,
                    Symbol = color == ConsoleColor.DarkCyan ? ' ' : (char)Random.Shared.Next(1, 256),
                    ConsoleColor = color
                });
                Thread.Sleep(delay);
            }
        }

        static ConsoleColor ColorSwitch(int iteration) => (iteration % 3 == 0, iteration % 2 == 0) switch
        {
            (true,true) => ConsoleColor.DarkCyan,
            (true, _) => ConsoleColor.Green,
            (_, true) => ConsoleColor.Green,
            _ => ConsoleColor.DarkCyan,
        };
        static async Task Main(string[] args)
        {
            Console.WriteLine("Сколько секунд залипаем? (Примерно :P)");
            var duration = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.CursorVisible = false;

            var printer = new ConsolePrinter();
            var presenter = new Presenter(printer);
            var cts = new CancellationTokenSource();

            presenter.StartListening(cts.Token);

            var timer = Task.WhenAny(Task.Delay(TimeSpan.FromSeconds(duration)));

            for (int i = 0; i < 30; i++)
            {
                var delay = Random.Shared.Next(30, 70);
                var column = i;
                var thread = new Thread(() => Print(presenter, column, delay)) { IsBackground = true };
                thread.Start();
            }

            await timer;
            cts.Cancel(false);
        }
    }
}