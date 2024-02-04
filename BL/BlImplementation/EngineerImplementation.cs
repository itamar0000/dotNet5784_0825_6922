
namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
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


    public void Delete(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id)
            ?? throw new BO.BlDoesNotExistException($"Engineer with ID = {id} doe's not exists");

        bool tasks = _dal.Task.ReadAll().Where(item => item.EngineerId == id).Any();
        if (tasks == true)
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

    public BO.Engineer Read(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id)
            ?? throw new BO.BlDoesNotExistException($"Engineer with ID = {id} does not exists");

        return ConvertDoToBo(doEngineer);
    }

    public BO.Engineer? Read(Func<BO.Engineer?, bool>? filter)
    {
        BO.Engineer? boEngineer = _dal.Engineer.ReadAll().Select(item => ConvertDoToBo(item)).FirstOrDefault(filter);

        return boEngineer;
    }

    public IEnumerable<BO.Engineer>? ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        var boEngineers = _dal.Engineer.ReadAll().Select(item => ConvertDoToBo(item));

        if(filter != null)
        {
            boEngineers.Where(item => filter(item));
        }

        return boEngineers;
    }

    public void Update(BO.Engineer boEngineer)
    {
        checkEngineer(boEngineer);

        DO.Engineer doEngineer = ConvertBoToDo(boEngineer);

        if ((DO.EngineerExperience)boEngineer.Level < _dal.Engineer.Read(boEngineer.Id).Level)
            throw new BO.BlInvalidInputException($"Engineer's Level can only go up");

         
            var tasks = _dal.Task.ReadAll(task => task.EngineerId == boEngineer.Id);

            if ((tasks.Any(task => task.isActive == true && task.CompleteDate is null && task.StartDate is not null)))
                throw new BO.BlInvalidInputException($"Engineer's Task can not be changed because he in a middle of another task");

        try
        {        
            _dal.Engineer.Update(doEngineer);

            DO.Task tas = _dal.Task.Read(task => task.Id == boEngineer.Task.Id) with { EngineerId = boEngineer.Id };

            _dal.Task.Update(tas);            
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID = {boEngineer.Id} does not exists", ex);
        }
    }




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

    private BO.Engineer ConvertDoToBo(DO.Engineer doEngineer)
    {
        BO.Engineer boEngineer = new BO.Engineer
            (Id: doEngineer.Id,
             Name: doEngineer.Name,
             Email: doEngineer.Email,
             Cost: doEngineer.Cost,
             Level: (DO.EngineerExperience)doEngineer.Level);

        BO.TaskInEngineer? task = (from item in _dal.Task.ReadAll()
                                   where (item.EngineerId == boEngineer.Id &&
                                   item.StartDate <= DateTime.Now &&
                                   item.CompleteDate == null)
                                   select new BO.TaskInEngineer
                                   {
                                       Id = item.Id,
                                       Alias = item.Alias,
                                   }).FirstOrDefault();
        if (task != null)
            boEngineer.Task = task;

        return boEngineer;
    }

    
}