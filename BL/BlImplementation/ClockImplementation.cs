using BlApi;
using BO;
using System.Data.Common;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BlImplementation;

/// <summary>
/// Represents the implementation of the clock functionality.
/// </summary>
internal class ClockImplementation : BlApi.IClock
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Gets the end date of the project.
    /// </summary>
    /// <returns>The end date of the project.</returns>
    public DateTime? GetEndDate()
    {
        return _dal.Clock.GetEndDate();
    }

    /// <summary>
    /// Gets the start date of the project.
    /// </summary>
    /// <returns>The start date of the project.</returns>
    public DateTime? GetStartDate()
    {
        return _dal.Clock.GetStartDate();
    }

    /// <summary>
    /// Sets the end date of the project.
    /// </summary>
    /// <param name="time">The end date to be set.</param>
    public void SetEndDate(DateTime time)
    {
        _dal.Clock.SetEndDate(time);
    }

    /// <summary>
    /// Sets the start date of the project.
    /// </summary>
    /// <param name="time">The start date to be set.</param>
    public void SetStartDate(DateTime time)
    {
        _dal.Clock.SetStartDate(time);
    }

    /// <summary>
    /// Gets the status of the project based on the current date.
    /// </summary>
    /// <returns>The status of the project.</returns>
    public ProjectStatus GetStatus()
    {
        if (GetStartDate() == null)
            return ProjectStatus.BeforeStart;
        if (DateTime.Now > GetEndDate())
            return ProjectStatus.end;
        else if (DateTime.Now < GetStartDate())
            return ProjectStatus.BeforeStart;
        return ProjectStatus.Start;
    }

    public DateTime? GetCurrentDate()
    {
        return _dal.Clock.GetCurrentDate();
    }

    public void SetCurrentDate(DateTime time)
    {
        _dal.Clock.SetCurrentDate(time);
    }
}