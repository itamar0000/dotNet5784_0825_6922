namespace BlApi;


/// <summary>
/// Represents the interface for business logic operations.
/// </summary>
public interface IBl
{
    /// <summary>
    /// Gets the interface for task-related operations.
    /// </summary>
    public ITask Task { get; }

    /// <summary>
    /// Gets the interface for engineer-related operations.
    /// </summary>
    public IEngineer Engineer { get; }

    /// <summary>
    /// Gets the interface for milestone-related operations.
    /// </summary>
    public IMilestone Milestone { get; }

    /// <summary>
    /// Gets the interface for clock-related operations.
    /// </summary>
    public IClock Clock { get; }

    public void InitializeDB() => DalTest.Initialization.Do();

    public void ResetDB() => DalTest.Initialization.Reset();


    #region Time

    public DateTime CurrentClock { get; }

    public void PromoteHour();
    public void PromoteDay();
    public void ResetTime();
    #endregion

}