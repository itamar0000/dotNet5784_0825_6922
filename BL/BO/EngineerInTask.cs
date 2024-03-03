namespace BO;

//// <summary>
/// Represents an engineer associated with a task.
/// </summary>
public class EngineerInTask
{
    /// <summary>
    /// Gets or sets the ID of the engineer.
    /// </summary>
    public int? Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the engineer.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Returns a string that represents the current engineer.
    /// </summary>
    /// <returns>A string that represents the current engineer.</returns>
    public override string ToString() => this.ToStringProperty();
}