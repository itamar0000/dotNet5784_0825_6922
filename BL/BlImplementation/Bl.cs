namespace BlImplementation;
using BlApi;

/// <summary>
/// Represents the Business Logic (BL) implementation.
/// </summary>
internal class Bl : IBl
{
    /// <summary>
    /// Gets the task-related functionality.
    /// </summary>
    public ITask Task => new TaskImplementation();

    /// <summary>
    /// Gets the engineer-related functionality.
    /// </summary>
    public IEngineer Engineer => new EngineerImplementation();

    /// <summary>
    /// Gets the milestone-related functionality.
    /// </summary>
    public IMilestone Milestone => throw new NotImplementedException();

    /// <summary>
    /// Gets the clock-related functionality.
    /// </summary>
    public IClock Clock => new ClockImplementation();
}