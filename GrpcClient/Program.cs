using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Only for testing while developing -> Use GrpcClientUi!

            var stopwatchHelper = new StopwatchClientHelper();
            stopwatchHelper.Start();

            Thread.Sleep(5000);

            var isRunning = await stopwatchHelper.IsRunning();

            Thread.Sleep(3000);

            var time = await stopwatchHelper.GetTimeInMS();

            Thread.Sleep(2000);

            stopwatchHelper.Stop();

            Console.WriteLine("Press any Key to exit!");
            Console.ReadLine();
        }
    }
}
