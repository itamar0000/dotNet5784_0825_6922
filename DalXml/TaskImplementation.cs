namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";
    readonly string s_config_xml = "data-config";

    /// <summary>
    /// Creates a new task and inserts it into the XML file.
    /// </summary>
    /// <param name="item">The task to be created.</param>
    /// <returns>The ID of the newly created task.</returns>
    public int Create(Task item)
    {
        // Get the next ID from the XML file
        int id = Config.NextTaskId;

        var tasksList = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task newItem = item with { Id = id, CreatedAtDate = DateTime.Now };

        tasksList.Add(newItem);

        XMLTools.SaveListToXMLSerializer<Task>(tasksList, s_tasks_xml);

        // Return the ID of the new task
        return id;
    }

    /// <summary>
    /// Deletes a task with the specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the task to be deleted.</param>
    public void Delete(int id)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        Task? item = tasksList.Find(task => task.Id == id);

        if (item == null)
            throw new DalDoesNotExistException($"Task with ID = {id} does not exist");

        if (!item.isActive)
            throw new DalDeletionImpossible($"Task with ID = {id} is already deleted (deactivated)");

        Task updatedItem = item with { isActive = false };

        tasksList.Remove(item);
        tasksList.Add(updatedItem);

        XMLTools.SaveListToXMLSerializer<Task>(tasksList, s_tasks_xml);
    }

    /// <summary>
    /// Deletes all tasks from the XML file.
    /// </summary>
    public void DeleteAll()
    {
        List<Task> tasksList = new List<Task>();
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, s_tasks_xml);
        XElement configList = XMLTools.LoadListFromXMLElement(s_config_xml);
        configList.Element("NextTaskId").Value = "1";
        XMLTools.SaveListToXMLElement(configList, s_config_xml);
    }

    /// <summary>
    /// Reads a task with the specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the task to be read.</param>
    /// <returns>The read task.</returns>
    public Task? Read(int id)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        Task? item = tasksList.FirstOrDefault(task => task.Id == id);

        if (item == null)
            throw new DalDoesNotExistException($"Task with ID = {id} does not exist");

        if (!item.isActive)
            throw new DalDeletionImpossible($"Task with ID = {id} is already deleted (deactivated)");

        return item;
    }

    /// <summary>
    /// Reads a task based on the provided filter from the XML file.
    /// </summary>
    /// <param name="filter">The filter predicate for reading the task.</param>
    /// <returns>The read task.</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        IEnumerable<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        return tasksList.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all tasks from the XML file based on the provided filter.
    /// </summary>
    /// <param name="filter">The filter predicate for reading tasks.</param>
    /// <returns>The collection of read tasks.</returns>
    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null)
    {
        IEnumerable<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        if (filter == null)
        {
            return tasksList;
        }
        else
        {
            return tasksList.Where(filter);
        }
    }

    /// <summary>
    /// Updates an existing task in the XML file.
    /// </summary>
    /// <param name="item">The task to be updated.</param>
    public void Update(Task item)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        Task? found = tasksList.Find(task => (item.Id == task.Id && task.isActive
        == true));

        if (found == null)
            throw new DalDoesNotExistException($"Task with ID = {item.Id} does not exist");

        tasksList.Remove(found);
        tasksList.Add(item);

        XMLTools.SaveListToXMLSerializer<Task>(tasksList, s_tasks_xml);
    }

  
}