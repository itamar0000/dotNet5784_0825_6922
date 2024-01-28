
using DO;

namespace BO;

internal class Milestone
{
    public int Id { get; set; } // Primary key, auto-incrementing
    public string Description { get; set; }
    public string Alias { get; set; }
    public DateTime CreatedAtDate { get; set; } // Date when the task was added to the system
    public Status Status { get; set; } // Calculated
    public DateTime? ForecastDate { get; set; } // A revised scheduled completion date
    public DateTime? DeadlineDate { get; set; } // The latest complete date
    public DateTime? CompleteDate { get; set; } // Real completion date
    public double CompletionPercentage { get; set; } // Percentage of completed tasks - Calculated
    public string Remarks { get; set; } // Free remarks from project meetings
    public List<TaskInList> Dependencies { get; set; } // List of tasks that this milestone depends on
}
