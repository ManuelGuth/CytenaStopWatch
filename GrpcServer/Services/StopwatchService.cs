using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class StopwatchService : Stopwatch.StopwatchBase
    {
        private readonly ILogger<StopwatchService> _logger;
        private System.Diagnostics.Stopwatch _stopwatch;

        public StopwatchService(ILogger<StopwatchService> logger, System.Diagnostics.Stopwatch stopwatch)
        {
            _logger = logger;
            _stopwatch = stopwatch;
            Console.WriteLine("Init");
        }

        public override Task<ElapsedTimeMilliseconds> GetElapsedTimeMilliseconds(GetTime request, ServerCallContext context)
        {
            var elapsedTime = new ElapsedTimeMilliseconds { Milliseconds = _stopwatch.ElapsedMilliseconds };
            return Task.FromResult(elapsedTime);
        }

        public override Task<EmptyResponse> Start(StartTime request, ServerCallContext context)
        {
            if (!_stopwatch.IsRunning) 
            {
                _stopwatch.Start();
            }
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<EmptyResponse> Stop(StopTime request, ServerCallContext context)
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
            }
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<Status> GetCurrentStatus(GetStatus request, ServerCallContext context)
        {
            var isRunning = _stopwatch.IsRunning;
            return Task.FromResult(new Status{ IsRunning = isRunning });
        }
    }
}
