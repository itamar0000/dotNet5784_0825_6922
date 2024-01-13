namespace Dal;
using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

internal class EngineerImplementation : IEngineer
{
/// <summary>
///  check if item already exists if it doesnt it add this item to the list and create it his id
/// </summary>
/// <exception cref="NullReferenceException">"Object from type Engineer with this ID already exsists"</exception>
    public int Create(Engineer item)
    {
       
        if (!(Read(item.Id)==null)) { throw new DalAlreadyExistsException("\"Object from type Engineer with this ID already exsists\""); }
        DataSource.Engineers.Add(item); 
        return item.Id;
    }       

    /// <summary>
    /// checks if and item with this id exists if it is deletes it otherwise throw exception
    /// </summary>
    public void Delete(int id)
    {
        Engineer? a = Read(id);
        if (a != null)
        {
            DataSource.Engineers.Remove(a);
        }
        else { throw new DalDoesNotExistException($"Engineer with ID = {id} does not exist"); }
    }

    /// <summary>
    /// checks if an item with this id exists if it is returns the item otherwise return null
    /// </summary>
    public Engineer? Read(int  id)
    {
       return DataSource.Engineers.FirstOrDefault(item => item.Id == id);
    }

    /// <summary>
    /// copy the list into another list
    /// </summary>
    public IEnumerable<DO.Engineer?> ReadAll(Func<Engineer?,bool>?filter=null)
    {
       if(filter==null)
        {
            return DataSource.Engineers.Select(a => a);
        }
        return DataSource.Engineers.Where(filter);
      
    }

    /// <summary>
    /// checks if this engineer exists if it is update its values otherwise throw exception
    /// </summary>
    public void Update(Engineer item)
    {
        Engineer? a = Read(item.Id);
        if ( a== null)
        {
            throw new DalDoesNotExistException($"Dependency with ID = {item.Id} does not exist");
        }
        Delete(a.Id);
        Create(item);
    }
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }
}