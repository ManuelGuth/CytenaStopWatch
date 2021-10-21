using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    public class StopwatchClientHelper
    {
        private GrpcChannel _Channel;
        private Stopwatch.StopwatchClient _StopwatchClient;

        public StopwatchClientHelper(string adress="https://localhost:5001")
        {
            _Channel = GrpcChannel.ForAddress(adress);
            _StopwatchClient = new Stopwatch.StopwatchClient(_Channel);
        }
        /// <summary>
        /// Starts/Continues the time on the server
        /// </summary>
        public async void Start()
        {
            var start = new StartTime();
            Console.WriteLine("Start timing.");
            await _StopwatchClient.StartAsync(start);
        }

        /// <summary>
        /// Stops/Pauses the time on the server
        /// </summary>
        public async void Stop()
        {
            var stop = new StopTime();
            Console.WriteLine("Stoping...");
            await _StopwatchClient.StopAsync(stop);
        }

        /// <summary>
        /// Gets the current State of the server
        /// </summary>
        /// <returns>
        /// true - if time is running
        /// false - if time stopped
        /// </returns>
        public async Task<bool> IsRunning()
        {
            var status = new GetStatus();
            var response = await _StopwatchClient.GetCurrentStatusAsync(status);
            Console.WriteLine($"Running: {response.IsRunning}");
            return response.IsRunning;
        }

        /// <summary>
        /// Gets the current measured time from the server 
        /// </summary>
        /// <returns>time in milliseconds</returns>
        public async Task<long> GetTimeInMS()
        {
            var timeRequest = new GetTime();
            var response = await _StopwatchClient.GetElapsedTimeMillisecondsAsync(timeRequest);
            Console.WriteLine($"Current Runtime: {response.Milliseconds}");
            return response.Milliseconds;
        }
    }
}
