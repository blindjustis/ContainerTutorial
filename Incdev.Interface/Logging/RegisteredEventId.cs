using Microsoft.Extensions.Logging;

namespace Incdev.Interface.Logging
{
    public class RegisteredEventId
    {
        public EventId Id { get; private set; }
        public LogLevel Level { get; private set; }

        public RegisteredEventId(EventId id, LogLevel level)
        {
            Id = id;
            Level = level;
        }
    }
}
