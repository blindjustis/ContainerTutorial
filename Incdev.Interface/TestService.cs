using System;
using Incdev.Interface.Logging;
using Microsoft.Extensions.Logging;

namespace Incdev.Interface
{
    public interface ITestService
    {
        void DoStuff();
    }

    internal static class EventRegister
    {
        public static readonly RegisteredEventId ServiceEvent = new RegisteredEventId(new EventId(6000, "Service Called"), LogLevel.Information);
    }

    public class TestService : ITestService
    {
        private readonly ILogger _log;
        public TestService(ILogger<TestService> log)
        {
            _log = log;
        }

        public void DoStuff()
        {
            _log.LogEvent()(EventRegister.ServiceEvent, ("FIND","CHILD"));
        }
    }
}
