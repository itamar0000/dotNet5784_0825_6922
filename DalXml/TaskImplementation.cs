namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;

internal class TaskImplementation:ITask
{
    readonly string s_tasks_xml = "tasks";

    // make a function that create a Task and insert it to the XML file
    // the function will return the id of the new Task
    // the function will be called from Create(Task item)
    public int Create(Task item)
    {
        // get the next id from the XML file
        int id = Config.NextTaskId;
       
        var taskslist = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task newItem = item with { Id = id, CreatedAtDate = DateTime.Now };

        taskslist.Add(newItem);

        XMLTools.SaveListToXMLSerializer<Task>(taskslist, s_tasks_xml);

        // return the id of the new Task
        return id;
    }
  
    public void Delete(int id)
    {
        List<Task>  taskslist = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        Task? item = taskslist.Find(task => task.Id == id);

        if (item == null) 
            throw new DalDoesNotExistException($"Task with ID = {id} does not exist");

        if (!item.isActive)
            throw new DalDeletionImpossible($"Task with ID = {id} is allready deleted (deactivated)");

        Task updatedItem = item with { isActive = false };

        taskslist.Remove(item);
        taskslist.Add(updatedItem);

        XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
    }

    public Task? Read(int id)
    {
        List<Task> taskslist = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        Task? item = taskslist.FirstOrDefault(task => task.Id == id);

        if (item == null)
            throw new DalDoesNotExistException($"Task with ID = {id} does not exist");

        if (!item.isActive)
            throw new DalDeletionImpossible($"Task with ID = {id} is allready deleted (deactivated)");

        return item;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        IEnumerable<Task> taskslist = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        return taskslist.FirstOrDefault(filter);
    }

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null)
    {
        IEnumerable<Task> taskslist = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        if (filter == null)
        {
            return taskslist;
        }
        else
        {
            return taskslist.Where(filter);
        }
        
    }

    public void Update(Task item)
    {
        List<Task> taskslist = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        Task? found = taskslist.Find(task => (item.Id == task.Id && task.isActive == true));

        if (found == null)
            throw new DalDoesNotExistException($"Task with ID = {item.Id} does not exist");

        taskslist.Remove(found);
        taskslist.Add(item);

        XMLTools.SaveListToXMLSerializer<Task>(taskslist, s_tasks_xml);
    }
}
