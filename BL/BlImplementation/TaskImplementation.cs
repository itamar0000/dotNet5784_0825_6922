using BlApi;
using BO;
using DalApi;
using System.Runtime.InteropServices;

namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private IDal _dal = DalApi.Factory.Get;
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

    public int Delete(int id)
    {
        DO.Task? task = _dal.Task.Read(id);
        if(task == null||task.isActive==false)
            throw new  BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        bool tasks=_dal.Dependency.ReadAll().Where(t =>t.DependensOnTask==id).Any();
        if (tasks == false)
            try {
                Delete(id); } catch (Exception ex){ throw new BlDeletionImpossible($"Task with ID={id} cannot be deleted", ex); }
        else
            throw new BlDeletionImpossible($"Task with ID={id}cannot be deleted");
        return 0;
    }

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

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter = null)
    {
        if(filter!=null)
        {
            var tasks= _dal.Task.ReadAll().Where(item=>item.isActive).Select(item => new BO.Task()
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
            });
           return tasks.Where(item => filter(item));
   
        }
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
        });
    }

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
             EngineerId: item.Engineer.Id,
             Complexity: (DO.EngineerExperience?)item.Complexity
             );
       if(item.Dependencies!=getDependencies(task))
        {
           _dal.Dependency.ReadAll(items => items.DependentTask == item.Id).ToList().ForEach(items => _dal.Dependency.Delete(items.Id));
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
        int id;
        try
        {
            _dal.Task.Update(task);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} already exists", ex);
        }
    }
    public bool getIsMilestone(BO.Task item)
    {
        return item.Milestone?.Id == null;
    }
    public BO.Status? getStatus(DO.Task? item)
    {
        if (item.CompleteDate != null)
            return BO.Status.Done;
        if (item.StartDate != null)
            return BO.Status.OnTrack;
        if (item.ScheduledDate != null)
            return BO.Status.Scheduled;
        return BO.Status.Unscheduled;
    }
    public List<BO.TaskInList>? getDependencies(DO.Task item)
    {
        return _dal.Dependency.ReadAll(d => d.DependentTask == item.Id).Select(d => new BO.TaskInList()
        {
            Id = (int)d.DependensOnTask!,
            Alias = _dal.Task.Read((int)d.DependensOnTask).Alias,
            Description = _dal.Task.Read((int)d.DependensOnTask).Description,
            Status = getStatus(_dal.Task.Read((int)d.DependensOnTask))
        }).ToList();
    }
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

    private DateTime? EarliestDate(BO.Task item)
    {
        if(item.ScheduledDate.HasValue)
            return item.ScheduledDate;
        IEnumerable<DO.Dependency> deps = _dal.Dependency.ReadAll(items => items.DependentTask == item.Id);
       if(!deps.Any())
        {
            return DateTime.Now ;
        }
        var tasks = _dal.Task.ReadAll();
        var dependenttasks = deps.Select(items => _dal.Task.Read((int)items.DependensOnTask));
        //if (dependenttasks.Any())
        //    throw new BlNullPropertyException($"not all the tasks before has start date");
        return dependenttasks.Max(items => getForecastDate(items));


    }

    public void SetScheduele(BO.Task item)
    {
        item.Status = BO.Status.Scheduled;
        Update(item.Id, EarliestDate(item));
     
    }
    private DateTime? getForecastDate(DO.Task item)
    {
        return item.ScheduledDate + item.RequiredEffortTime;
    }
}
