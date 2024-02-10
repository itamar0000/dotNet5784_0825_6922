namespace BO;

/// <summary>
/// Represents a milestone in a list.
/// </summary>
public class MilestoneInList
{
    /// <summary>
    /// Gets or initializes the ID of the milestone.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the description of the milestone.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Gets or initializes the alias of the milestone.
    /// </summary>
    public string Alias { get; init; }

    /// <summary>
    /// Gets or sets the status of the milestone.
    /// </summary>
    public Status? Status { get; set; }

    /// <summary>
    /// Gets or sets the completion percentage of the milestone.
    /// </summary>
    public double? CompletionPercentage { get; set; }

    /// <summary>
    /// Returns a string representation of the milestone.
    /// </summary>
    /// <returns>A string representation of the milestone.</returns>
    public override string ToString() => this.ToStringProperty();
}