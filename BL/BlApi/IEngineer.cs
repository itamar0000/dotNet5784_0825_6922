namespace BlApi;

/// <summary>
/// Represents the interface for engineer-related functionality.
/// </summary>
public interface IEngineer
{
    /// <summary>
    /// Creates a new engineer.
    /// </summary>
    /// <param name="boEngineer">The engineer to create.</param>
    /// <returns>The ID of the created engineer.</returns>
    public int Create(BO.Engineer boEngineer);

    /// <summary>
    /// Deletes an engineer by ID.
    /// </summary>
    /// <param name="id">The ID of the engineer to delete.</param>
    public void Delete(int id);

    /// <summary>
    /// Updates an existing engineer.
    /// </summary>
    /// <param name="boEngineer">The updated engineer.</param>
    public void Update(BO.Engineer boEngineer);

    /// <summary>
    /// Reads an engineer by ID.
    /// </summary>
    /// <param name="id">The ID of the engineer to read.</param>
    /// <returns>The engineer with the specified ID.</returns>
    public BO.Engineer? Read(int id);

    /// <summary>
    /// Reads an engineer based on a custom filter.
    /// </summary>
    /// <param name="filter">A custom filter function.</param>
    /// <returns>The engineer that matches the filter.</returns>
    public BO.Engineer? Read(Func<BO.Engineer?, bool>? filter);

    /// <summary>
    /// Reads all engineers based on a custom filter.
    /// </summary>
    /// <param name="filter">A custom filter function.</param>
    /// <returns>An enumerable collection of engineers that match the filter.</returns>
    public IEnumerable<BO.Engineer>? ReadAll(Func<BO.Engineer?, bool>? filter = null);

    /// <summary>
    /// Assigns a task to an engineer.
    /// </summary>
    /// <param name="engineerId">The ID of the engineer.</param>
    /// <param name="taskId">The ID of the task to assign.</param>
    public void Assign(int engineerId, int taskId);
}