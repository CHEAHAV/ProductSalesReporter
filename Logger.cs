using System;
using System.IO;

namespace ProductSalesReporter
{
    public static class Logger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        private static readonly string ErrorLogFilePath = Path.Combine(LogDirectory, "errors.txt");

        public static void LogError(string message)
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message}{Environment.NewLine}";
                File.AppendAllText(ErrorLogFilePath, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
    }
}