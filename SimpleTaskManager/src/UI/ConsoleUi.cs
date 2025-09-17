using SimpleTaskManager.Models;
using SimpleTaskManager.Services;
using System.Threading.Tasks;

namespace SimpleTaskManager.UI
{
    
    public static class ConsoleUi
    {
        public static void Run()
        {
        Console.Write("Enter username: ");
            string? username = Console.ReadLine();
            var service = new TaskService(username);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Task Manager ---");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. List tasks");
                Console.WriteLine("3. Mark task as done");
                Console.WriteLine("4. Delete task");
                Console.WriteLine("5. Exit");
                Console.Write("Choose: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        Console.Write("Task title: ");
                        string? title = Console.ReadLine();

                        Console.Write("Due date (dd.MM.yyyy) or empty: ");
                        string? dateInput = Console.ReadLine();
                        DateTime? dueDate = null;
                        if (!string.IsNullOrWhiteSpace(dateInput) &&
                            DateTime.TryParseExact(dateInput, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsed))
                        {
                            dueDate = parsed;
                        }

                        service.AddTask(title, dueDate);
                        break;

                    case "2":
                        service.ListTasks();
                        break;

                    case "3":
                        Console.Write("Task id: ");
                        if (int.TryParse(Console.ReadLine(), out int idDone))
                            service.MarkDone(idDone);
                        break;

                    case "4":
                        Console.Write("Task id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int idDelete))
                            service.DeleteTask(idDelete);
                        break;

                    case "5":
                        running = false;
                        break;
                }
            }
        }
    }
}
