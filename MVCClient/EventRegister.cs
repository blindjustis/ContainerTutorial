using System;
using Incdev.Interface.Logging;
using Microsoft.Extensions.Logging;

namespace MVCClient
{
    internal static class EventRegister
    {
        public static readonly RegisteredEventId ContactEvent = new RegisteredEventId(new EventId(7000, "Contact Called"), LogLevel.Information);

        public static readonly RegisteredEventId ExceptionGeneratedEvent = new RegisteredEventId(new EventId(7001, "Somehing Blew!"), LogLevel.Error);

        public static readonly RegisteredEventId TheEvent = new RegisteredEventId(new EventId(7002, "The Thing Happened"), LogLevel.Information);
    }
}
