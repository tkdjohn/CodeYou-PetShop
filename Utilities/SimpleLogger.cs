using Microsoft.Extensions.Logging;

namespace Utilities {
    public class SimpleLogger :ILogger {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel) {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
            Console.Error.WriteLine(exception?.ToString());
        }

        public void LogException(Exception ex) {
            // for now we will just write it to error console
            Console.Error.WriteLine(ex.ToString());
        }

        public void LogDebug(string message) {
          Console.WriteLine(message);
        }
    }
}
