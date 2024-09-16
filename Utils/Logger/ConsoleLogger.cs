namespace BackendService.Utils.Logger
{
    public class ConsoleLogger : BaseLogger
    {
        public override void Log(string message, LogLevel level)
        {
            var logMessage = FormatLogger(message, level);
            Console.WriteLine(logMessage);
        }
    }
}
