namespace Classes;

public class Tasks
{
    public string taskName { get; }
    public string status { get; set; }

    public Tasks(string taskName)
    {
        this.taskName = taskName;
        this.status = "Incomplete";
    }
}