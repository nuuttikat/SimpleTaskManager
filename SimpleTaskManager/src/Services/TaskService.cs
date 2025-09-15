using SimpleTaskManager.Models;

namespace SimpleTaskManager.Services
{
    public class TaskService
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private string file;

        public TaskService(string username)
        {
            file = $"{username}_tasks.txt"; // käyttäjäkohtainen tiedosto
            Load();
        }

        public void AddTask(string title, DateTime? dueDate = null)
        {
            int id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(new TaskItem { Id = id, Title = title, Done = false, DueDate = dueDate });
            Save();
        }

        public void ListTasks()
        {
            foreach (var task in tasks)
            {
                if (task.Done)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (task.DueDate.HasValue && task.DueDate.Value < DateTime.Now)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(task.ToString());
            }
            Console.ResetColor();
        }

        public void MarkDone(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Done = true;
                Save();
            }
        }

        public void DeleteTask(int id)
        {
            tasks = tasks.Where(t => t.Id != id).ToList();
            Save();
        }

        private void Save()
        {
            var lines = tasks.Select(t =>
                $"{t.Id}|{t.Title}|{t.Done}|{t.DueDate?.ToString("o")}");
            File.WriteAllLines(file, lines);
        }

        private void Load()
        {
            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);
                tasks = lines.Select(line =>
                {
                    var parts = line.Split('|');
                    return new TaskItem
                    {
                        Id = int.Parse(parts[0]),
                        Title = parts[1],
                        Done = bool.Parse(parts[2]),
                        DueDate = string.IsNullOrEmpty(parts[3]) ? null : DateTime.Parse(parts[3])
                    };
                }).ToList();
            }
        }
    }
}
