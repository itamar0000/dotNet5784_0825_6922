namespace Dal;
using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
       
        if (!(Read(item.Id)==null)) { throw new NullReferenceException("\"Object from type Engineer with this ID already exsists\""); }
        DataSource.Engineers.Add(item); 
        return item.Id;
    }       
    public void Delete(int id)
    {
        Engineer? a = Read(id);
        if (a != null)
        {
            DataSource.Engineers.Remove(a);
        }
        else { throw new Exception($"Dependency with ID={id} already exist"); }
    }

    public Engineer? Read(int  id)
    {
       return DataSource.Engineers.Find(a =>(a.Id==id));
    
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? a = Read(item.Id);
        if ( a== null)
        {
            throw new Exception($"Dependency with ID={item.Id} does not exist");
        }
        Delete(a.Id);
        Create(a);
    }
}
