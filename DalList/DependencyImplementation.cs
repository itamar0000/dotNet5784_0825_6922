namespace Dal;
using DalApi;
using DO;

public class DependencyImplementation : IDependency
{
    /// <summary>
    ///  check if item already exists if it doesnt it add this item to the list
    /// </summary>
    public int Create(Dependency item)
    {
        int NewId = DataSource.Config.NextTaskId;
        Dependency copy=item with { Id=NewId };
        DataSource.Dependencys.Add(copy);
        return NewId;
    }

    /// <summary>
    /// check if item exists if it is delete it otherwise throw exception
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Dependency? a = Read(id);
        if (a==null) { throw new Exception($"Dependency with ID={id} does not exist"); }
        DataSource.Dependencys.Remove(a);
    }
    
    /// <summary>
    ///  check if the item with specific id exists and if it is returns the item else throw excpetion
    /// </summary>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.Find(x => x.Id == id);

    }

    /// <summary>
    /// copy the list into a new list
    /// </summary>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

/// <summary>
/// checks if item with the same id exists if it is deletes it and recreate it with updated values
/// </summary>
    public void Update(Dependency item)
    {
        Dependency? a = Read(item.Id);
        if (a == null)
        {
            throw new Exception($"Dependency with ID={item.Id} does not exist");
        }
        DataSource.Dependencys.Remove(a);
        DataSource.Dependencys.Add(item);
    }
}