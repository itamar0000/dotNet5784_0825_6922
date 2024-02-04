
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
        if (boEngineer.Id <= 0)
            throw new BO.BlInvalidInputException("Engineer can't be with unpositive Id");

        if (boEngineer.Name == "")
            throw new BO.BlInvalidInputException("Engineer can't be with an empty Name");

        if (boEngineer.Cost <= 0)
            throw new BO.BlInvalidInputException("Engineer can't be with unpositive Cost");

        /*if (!boEngineer.Email.Contains('@'))
            throw new BO.BlInvalidInputException("Engineer's Mail must containe '@'");

        if (boEngineer.Email.Contains(" "))
            throw new BO.BlInvalidInputException("Engineer's Mail must not containe ' '"); */

        if (!new EmailAddressAttribute().IsValid(boEngineer.Email))
            throw new BO.BlInvalidInputException("Engineer's Mail is invalid");


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
            throw new BO.BlDeletionImpossible($"Task with ID = {id} cannot be deleted");
        else
            _dal.Engineer.Delete(id);
    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id)
            ?? throw new BO.BlDoesNotExistException($"Engineer with ID = {id} does not exists");

        BO.Engineer doEngineer = 
    }

    public IEnumerable<BO.Engineer> ReadAll()
    {
        return _dal.Engineer.ReadAll().Select(item => ConvertDoToBo(item));
    }

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0)
            throw new BO.BlInvalidInputException("Engineer can't be with unpositive Id");

        if (boEngineer.Name == "")
            throw new BO.BlInvalidInputException("Engineer can't be with an empty Name");

        if (boEngineer.Cost <= 0)
            throw new BO.BlInvalidInputException("Engineer can't be with unpositive Cost");   

        if (!new EmailAddressAttribute().IsValid(boEngineer.Email))
            throw new BO.BlInvalidInputException("Engineer's Mail is invalid");

        /////////////////////// לעדכן משימההההההה ///////////////////////

        DO.Engineer doEngineer = ConvertBoToDo(boEngineer);

        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID = {boEngineer.Id} does not exists", ex);
        }
    }



    private static DO.Engineer ConvertBoToDo(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
                (Id: boEngineer.Id,
                Name: boEngineer.Name,
                Email: boEngineer.Email,
                Cost: boEngineer.Cost,
                Level: (DO.EngineerExperience)boEngineer.Level);

        return doEngineer;
    }

    private static BO.Engineer ConvertDoToBo(DO.Engineer doEngineer)
    {
        BO.Engineer boEngineer = new BO.Engineer
            (Id: doEngineer.Id,
             Name: doEngineer.Name,
             Email: doEngineer.Email,
             Cost: doEngineer.Cost,
             Level: (DO.EngineerExperience)doEngineer.Level);
        boEngineer.Task();

        return boEngineer;
    }
}