using BlApi;
using BO;
using DalApi;
using System.Runtime.InteropServices;

namespace BlImplementation;

/// <summary>
/// Represents the implementation of the task-related business logic.
/// </summary>
internal class TaskImplementation : BlApi.ITask
{
    private IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="item">The task to create.</param>
    /// <returns>The ID of the newly created task.</returns>
    public int Create(BO.Task item)
    {
        /*   foreach(var dependency in item.Dependencies)
           {
               DO.Task help= _dal.Task.Read(dependency.Id);

           }*/
        if (item.Alias == "")
        {
            throw new BO.BlInvalidInputException("Alias cannot be null");
        }
        DO.Task task = new DO.Task
            (Id: item.Id,
            Alias: item.Alias,
            Description: item.Description,
             CreatedAtDate: item.CreatedAtDate,
             IsMileStone: getIsMilestone(item),
             isActive: true,
             ScheduledDate: item.ScheduledDate,
             StartDate: item.StartDate,
             CompleteDate: item.CompleteDate,
             DeadlineDate: item.DeadlineDate,
             Deliverables: item.Deliverables,
             Remarks: item.Remarks,
             EngineerId: item.Engineer?.Id,
             Complexity: (DO.EngineerExperience?)item.Complexity
             );
        int id;
        try
        {
            id = _dal.Task.Create(task);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} already exists", ex);
        }
        return id;

    }

    /// <summary>
    /// Deletes a task.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <returns>An integer indicating the result of the deletion.</returns>
    public int Delete(int id)
    {
        DO.Task? task = _dal.Task.Read(id);

        // Task not found or already inactive
        if (task == null||task.isActive==false)
            throw new  BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        // Checking if task has dependencies
        bool tasks =_dal.Dependency.ReadAll().Where(t =>t!.DependensOnTask==id).Any();

        // Deleting task if no dependencies exist
        if (tasks == false)
            try {
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new BlDeletionImpossible($"Task with ID={id} cannot be deleted", ex);
            }
        else
            throw new BlDeletionImpossible($"Task with ID={id}cannot be deleted");
        return 0;
    }

    /// <summary>
    /// Reads a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to read.</param>
    /// <returns>The task with the specified ID.</returns>
    public BO.Task Read(int id)

    {
        DO.Task? item = _dal.Task.Read(id);
        if (item == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return new BO.Task()
        {
            Id = item.Id,
            Alias = item.Alias,
            Description = item.Description,
            CreatedAtDate = item.CreatedAtDate,
            ScheduledDate = item.ScheduledDate,
            StartDate = item.StartDate,
            CompleteDate = item.CompleteDate,
            ForecastDate = getForecastDate(item),
            Deliverables = item.Deliverables,
            RequiredEffortTime = item.RequiredEffortTime,
            Remarks = item.Remarks,
            Complexity = (BO.EngineerExperience?)item.Complexity
        };
    }

    /// <summary>
    /// Reads all tasks that match the specified filter.
    /// </summary>
    /// <param name="filter">A function to filter tasks.</param>
    /// <returns>A collection of tasks that match the filter.</returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter = null)
    {
        if(filter!=null)
        {
            var tasks= _dal.Task.ReadAll().Where(item=>item!.isActive).Select(item => new BO.Task()
            {
                Id = item!.Id,
                Alias = item.Alias,
                Description = item.Description,
                CreatedAtDate = item.CreatedAtDate,
                ScheduledDate = item.ScheduledDate,
                StartDate = item.StartDate,
                CompleteDate = item.CompleteDate,
                ForecastDate = getForecastDate(item),
                Deliverables = item.Deliverables,
                RequiredEffortTime = item.RequiredEffortTime,
                Remarks = item.Remarks,
                Status = getStatus(item),
                Dependencies = getDependencies(item),
                Complexity = (BO.EngineerExperience?)item.Complexity
            });
           return tasks.Where(item => filter(item));
   
        }
        return _dal.Task.ReadAll().Select(item => new BO.Task()
        {
            Id = item!.Id,
            Alias = item.Alias,
            Description = item.Description,
            CreatedAtDate = item.CreatedAtDate,
            ScheduledDate = item.ScheduledDate,
            StartDate = item.StartDate,
            CompleteDate = item.CompleteDate,
            ForecastDate = getForecastDate(item),
            Deliverables = item.Deliverables,
            RequiredEffortTime = item.RequiredEffortTime,
            Remarks = item.Remarks,
            Status = getStatus(item),
            Dependencies = getDependencies(item),
            Complexity = (BO.EngineerExperience?)item.Complexity
        });
    }

    /// <summary>
    /// Updates a task.
    /// </summary>
    /// <param name="item">The task to update.</param>
    public void Update(BO.Task item)
    {
        if (item.Alias == "")
        {
            throw new BO.BlInvalidInputException("Alias cannot be null");
        }
        if (item.Id <= 0)
        {
            throw new BO.BlInvalidInputException("Id cannot be negative");

        }
        DO.Task task = new DO.Task
            (Id: item.Id,
             Alias: item.Alias,
             Description: item.Description,
             CreatedAtDate: item.CreatedAtDate,
             IsMileStone: getIsMilestone(item),
             isActive: true,
             ScheduledDate: item.ScheduledDate,
             StartDate: item.StartDate,
             CompleteDate: item.CompleteDate,
             DeadlineDate: item.DeadlineDate,
             Deliverables: item.Deliverables,
             Remarks: item.Remarks,
             EngineerId: item.Engineer?.Id,
             Complexity: (DO.EngineerExperience?)item.Complexity
             );
        if (item.Dependencies != getDependencies(task))
        {
            _dal.Dependency.ReadAll(items => items.DependentTask == item.Id).ToList().ForEach(items => _dal.Dependency.Delete(items!.Id));
            foreach (var dependency in item.Dependencies)
            {
                DO.Dependency dep = new DO.Dependency
                {
                    DependentTask = item.Id,
                    DependensOnTask = dependency.Id
                };
                _dal.Dependency.Create(dep);
            }
        }
        try
        {
            _dal.Task.Update(task);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} already exists", ex);
        }
    }

    /// <summary>
    /// Determines whether a task is a milestone.
    /// </summary>
    /// <param name="item">The task to check.</param>
    /// <returns>True if the task is a milestone; otherwise, false.</returns>
    private bool getIsMilestone(BO.Task item)
    {
        return item.Milestone?.Id == null;
    }

    /// <summary>
    /// Gets the status of a task.
    /// </summary>
    /// <param name="item">The task to get the status for.</param>
    /// <returns>The status of the task.</returns>
    public BO.Status? getStatus(DO.Task? item)
    { 
        if (item!.CompleteDate != null)
            return BO.Status.Done;
        if (item.StartDate != null)
            return BO.Status.OnTrack;
        if (item.ScheduledDate != null)
            return BO.Status.Scheduled;
        return BO.Status.Unscheduled;
    }

    /// <summary>
    /// Retrieves dependencies for a task.
    /// </summary>
    /// <param name="item">The task to retrieve dependencies for.</param>
    /// <returns>A list of dependencies for the task.</returns>
    private List<BO.TaskInList>? getDependencies(DO.Task item)
    {
        return _dal.Dependency.ReadAll(d => d!.DependentTask == item.Id).Select(d => new BO.TaskInList()
        {
            Id = (int)d.DependensOnTask!,
            Alias = _dal.Task.Read((int)d.DependensOnTask)!.Alias,
            Description = _dal.Task.Read((int)d.DependensOnTask)!.Description,
            Status = getStatus(_dal.Task.Read((int)d.DependensOnTask))
        }).ToList();
    }

    /// <summary>
    /// Updates the scheduled date of a task.
    /// </summary>
    /// <param name="item">The task to update the scheduled date for.</param>
    public void Update(int id, DateTime? date)
    {
        DO.Task? task = _dal.Task.Read(id);

        if (task == null)
            throw new BO.BlDoesNotExistException($"Task with ID = {id} does Not exist");

        bool notScheduled = (from dependency in getDependencies(task)
                             where dependency.Status != Status.Scheduled
                             select dependency).Any();
        if (notScheduled)
            throw new BlInvalidInputException($"Task date = {date} not valid");
        task = task with { ScheduledDate = date };
        _dal.Task.Update(task);

    }

    /// <summary>
    /// Reads a task based on a filter function.
    /// </summary>
    /// <param name="filter">The filter function.</param>
    /// <returns>The task that matches the filter.</returns>
    public BO.Task? Read(Func<BO.Task?, bool>? filter )
    {
        return _dal.Task.ReadAll().Select(item => new BO.Task()
        {
            Id = item.Id,
            Alias = item.Alias,
            Description = item.Description,
            CreatedAtDate = item.CreatedAtDate,
            ScheduledDate = item.ScheduledDate,
            StartDate = item.StartDate,
            CompleteDate = item.CompleteDate,
            ForecastDate = getForecastDate(item),
            Deliverables = item.Deliverables,
            RequiredEffortTime = item.RequiredEffortTime,
            Remarks = item.Remarks,
            Status = getStatus(item),
            Dependencies = getDependencies(item),
            Complexity = (BO.EngineerExperience?)item.Complexity
        }).FirstOrDefault(item => filter(item));
    }

    /// <summary>
    /// Calculates the earliest date among the dependencies of a task.
    /// </summary>
    /// <param name="item">The task to calculate the earliest date for.</param>
    /// <returns>The earliest date among the dependencies of the task.</returns>
    private DateTime? EarliestDate(BO.Task item)
    {
        if(item.ScheduledDate.HasValue)
            return item.ScheduledDate;
        //gets a list of all the dependencies of the task means all the task i depeneds on
        IEnumerable<DO.Dependency?> deps = _dal.Dependency.ReadAll(items => items.DependentTask == item.Id);
        //sort the list by id
        deps.OrderBy(item=>item?.Id);
       if(!deps.Any())
        {//if it doesnt depend on anything return now
            return DateTime.Now ;
        }
       //gets a list of all the tasks i depeneds on
        var dependenttasks = deps.Select(items => _dal.Task.Read((int)items.DependensOnTask));

        //if (dependenttasks.Any())
        //    throw new BlNullPropertyException($"not all the tasks before has start date");
        
        return dependenttasks.Max(items => getForecastDate(items));
    }

    /// <summary>
    /// Sets the status of a task to "Scheduled" and updates its scheduled date.
    /// </summary>
    /// <param name="item">The task to set the status and scheduled date for.</param>
    public void SetScheduele(BO.Task item)
    {
        item.Status = BO.Status.Scheduled;

        Update(item.Id, EarliestDate(item));
     
    }

    /// <summary>
    /// Calculates the forecast date for a task based on its scheduled date and required effort time.
    /// </summary>
    /// <param name="item">The task to calculate the forecast date for.</param>
    /// <returns>The forecast date for the task.</returns>
    private DateTime? getForecastDate(DO.Task item)
    {
        return item.ScheduledDate + item.RequiredEffortTime;
    }
}
