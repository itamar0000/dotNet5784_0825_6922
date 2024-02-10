namespace BO;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a task in the system.
/// </summary>
public class Task
{
    /// <summary>
    /// Gets or initializes the ID of the task.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the description of the task.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Gets or initializes the alias of the task.
    /// </summary>
    public string Alias { get; init; }

    /// <summary>
    /// Gets or initializes the creation date of the task.
    /// </summary>
    public DateTime CreatedAtDate { get; init; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status? Status { get; set; }

    /// <summary>
    /// Gets or sets the list of dependencies of the task.
    /// </summary>
    public List<BO.TaskInList>? Dependencies { get; set; }

    /// <summary>
    /// Gets or sets the milestone associated with the task.
    /// </summary>
    public BO.MilestoneInTask? Milestone { get; set; }

    /// <summary>
    /// Gets or initializes the required effort time for the task.
    /// </summary>
    public TimeSpan? RequiredEffortTime { get; init; }

    /// <summary>
    /// Gets or sets the start date of the task.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the scheduled date of the task.
    /// </summary>
    public DateTime? ScheduledDate { get; set; }

    /// <summary>
    /// Gets or sets the forecasted date of the task.
    /// </summary>
    public DateTime? ForecastDate { get; set; }

    /// <summary>
    /// Gets or sets the deadline date of the task.
    /// </summary>
    public DateTime? DeadlineDate { get; set; }

    /// <summary>
    /// Gets or sets the completion date of the task.
    /// </summary>
    public DateTime? CompleteDate { get; set; }

    /// <summary>
    /// Gets or initializes the deliverables associated with the task.
    /// </summary>
    public string? Deliverables { get; init; }

    /// <summary>
    /// Gets or initializes the remarks related to the task.
    /// </summary>
    public string? Remarks { get; init; }

    /// <summary>
    /// Gets or sets the engineer associated with the task.
    /// </summary>
    public BO.EngineerInTask? Engineer { get; set; }

    /// <summary>
    /// Gets or initializes the complexity of the task.
    /// </summary>
    public BO.EngineerExperience? Complexity { get; init; }

    /// <summary>
    /// Returns a string representation of the task.
    /// </summary>
    /// <returns>A string representation of the task.</returns>
    public override string ToString() => this.ToStringProperty();
}