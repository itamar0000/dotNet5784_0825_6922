namespace BO;

/// <summary>
/// Represents a milestone associated with a task.
/// </summary>
public class MilestoneInTask
{
    /// <summary>
    /// Gets or initializes the ID of the milestone.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the alias of the milestone.
    /// </summary>
    public string Alias { get; init; }

    /// <summary>
    /// Returns a string representation of the milestone.
    /// </summary>
    /// <returns>A string representation of the milestone.</returns>
    public override string ToString() => this.ToStringProperty();
}