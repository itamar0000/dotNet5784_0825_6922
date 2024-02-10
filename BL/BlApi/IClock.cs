using BO;
namespace BlApi;

/// <summary>
/// Represents the interface for clock-related functionality.
/// </summary>
public interface IClock
{
    /// <summary>
    /// Sets the start date of the project.
    /// </summary>
    /// <param name="time">The start date to set.</param>
    public void SetStartDate(DateTime time);

    /// <summary>
    /// Sets the end date of the project.
    /// </summary>
    /// <param name="time">The end date to set.</param>
    public void SetEndDate(DateTime time);

    /// <summary>
    /// Retrieves the start date of the project.
    /// </summary>
    /// <returns>The start date of the project.</returns>
    public DateTime? GetStartDate();

    /// <summary>
    /// Retrieves the end date of the project.
    /// </summary>
    /// <returns>The end date of the project.</returns>
    public DateTime? GetEndDate();

    /// <summary>
    /// Retrieves the current status of the project based on the current date.
    /// </summary>
    /// <returns>The current status of the project.</returns>
    public ProjectStatus GetStatus();
}