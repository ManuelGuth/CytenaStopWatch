using Grpc.Core;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class StopwatchService : Stopwatch.StopwatchBase
    {
        private System.Diagnostics.Stopwatch _Stopwatch;

        public StopwatchService()
        {
            _Stopwatch = new System.Diagnostics.Stopwatch();
        }

        public override Task<ElapsedTimeMilliseconds> GetElapsedTimeMilliseconds(GetTime request, ServerCallContext context)
        {
            var elapsedTime = new ElapsedTimeMilliseconds { Milliseconds = _Stopwatch.ElapsedMilliseconds };
            return Task.FromResult(elapsedTime);
        }

        public override Task<EmptyResponse> Start(StartTime request, ServerCallContext context)
        {
            if (!_Stopwatch.IsRunning)
            {
                _Stopwatch.Start();
            }
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<EmptyResponse> Stop(StopTime request, ServerCallContext context)
        {
            if (_Stopwatch.IsRunning)
            {
                _Stopwatch.Stop();
            }
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<Status> GetCurrentStatus(GetStatus request, ServerCallContext context)
        {
            var isRunning = _Stopwatch.IsRunning;
            return Task.FromResult(new Status{ IsRunning = isRunning });
        }
    }
}
