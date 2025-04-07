
namespace UsersManagementApi
{
    public static class FileLogger
    {
        public static void Log(string message)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
            if (File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{DateTime.UtcNow}: {message}");
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"{DateTime.UtcNow}: {message}");
                }
            }
        }

    }
}
