namespace BO;

/// <summary>
/// Represents a task in a list.
/// </summary>
public class TaskInList
{
    /// <summary>
    /// Gets or initializes the ID of the task.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the description of the task.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Gets or initializes the alias of the task.
    /// </summary>
    public string Alias { get; init; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status? Status { get; set; }

    /// <summary>
    /// Returns a string representation of the task.
    /// </summary>
    /// <returns>A string representation of the task.</returns>
    public override string ToString() => this.ToStringProperty();
}