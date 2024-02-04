
using DO;

namespace BO;

public class Milestone
{
    public int Id { get; init; } // Primary key, auto-incrementing
    public string Description { get; init; }
    public string Alias { get; init; }
    public DateTime CreatedAtDate { get; init; } // Date when the task was added to the system
    public Status? Status { get; set; } // Calculated
    public DateTime? ForecastDate { get; set; } // A revised scheduled completion date
    public DateTime? DeadlineDate { get; init; } // The latest complete date
    public DateTime? CompleteDate { get; set; } // Real completion date
    public double? CompletionPercentage { get; set; } // Percentage of completed tasks - Calculated
    public string? Remarks { get; init; } // Free remarks from project meetings
    public List<TaskInList>? Dependencies { get; init; } // List of tasks that this milestone depends on
    public override string ToString() => this.ToStringProperty();
}
