namespace Dal;
using DalApi;
using System.Diagnostics;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer =>  new EngineerImplementation();
    public IDependency Dependency => new DependencyImplementation();
    public ITask Task => new TaskImplementation();

    public IClock Clock => new ClockImplementation();   

    public void DeleteAll()
    {
        Engineer.DeleteAll();
        Dependency.DeleteAll();
        Task.DeleteAll();
    }
}
