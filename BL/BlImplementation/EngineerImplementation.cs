
namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;

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

        if (!boEngineer.Email.Contains('@'))
            throw new BO.BlInvalidInputException("Engineer's Mail must containe '@'");

        if (boEngineer.Email.Contains(" "))
            throw new BO.BlInvalidInputException("Engineer's Mail must not containe ' '");

        DO.Engineer doEngineer = ConvertBoToDo(boEngineer);

        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }

    }


    public int Delete(int id)
    {
        BO.Engineer boEngineer = Read(id);
        if (boEngineer.Task)
        throw new NotImplementedException();
    }

    public Engineer Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public int Update(Engineer item)
    {
        throw new NotImplementedException();
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


}