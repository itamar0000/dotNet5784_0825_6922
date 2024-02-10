using DO;

namespace BO;

/// <summary>
/// Represents a milestone in a project.
/// </summary>
public class Milestone
{
    /// <summary>
    /// Gets or initializes the ID of the milestone.
    /// </summary>
    public int Id { get; init; } // Primary key, auto-incrementing

    /// <summary>
    /// Gets or initializes the description of the milestone.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Gets or initializes the alias of the milestone.
    /// </summary>
    public string Alias { get; init; }

    /// <summary>
    /// Gets or initializes the date when the milestone was added to the system.
    /// </summary>
    public DateTime CreatedAtDate { get; init; } // Date when the task was added to the system

    /// <summary>
    /// Gets or sets the status of the milestone.
    /// </summary>
    public Status? Status { get; set; } // Calculated

    /// <summary>
    /// Gets or sets the revised scheduled completion date of the milestone.
    /// </summary>
    public DateTime? ForecastDate { get; set; } // A revised scheduled completion date

    /// <summary>
    /// Gets or initializes the latest complete date of the milestone.
    /// </summary>
    public DateTime? DeadlineDate { get; init; } // The latest complete date

    /// <summary>
    /// Gets or sets the real completion date of the milestone.
    /// </summary>
    public DateTime? CompleteDate { get; set; } // Real completion date

    /// <summary>
    /// Gets or sets the percentage of completed tasks for the milestone.
    /// </summary>
    public double? CompletionPercentage { get; set; } // Percentage of completed tasks - Calculated

    /// <summary>
    /// Gets or initializes the remarks associated with the milestone.
    /// </summary>
    public string? Remarks { get; init; } // Free remarks from project meetings

    /// <summary>
    /// Gets or initializes the list of tasks that this milestone depends on.
    /// </summary>
    public List<TaskInList>? Dependencies { get; init; } // List of tasks that this milestone depends on

    /// <summary>
    /// Returns a string representation of the milestone.
    /// </summary>
    /// <returns>A string representation of the milestone.</returns>
    public override string ToString() => this.ToStringProperty();
}