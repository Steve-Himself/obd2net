using System;
using System.Diagnostics;
using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Logging
{
    public class TraceLogger : ILogger
    {
        public void Debug(string format, params object[] args)
        {
            Trace.WriteLine(string.Format(format, args));
        }

        public void Info(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        public void Warn(string format, params object[] args)
        {
            Trace.TraceWarning(format, args);
        }

        public void Warn(Exception exc, string format, params object[] args)
        {
            Trace.TraceWarning(format, args);
            if (exc == null) return;

            string exceptionMessage;
            if (exc.InnerException != null)
            {
                exceptionMessage = $"{exc.Message}{Environment.NewLine}{exc.StackTrace}{Environment.NewLine}{exc.InnerException.Message}{Environment.NewLine}{exc.InnerException.StackTrace}";
            }
            else
            {
                exceptionMessage = $"{exc.Message}{Environment.NewLine}{exc.StackTrace}";
            }
            Trace.TraceError(exceptionMessage);
        }

        public void Error(string format, params object[] args)
        {
            Trace.TraceError(format, args);
        }

        public void Error(Exception exc, string format, params object[] args)
        {
            Trace.TraceError(format, args);
            if (exc == null) return;

            string exceptionMessage;
            if (exc.InnerException != null)
            {
                exceptionMessage = $"{exc.Message}{Environment.NewLine}{exc.StackTrace}{Environment.NewLine}{exc.InnerException.Message}{Environment.NewLine}{exc.InnerException.StackTrace}";
            }
            else
            {
                exceptionMessage = $"{exc.Message}{Environment.NewLine}{exc.StackTrace}";
            }
            Trace.TraceError(exceptionMessage);
        }
    }
}