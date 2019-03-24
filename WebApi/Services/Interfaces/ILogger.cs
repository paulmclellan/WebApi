namespace WebApi.Services.Interfaces
{
    public interface ILogger
    {
        void LogInformation(string msg);
        void LogError(string error);
    }
}