namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Implementation of the Engineer interface
/// </summary>
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates a new engineer.
    /// </summary>
    /// <param name="boEngineer">The engineer object to create.</param>
    /// <returns>The ID of the newly created engineer.</returns>
    /// <exception cref="BO.BlAlreadyExistsException">Thrown if an engineer with the same ID already exists.</exception>
    public int Create(BO.Engineer boEngineer)
    {
        checkEngineer(boEngineer);

        DO.Engineer doEngineer = ConvertBoToDo(boEngineer);

        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID = {boEngineer.Id} already exists", ex);
        }
    }

    /// <summary>
    /// Deletes an engineer.
    /// </summary>
    /// <param name="id">The ID of the engineer to delete.</param>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the engineer does not exist.</exception>
    /// <exception cref="BO.BlDeletionImpossible">Thrown if deletion is impossible due to active tasks.</exception>
    public void Delete(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id)
            ?? throw new BO.BlDoesNotExistException($"Engineer with ID = {id} doe's not exists");

        bool flag = _dal.Task.ReadAll().Where(item => item.EngineerId == id).Select(item => new BO.Task()
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
            Status = getStatus(item),
            Dependencies = getDependencies(item),
            Complexity = (BO.EngineerExperience?)item.Complexity
        }).Where(item=>item.Status==Status.OnTrack||item.Status==Status.Done).Any();

        if (flag == true)
            throw new BO.BlDeletionImpossible($"Engineer with ID = {id} cannot be deleted");
        else
        {
            _dal.Engineer.Delete(id);

            /*try
            {
                _dal.Engineer.Delete(id);
            }
            catch (DO.DalAlreadyExistsException ex)
            {
                throw new BO.BlDoesNotExistException
            }*/
        }
    }

    /// <summary>
    /// Reads an engineer by ID.
    /// </summary>
    /// <param name="id">The ID of the engineer to read.</param>
    /// <returns>The engineer object.</returns>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the engineer does not exist.</exception>
    public BO.Engineer Read(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id)
            ?? throw new BO.BlDoesNotExistException($"Engineer with ID = {id} does not exists");

        return ConvertDoToBo(doEngineer);
    }

    /// <summary>
    /// Reads an engineer based on a filter.
    /// </summary>
    /// <param name="filter">The filter condition.</param>
    /// <returns>The engineer object that matches the filter condition, if any.</returns>
    public BO.Engineer? Read(Func<BO.Engineer?, bool>? filter)
    {
        BO.Engineer? boEngineer = _dal.Engineer.ReadAll().Select(item => ConvertDoToBo(item)).FirstOrDefault(filter);

        return boEngineer;
    }

    /// <summary>
    /// Reads all engineers optionally filtered by a condition.
    /// </summary>
    /// <param name="filter">The filter condition.</param>
    /// <returns>A collection of engineer objects.</returns>
    public IEnumerable<BO.Engineer>? ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        var boEngineers = _dal.Engineer.ReadAll().Select(item => ConvertDoToBo(item));

        if(filter != null)
        {
            boEngineers.Where(item => filter(item));
        }

        return boEngineers;
    }

    /// <summary>
    /// Updates an existing engineer.
    /// </summary>
    /// <param name="boEngineer">The engineer object to update.</param>
    /// <exception cref="BO.BlInvalidInputException">Thrown if the input engineer data is invalid.</exception>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the engineer does not exist.</exception>
    public void Update(BO.Engineer boEngineer)
    {
        checkEngineer(boEngineer);

        DO.Engineer doEngineer = ConvertBoToDo(boEngineer);

        if ((DO.EngineerExperience)boEngineer.Level < _dal.Engineer.Read(boEngineer.Id).Level)
            throw new BO.BlInvalidInputException($"Engineer's Level can only go up");

         
            var tasks = _dal.Task.ReadAll(task => task.EngineerId == boEngineer.Id);

            if ((tasks.Any(task => task.isActive == true && task.CompleteDate is null && task.StartDate is not null)))
                throw new BO.BlInvalidInputException($"Engineer's Task can not be changed because he in a middle of another task");

        /* if((DO.EngineerExperience)boEngineer.Level < _dal.Task.Read(boEngineer.Task.Id)?.Complexity)
             throw new BO.BlInvalidInputException($"Engineer's Task can not be changed because the complexity's task is to high");*/

        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID = {boEngineer.Id} does not exists", ex);
        }
    }

    /// <summary>
    /// Checks if the engineer data is valid.
    /// </summary>
    /// <param name="boEngineer">The engineer object to validate.</param>
    /// <exception cref="BO.BlInvalidInputException">Thrown if the engineer data is invalid.</exception>
    private static void checkEngineer(Engineer boEngineer)
    {
        if (boEngineer.Id <= 0)
            throw new BO.BlInvalidInputException("Engineer can't be with unpositive Id");

        if (boEngineer.Name == "")
            throw new BO.BlInvalidInputException("Engineer can't be with an empty Name");

        if (boEngineer.Cost <= 0)
            throw new BO.BlInvalidInputException("Engineer can't be with unpositive Cost");

        if (!new EmailAddressAttribute().IsValid(boEngineer.Email))
            throw new BO.BlInvalidInputException("Engineer's Mail is invalid");
    }

    /// <summary>
    /// Converts a business object engineer to a data object engineer.
    /// </summary>
    /// <param name="boEngineer">The business object engineer to convert.</param>
    /// <returns>The data object engineer.</returns>
    private DO.Engineer ConvertBoToDo(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
                (Id: boEngineer.Id,
                Name: boEngineer.Name,
                Email: boEngineer.Email,
                Cost: boEngineer.Cost,
                Level: (DO.EngineerExperience)boEngineer.Level);

        return doEngineer;
    }

    /// <summary>
    /// Converts a data object engineer to a business object engineer.
    /// </summary>
    /// <param name="doEngineer">The data object engineer to convert.</param>
    /// <returns>The business object engineer.</returns>
    private BO.Engineer ConvertDoToBo(DO.Engineer doEngineer)
    {
        BO.Engineer boEngineer = new()
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Cost = doEngineer.Cost,
            Level = (BO.EngineerExperience)doEngineer.Level
        };



        int minID = 0;
        DateTime? min = DateTime.MaxValue;

        foreach (var item in _dal.Task.ReadAll())
        {
            if (item.EngineerId == boEngineer.Id && item.ScheduledDate < min && item.CompleteDate == null)
            {
                min = item.ScheduledDate;
                minID = item.Id;
            }
            
        }

        if (minID != 0)
        {
            boEngineer.Task = new BO.TaskInEngineer()
            {
                Id = minID,
                Alias = _dal.Task.Read(minID).Alias,
            };
        }
        
        return boEngineer;
    }

    /// <summary>
    /// Assigns a task to an engineer.
    /// </summary>
    /// <param name="engineerId">The ID of the engineer.</param>
    /// <param name="taskId">The ID of the task.</param>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if either the engineer or the task does not exist.</exception>
    /// <exception cref="BO.BlInvalidInputException">Thrown if the task is already assigned to an engineer or if the task is completed.</exception>
    public void Assign(int engineerId, int taskId)
    {
        DO.Engineer? engineer = _dal.Engineer.Read(engineerId);
        DO.Task? task = _dal.Task.Read(taskId);
        if(engineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID = {engineerId} does not exists");
       if(task==null)
            throw new BO.BlDoesNotExistException($"Task with ID = {taskId} does not exists");
       if (task.EngineerId != null)
            throw new BO.BlInvalidInputException($"Task with ID = {taskId} already assigned to an engineer");
        if (task.CompleteDate != null)
            throw new BO.BlInvalidInputException($"Task with ID = {taskId} already completed");
        task=task with { EngineerId = engineerId };
        _dal.Task.Update(task);
        Update(ConvertDoToBo(engineer));
    }

    
    /// <summary>
    /// Gets the status of a task.
    /// </summary>
    /// <param name="item">The task.</param>
    /// <returns>The status of the task.</returns>
    private BO.Status? getStatus(DO.Task? item)
    {
        if (item.CompleteDate != null)
            return BO.Status.Done;
        if (item.StartDate != null)
            return BO.Status.OnTrack;
        if (item.ScheduledDate != null)
            return BO.Status.Scheduled;
        return BO.Status.Unscheduled;
    }

    /// <summary>
    /// Gets the dependencies of a task.
    /// </summary>
    /// <param name="item">The task.</param>
    /// <returns>A list of task dependencies.</returns>
    private List<BO.TaskInList>? getDependencies(DO.Task item)
    {
        var dependencies = _dal.Dependency.ReadAll().Where(d => d.DependensOnTask == item.Id).Select(d => new BO.TaskInList()
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