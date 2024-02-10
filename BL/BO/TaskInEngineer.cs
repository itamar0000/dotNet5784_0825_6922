namespace BO;

/// <summary>
/// Represents a task assigned to an engineer.
/// </summary>
public class TaskInEngineer
{
    /// <summary>
    /// Gets or initializes the ID of the task.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the alias of the task.
    /// </summary>
    public string Alias { get; init; }

    /// <summary>
    /// Returns a string representation of the task assigned to an engineer.
    /// </summary>
    /// <returns>A string representation of the task assigned to an engineer.</returns>
    public override string ToString() => this.ToStringProperty();
}

