using System.Text.Json; // Provides JSON serialization/deserialization
using System;

namespace tasktracker
{
    public class TaskItem
    {
        // Read-only property (set only in constructor)
        public string TaskName { get; }

        // Read/Write property (can be changed during runtime)
        public string Status { get; set; }

        // Constructor that sets default status to "Incomplete"
        public TaskItem(string taskName)
        {
            this.TaskName = taskName;
            this.Status = "Incomplete";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // File path where tasks will be stored in JSON format
            string filePath = (@"<filepath>\tracker.json");

            // Create an empty JSON array file if it doesn't exist
            if (!File.Exists(filePath))
            {
                string brackets = "[]"; // Represents an empty list in JSON
                File.WriteAllText(filePath, brackets);
            }

            // List to hold all task objects in memory
            List<TaskItem> tasksList;

            // Read the JSON file as a string
            string jsonString = File.ReadAllText(filePath);

            // Deserialize JSON into a List<TaskItem> object
            tasksList = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);

            // Flag to control the menu loop
            bool finished = false;

            // Main program loop: runs until user chooses to exit
            while (!finished)
            {
                // Menu display (multi-line string with """")
                Console.WriteLine("""
                        Task Tracker Menu:
                    --------------------------
                    1. Display All Tasks
                    2. Add Another Task
                    3. Mark Task as Complete
                    4. Remove a Task
                    5. Exit the Menu

                    Please enter a number:
                    """);

                // Read user input and convert to integer
                int userAction = int.Parse(Console.ReadLine());

                // Perform action based on user's menu choice
                switch (userAction)
                {
                    case 1:
                        Console.Clear();

                        // Loop through all tasks and display their name and status
                        foreach (TaskItem item in tasksList)
                        {
                            Console.WriteLine($"{item.TaskName} - {item.Status}");
                        }

                        // Wait for a key press before going back to the menu
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();

                        // Ask for the new task's name
                        Console.WriteLine("Task Name to be Added: ");
                        string newTaskName = Console.ReadLine();

                        // Create a new task object with default status
                        TaskItem newTask = new TaskItem(newTaskName);

                        // Add the new task to the in-memory list
                        tasksList.Add(newTask);
                        break;

                    case 3:
                        Console.Clear();

                        // Ask for the task name to mark as complete
                        Console.WriteLine("Task Name to be Completed: ");
                        string completedTask = Console.ReadLine();

                        // Find the task in the list by its name, then set status to "Complete"
                        tasksList[(tasksList.FindIndex(item => item.TaskName == completedTask))].Status = "Complete";
                        break;

                    case 4:
                        Console.Clear();

                        // Ask for the task name to remove
                        Console.WriteLine("Task Name to be Removed: ");
                        string removedTask = Console.ReadLine();

                        // Find the task in the list by its name, then remove it
                        tasksList.Remove(tasksList[(tasksList.FindIndex(item => item.TaskName == removedTask))]);
                        break;

                    case 5:
                        Console.Clear();

                        // Inform user that tasks are being saved
                        Console.WriteLine("Your Tasks have been saved. Thank for using Task Tracker!");

                        // Serialize the in-memory list back to JSON format
                        jsonString = JsonSerializer.Serialize(tasksList);

                        // Write the JSON string to the file
                        File.WriteAllText(filePath, jsonString);

                        // Set loop flag to true to exit program
                        finished = true;

                        // Delay before program closes (5 seconds)
                        Thread.Sleep(5000);
                        break;
                }

                // Clear screen before showing menu again
                Console.Clear();
            }
        }
    }
}
