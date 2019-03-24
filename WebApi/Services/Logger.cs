using System.Diagnostics;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class Logger : ILogger
    {
        public void LogError(string error)
        {
            Debug.WriteLine($"ERROR: {error}");
        }

        public void LogInformation(string msg)
        {
            Debug.WriteLine($"INFO: {msg}");
        }
    }
}