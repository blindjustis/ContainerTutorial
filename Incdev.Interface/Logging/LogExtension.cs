using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Incdev.Interface.Logging
{
    public static class LogExtension
    {
        public delegate void ParamsFunc(RegisteredEventId id, params (string Name, object Data)[] args);
        public delegate void ExceptionAndParamsFunc(RegisteredEventId id, Exception exception, params (string Name, object Data)[] args);

        public static ParamsFunc LogEvent(this ILogger logger, [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) => 
            (id, args) => CallLog(logger, id, filePath, line, args);

        public static ExceptionAndParamsFunc LogExceptionEvent(this ILogger logger,  [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) =>
            (id, ex, args) => CallLog(logger, id, ex, filePath, line, args);


        private static string BuildMessage(string name, (string Name, object Data)[] args)
        {
            var sb = new StringBuilder(name)
                .Append(" {FilePath} {LineNumber} ");

            if (null != args && args.Length > 0)
            {
                sb.Append(" Args : ");
                foreach (var arg in args)
                {
                    sb.Append("{")
                      .Append(arg.Name)
                      .Append("} ");
                }
            }

            return sb.ToString();
        }

        public static object[] BuildArgs(string filePath, int line, (string Name, object Data)[] args )
        {
            var list = new List<object>();
            list.Add(filePath);
            list.Add(line);
            var argData = args?.Select(a => a.Data).ToArray();
            if (args != null && args.Length > 0)
                list.AddRange(args.Select(a => a.Data));
            
            return list.ToArray();
        }

        private static void CallLog(ILogger logger, RegisteredEventId id, string filePath , int line , params (string Name, object Data)[] args)
        {
            var message = BuildMessage(id.Id.Name, args);
            var data = BuildArgs( filePath, line, args);

            Action<EventId, string, object[]> method = null;
            switch (id.Level)
            {
                case LogLevel.Trace:
                    method = logger.LogTrace;
                    break;
                case LogLevel.Debug:
                    method = logger.LogDebug;
                    break;
                case LogLevel.Information:
                    method = logger.LogInformation;
                    break;
                case LogLevel.Warning:
                    method = logger.LogWarning;
                    break;
                case LogLevel.Error:
                    method = logger.LogError;
                    break;
                case LogLevel.Critical:
                    method = logger.LogCritical;
                    break;
            }


            method(id.Id, message, data);
        }

        private static void CallLog(ILogger logger, RegisteredEventId id, Exception ex, string filePath, int line, params (string Name, object Data)[] args)
        {
            Action<EventId, Exception, string, object[]> method = null;
            switch (id.Level)
            {
                case LogLevel.Trace:
                    method = logger.LogTrace;
                    break;
                case LogLevel.Debug:
                    method = logger.LogDebug;
                    break;
                case LogLevel.Information:
                    method = logger.LogInformation;
                    break;
                case LogLevel.Warning:
                    method = logger.LogWarning;
                    break;
                case LogLevel.Error:
                    method = logger.LogError;
                    break;
                case LogLevel.Critical:
                    method = logger.LogCritical;
                    break;
            }

            var message = BuildMessage(id.Id.Name, args);
            var data = BuildArgs( filePath, line, args);

            method(id.Id, ex, message, data);
        }
    }
}
