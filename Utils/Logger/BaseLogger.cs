namespace BackendService.Utils.Logger
{
    public abstract class BaseLogger : ICustomeLogger
    {
        public abstract void Log(string message, LogLevel level);

        protected string FormatLogger(string message, LogLevel level)
        {
            return $"[{DateTime.UtcNow}] {level.ToString()}: {message}";
        }
    }
}
