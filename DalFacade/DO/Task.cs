namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Alias"></param>
/// <param name="Description"></param>
/// <param name="CreatedAtDate">Date when the task was added to the system</param>
/// <param name="IsMilestone"></param>
/// <param name="Complexity">task: minimum expirience for engineer to assign</param>
/// <param name="ScheduledDate">the planned start date</param>
/// <param name="StartDate">the real start date</param>
/// <param name="RequiredEffortTime">how many men-days needed for the task (for MS it is null)</param>
/// <param name="DeadlineDate">the latest complete date</param>
/// <param name="CompleteDate">task: real completion date</param>
/// <param name="Deliverables">task: description of deliverables for MS copmletion</param>
/// <param name="Remarks">free remarks from project meetings</param>
/// <param name="EngineerId"></param>
public record Task
(
    int Id,
    string Alias,
    string Description,
    DateTime CreatedAtDate,
    bool IsMilestone = false,
    DO.EngineerExperience? Complexity = null,
    DateTime? ScheduledDate = null,
    DateTime ?StartDate = null,
    TimeSpan? RequiredEffortTime = null,    
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null,
    bool IsActive=true
)
{
    Task() : this(0, "", "", DateTime.Now) { }
}