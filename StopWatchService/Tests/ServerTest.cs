using GrpcServer;
using GrpcServer.Services;
using NUnit.Framework;
using System.Threading;

namespace ServerTests
{
    public class ServerTests
    {
        StopwatchService _Service;

        [SetUp]
        public void Setup()
        {
            _Service = new StopwatchService();
        }

        [Test, Order(1)]
        public void AssertStatusIsNotRunningAfterInitiation()
        {
            Assert.That(_Service.GetCurrentStatus(new GetStatus(), null).Result.IsRunning, Is.False, "Status is 'running' after server start.");
        }

        [Test, Order(2)]
        public void AssertStatusIsRunningAfterStart()
        {
            _Service.Start(new StartTime(), null);
            Assert.That(_Service.GetCurrentStatus(new GetStatus(), null).Result.IsRunning, Is.True, "Status is not 'running' after start() call.");
        }

        [Test, Order(3)]
        public void AssertTimeIsRunningAfterStart()
        {
            _Service.Start(new StartTime(), null);
            var firstTime = _Service.GetElapsedTimeMilliseconds(new GetTime(), null).Result.Milliseconds;
            Thread.Sleep(20);
            var secondTime = _Service.GetElapsedTimeMilliseconds(new GetTime(), null).Result.Milliseconds;
            Assert.That(secondTime, Is.Not.EqualTo(firstTime), "Time not counting up after start.");
        }

        [Test, Order(4)]
        public void AssertTimeStopedAfterStop()
        {
            _Service.Start(new StartTime(), null);
            _Service.Stop(new StopTime(), null);
            var firstTime = _Service.GetElapsedTimeMilliseconds(new GetTime(), null).Result.Milliseconds;
            Thread.Sleep(20);
            var secondTime = _Service.GetElapsedTimeMilliseconds(new GetTime(), null).Result.Milliseconds;
            Assert.That(firstTime, Is.EqualTo(secondTime), "Time not stopped after stop.");
        }

        [Test, Order(5)]
        public void AssertStatusIsNotRunningAfterStop()
        {
            _Service.Start(new StartTime(), null);
            _Service.Stop(new StopTime(), null);
            Assert.That(_Service.GetCurrentStatus(new GetStatus(), null).Result.IsRunning, Is.False, "Status is 'running' after stop() call.");
        }

    }
}