using System;
using System.Diagnostics;

namespace API.Logging
{
    public interface ILogger
    {
        void LogUnhandledException(Exception exception);
    }

    public class Logger : ILogger
    {
        // stuff to check:
        // https://blogs.aws.amazon.com/net/post/Tx3N7JHP24J1EJ5/Configuring-Advanced-Logging-on-AWS-Elastic-Beanstalk
        // https://blogs.aws.amazon.com/net/post/TxZLWAOFZJQWRP/Logging-with-the-AWS-SDK-for-NET

        private static readonly TraceSource TraceSource = new TraceSource("Logger");

        public void LogUnhandledException(Exception exception)
        {
            TraceSource.TraceEvent(TraceEventType.Error, 0, exception.ToString());
        }


    }
}