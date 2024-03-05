namespace BlApi;

/// <summary>
/// Represents the interface for task-related functionality.
/// </summary>
public interface ITask
{
    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="item">The task to create.</param>
    /// <returns>The ID of the created task.</returns>
    public int Create(BO.Task item);

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <returns>The number of tasks deleted (0 or 1).</returns>
    public int Delete(int id);

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    /// <param name="item">The updated task.</param>
    public void Update(BO.Task item);

    /// <summary>
    /// Updates the scheduled date of a task.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="date">The new scheduled date.</param>
    public void Update(int id, DateTime? date);

    /// <summary>
    /// Reads a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to read.</param>
    /// <returns>The task with the specified ID, if found; otherwise, null.</returns>
    public BO.Task? Read(int id);

    /// <summary>
    /// Reads a task based on a custom filter.
    /// </summary>
    /// <param name="filter">A custom filter function.</param>
    /// <returns>The task that matches the filter, if found; otherwise, null.</returns>
    public BO.Task? Read(Func<BO.Task?, bool>? filter = null);

    /// <summary>
    /// Reads all tasks based on a custom filter.
    /// </summary>
    /// <param name="filter">A custom filter function.</param>
    /// <returns>An enumerable collection of tasks that match the filter.</returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter = null);

    /// <summary>
    /// Sets the schedule for a task.
    /// </summary>
    /// <param name="item">The task for which to set the schedule.</param>
    public void SetScheduele(BO.Task item);

    /// <summary>
    /// Updates the StartDate and EndDate on that task.
    /// </summary>
    /// <param name="item">The task to update.</param>
    public void UpdateDatesForEngineerWork(BO.Task item);
}