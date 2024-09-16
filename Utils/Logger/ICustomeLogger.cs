namespace BackendService.Utils.Logger
{
    public interface ICustomeLogger
    {
        public abstract void Log(string message, LogLevel level);
    }
}
