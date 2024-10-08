﻿namespace Dal;
using DalApi;
using DO;

internal class TaskImplementation : ITask
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
        Task? found = DataSource.Tasks.Find(task => (id == task.Id) && task.isActive);
        if (found == null) throw new DalDoesNotExistException($"Task with ID = {id} does not exist");
        else
        {
            Task item = found with { isActive = false };
            DataSource.Tasks.RemoveAll(temp=> temp.Id == id);
            DataSource.Tasks.Add(item);
        }
    }

    /// <summary>
    /// check if the item with specific id exists and if it is returns the item else throw excpetion
    /// </summary>
    public DO.Task? Read(int id)
    {
        Task? found = DataSource.Tasks.FirstOrDefault(task => id == task.Id);
        if ((found != null) && (found.isActive == true)) return found;
        return null;
    }

    /// <summary>
    /// copy the list into a new list
    /// </summary>
    public IEnumerable<DO.Task?> ReadAll(Func<Task?,bool>?filter=null)
    {
        if(filter==null)
        {
            return DataSource.Tasks.Select(task => task);
        }
        else
        {
            return DataSource.Tasks.Where(filter);
        }
    }

    /// <summary>
    ///  checks if item with the same id exists if it is deletes it and recreate it with updated values
    /// </summary>
    public void Update(DO.Task item)
    {
        Task? found = DataSource.Tasks.Find(task => (item.Id == task.Id && task.isActive == true));
        DataSource.Tasks.RemoveAll(x=>x.Id == item.Id);
        item = item with { ScheduledDate = item.ScheduledDate };
        DataSource.Tasks.Add(item);
    }

    /// <summary>
    /// the function gets a function and retrun the first value that suits the criterion else return deafualt
    /// </summary>
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }

    /// <summary>
    /// delete all the database
    /// </summary>
    public void DeleteAll()
    {
        DataSource.Engineers.Clear();
    }
}