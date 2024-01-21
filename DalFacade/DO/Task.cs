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
    bool IsMileStone = false,
    bool isActive = true,
    DateTime? ScheduledDate = null,
    DateTime? StartDate = null,
    TimeSpan? RequiredEffortTime = null,
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null,
    EngineerExperience? Complexity = null
)
{  /// <summary>
   /// Default constructor for the Task record.
   /// Initializes default values for some properties.
   /// </summary>
    public Task() : this(Id: 0, Alias: "", Description: "", CreatedAtDate: DateTime.Now) { }

    /// <summary>
    /// Determines whether the ScheduledDate property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeScheduledDate() { return ScheduledDate.HasValue; }

    /// <summary>
    /// Determines whether the StartDate property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeStartDate() { return StartDate.HasValue; }

    /// <summary>
    /// Determines whether the RequiredEffortTime property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeRequiredEffortTime() { return RequiredEffortTime.HasValue; }

    /// <summary>
    /// Determines whether the DeadlineDate property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeDeadlineDate() { return DeadlineDate.HasValue; }

    /// <summary>
    /// Determines whether the CompleteDate property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeCompleteDate() { return CompleteDate.HasValue; }

    /// <summary>
    /// Determines whether the Deliverables property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeDeliverables() { return !string.IsNullOrEmpty(Deliverables); }

    /// <summary>
    /// Determines whether the Remarks property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeRemarks() { return !string.IsNullOrEmpty(Remarks); }

    /// <summary>
    /// Determines whether the EngineerId property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeEngineerId() { return EngineerId.HasValue; }

    /// <summary>
    /// Determines whether the Complexity property should be included in serialization.
    /// </summary>
    public bool ShouldSerializeComplexity() { return Complexity.HasValue; }
}
