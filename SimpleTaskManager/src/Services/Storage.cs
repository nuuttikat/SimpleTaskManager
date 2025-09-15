using System.Text.Json;
using SimpleTaskManager.Models;

namespace SimpleTaskManager.Services
{
    public static class Storage
    {
        private static string filePath = "tasks.json";

        public static void Save(List<TaskItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, json);
        }

        public static List<TaskItem> Load()
        {
            if (!File.Exists(filePath))
                return new List<TaskItem>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
    }
}
