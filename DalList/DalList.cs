using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IEngineer Engineer => new EngineerImplementation();
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
