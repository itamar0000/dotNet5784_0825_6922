namespace Dal;
using DalApi;
using DO;

internal class DependencyImplementation : IDependency
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
        if (a==null) { throw new DalDoesNotExistException($"Dependency with ID = {id} does not exist"); }
        DataSource.Dependencys.RemoveAll(x=>x.Id==id);
    }
    
    /// <summary>
    ///  check if the item with specific id exists and if it is returns the item else throw excpetion
    /// </summary>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.FirstOrDefault(item => item.Id == id); ;

    }

    /// <summary>
    /// copy the list into a new list
    /// </summary>
    public IEnumerable<DO.Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Dependencys.Select(item => item);
        else
            return DataSource.Dependencys.Where(filter);

    }

    /// <summary>
    /// checks if item with the same id exists if it is deletes it and recreate it with updated values
    /// </summary>
    public void Update(Dependency item)
    {
        Dependency? a = Read(item.Id);
        if (a == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID = {item.Id} does not exist");
        }
        DataSource.Dependencys.Remove(a);
        DataSource.Dependencys.Add(item);
    }

    /// <summary>
    /// the function gets a function and retrun the first value that suits the criterion else return deafualt
    /// </summary>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencys.FirstOrDefault(filter);
    }

    /// <summary>
    /// delete all the database
    /// </summary>
    public void DeleteAll()
    {
        DataSource.Dependencys.Clear();
    }
}