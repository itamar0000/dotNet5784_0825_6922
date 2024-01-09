namespace Dal;
using DalApi;
using DO;

public class TaskImplementation : ITask
{
    /// <summary>
    /// check if item already exists if it doesnt it add this item to the list
    /// </summary>
    public int Create(DO.Task item)
    {
        int NewId = DataSource.Config.NextTaskId;
        Task copy = item with { Id = NewId };
        DataSource.Tasks.Add(copy);
        return NewId;
    }

    /// <summary>
    ///  check if item exists if it is delete it otherwise throw exception
    /// </summary>
    public void Delete(int id)
    {
        Task? found = DataSource.Tasks.Find(task => (id == task.Id) && task.IsActive);
        if (found == null) throw new Exception($"Task with ID = {id} does not exist");
        else
        {
            Task item = found with { IsActive = false };
            DataSource.Tasks.RemoveAll(temp=> temp.Id == id);
            DataSource.Tasks.Add(item);
        }
    }

    /// <summary>
    /// check if the item with specific id exists and if it is returns the item else throw excpetion
    /// </summary>
    public DO.Task? Read(int id)
    {
        Task? found = DataSource.Tasks.Find(task => id == task.Id);
        if ((found != null) && (found.IsActive == true)) return found;
        return null;
    }

    /// <summary>
    /// copy the list into a new list
    /// </summary>
    public List<DO.Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    /// <summary>
    ///  checks if item with the same id exists if it is deletes it and recreate it with updated values
    /// </summary>
    public void Update(DO.Task item)
    {
        Task? found = DataSource.Tasks.Find(task => (item.Id == task.Id && task.IsActive == true));
        DataSource.Tasks.Remove(found);
        item = item with { CreatedAtDate = item.CreatedAtDate };
        DataSource.Tasks.Add(item);
    }
}