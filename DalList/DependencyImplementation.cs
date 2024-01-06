namespace Dal;
using DalApi;
using DO;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int NewId = DataSource.Config.NextTaskId;
        Dependency copy=item with { Id=NewId };
        DataSource.Dependencys.Add(copy);
        return NewId;
    }

    public void Delete(int id)
    {
        Dependency? a = Read(id);
        if (a==null) { throw new Exception($"Dependency with ID={id} does not exist"); }
        DataSource.Dependencys.Remove(a);
    }
    

    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.Find(x => x.Id == id);

    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    public void Update(Dependency item)
    {
        Dependency? a = Read(item.Id);
        if (a == null)
        {
            throw new Exception($"Dependency with ID={item.Id} does not exist");
        }
        Delete(a.Id);
        Create(a);
    }
}
