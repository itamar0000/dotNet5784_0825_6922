namespace BO;

/// <summary>
/// Represents an engineer in the system.
/// </summary>
public class Engineer
{
    /// <summary>
    /// Gets or sets the ID of the engineer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the engineer.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets or sets the email of the engineer.
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Gets or sets the cost associated with the engineer.
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    /// Gets or sets the experience level of the engineer.
    /// </summary>
    public EngineerExperience Level { get; init; }

    /// <summary>
    /// The image of the engineer.
    /// </summary>
    public string? ImagePath { get; set; }

    /// <summary>
    /// Gets or sets the task associated with the engineer.
    /// </summary>
    public TaskInEngineer? Task { get; set; }

    /// <summary>
    /// Returns a string that represents the current engineer.
    /// </summary>
    /// <returns>A string that represents the current engineer.</returns>
    public override string ToString() => this.ToStringProperty();
}

