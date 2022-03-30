namespace Core;

public interface ILogger
{
    public void Info(string message);
    public void Log(string message);
    public void Warn(string message);
    public void Error(string message);
}

public enum LogLevel {
    Info,
    Log,
    Warn,
    Error
}

public abstract class Logger : ILogger
{
    public void Error(string message)
    {
        Console.WriteLine($"ERROR : {message}");
    }

    public void Info(string message)
    {
        Console.WriteLine($"INFO  : {message}");
    }

    public void Log(string message)
    {
        Console.WriteLine($"LOG   : {message}");
    }

    public void Warn(string message)
    {
        Console.WriteLine($"WARN  : {message}");
    }
}