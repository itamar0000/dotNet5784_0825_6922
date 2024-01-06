namespace Dal;
using DalApi;
using DO;

public class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        int NewId = DataSource.Config.NextTaskId;
        Task copy = item with { Id = NewId };
        DataSource.Tasks.Add(copy);
        return NewId;
    }

    public void Delete(int id)
    {
        Task? found = DataSource.Tasks.Find(task => id == task.Id);
        if (found == null) throw new Exception($"Task with ID = {id} does not exist");
        else DataSource.Tasks.Remove(found);
    }

    public DO.Task? Read(int id)
    {
        return DataSource.Tasks.Find(task => id == task.Id);
    }

    public List<DO.Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(DO.Task item)
    {
        Delete(item.Id);
        DataSource.Tasks.Add(item);
    }
}
