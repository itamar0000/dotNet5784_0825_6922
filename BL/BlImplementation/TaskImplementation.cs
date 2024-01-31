using BlApi;
using DalApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private IDal _dal = DalApi.Factory.Get; 
    public int Create(BO.Task item)
    {
        
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
            DeadlineDate = item.DeadlineDate,
            Deliverables = item.Deliverables,
            RequiredEffortTime = item.RequiredEffortTime,
            Remarks = item.Remarks,
            Complexity = (BO.EngineerExperience?)item.Complexity
        };
    }

    public IEnumerable<BO.Task> ReadAll()
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
            DeadlineDate = item.DeadlineDate,
            Deliverables = item.Deliverables,
            RequiredEffortTime = item.RequiredEffortTime,
            Remarks = item.Remarks,
            Status=getStatus(item),
            Dependencies=getDependencies(item),
            Complexity = (BO.EngineerExperience?)item.Complexity
        });
    }

    public int Update(BO.Task item)
    {
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

        int id;
        try
        {
            _dal.Task.Update(task);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} already exists", ex);
        }
        return ;
    }
   public bool getIsMilestone(BO.Task item)
    {
        return item.Milestone.Id == null;
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
      var dependencies=_dal.Dependency.ReadAll().Where(d => d.DependensOnTask == item.Id).Select(d => new BO.TaskInList()
        {
            Id = d.Id,
            Alias = _dal.Task.Read(d.Id).Alias,
            Description = _dal.Task.Read(d.Id).Description,
            Status = getStatus(_dal.Task.Read(d.Id)),
        });
        var dependenciesList = dependencies.ToList();
        return dependenciesList;
    }
    
}
