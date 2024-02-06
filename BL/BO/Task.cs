namespace BO;

using System;
using System.Collections.Generic;

public class Task
{
    public int Id { get; init; }
    public string Description { get; init; }
    public string Alias { get; init; }
    public DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; }
    public List<BO.TaskInList>? Dependencies { get; set; }
    public BO.MilestoneInTask? Milestone { get; set; }
    public TimeSpan? RequiredEffortTime { get; init; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; init; }
    public string? Remarks { get; init; }
    public BO.EngineerInTask? Engineer { get; set; }
    public BO.EngineerExperience? Complexity { get; init; }
    public override string ToString() => this.ToStringProperty();
}

