namespace BlImplementation;
using BlApi;
using System.Xml.Linq;

/// <summary>
/// Represents the Business Logic (BL) implementation.
/// </summary>
internal class Bl : IBl
{
    /// <summary>
    /// Gets the task-related functionality.
    /// </summary>
    //public ITask Task => new TaskImplementation();
    public ITask Task => new TaskImplementation(this);


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


    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime CurrentClock { get { return s_Clock; } private set { s_Clock = value; } }

    public void PromoteDay() => CurrentClock = CurrentClock.AddDays(1);

    public void PromoteHour() => CurrentClock = CurrentClock.AddHours(1);

    public void ResetTime() => CurrentClock = DateTime.Now;
}