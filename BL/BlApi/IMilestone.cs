namespace BlApi;

//// <summary>
/// Interface for interacting with milestones.
/// </summary>
public interface IMilestone
{
    /// <summary>
    /// Creates a milestone with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the milestone to create.</param>
    /// <returns>The ID of the created milestone.</returns>
    public int Create(int id);

    /// <summary>
    /// Deletes the milestone with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the milestone to delete.</param>
    /// <returns>The ID of the deleted milestone.</returns>
    public int Delete(int id);

    /// <summary>
    /// Updates the specified milestone.
    /// </summary>
    /// <param name="item">The milestone to update.</param>
    /// <returns>The ID of the updated milestone.</returns>
    public int Update(BO.Milestone item);

    /// <summary>
    /// Retrieves the milestone with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the milestone to retrieve.</param>
    /// <returns>The milestone with the specified ID.</returns>
    public BO.Milestone Read(int id);

    /// <summary>
    /// Retrieves all milestones.
    /// </summary>
    /// <returns>An enumerable collection of milestones.</returns>
    public IEnumerable<BO.Milestone> ReadAll();
}