
using DalApi;

namespace Dal;

sealed public class DalXml : IDal
{
    public IEngineer Engineer =>  new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public ITask Task => new TaskImplementation();
}
