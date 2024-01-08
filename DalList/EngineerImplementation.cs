namespace Dal;
using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

public class EngineerImplementation : IEngineer
{/// <summary>
///  check if item already exists if it doesnt it add this item to the list and create it his id
/// </summary>
/// <param name="item"></param>
/// <returns></returns>
/// <exception cref="NullReferenceException"></exception>
    public int Create(Engineer item)
    {
       
        if (!(Read(item.Id)==null)) { throw new NullReferenceException("\"Object from type Engineer with this ID already exsists\""); }
        DataSource.Engineers.Add(item); 
        return item.Id;
    }       
    /// <summary>
    /// checks if and item with this id exists if it is deletes it otherwise throw exception
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Engineer? a = Read(id);
        if (a != null)
        {
            DataSource.Engineers.Remove(a);
        }
        else { throw new Exception($"Engineer with ID={id} already exist"); }
    }
    /// <summary>
    /// checks if an item with this id exists if it is returns the item otherwise return null
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Engineer? Read(int  id)
    {
       return DataSource.Engineers.Find(a =>(a.Id==id));
    
    }
    /// <summary>
    /// copy the list into another list
    /// </summary>
    /// <returns></returns>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }
    /// <summary>
    /// checks if this engineer exists if it is update its values otherwise throw exception
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Engineer item)
    {
        Engineer? a = Read(item.Id);
        if ( a== null)
        {
            throw new Exception($"Dependency with ID={item.Id} does not exist");
        }
        Delete(a.Id);
        Create(item);
    }
}
