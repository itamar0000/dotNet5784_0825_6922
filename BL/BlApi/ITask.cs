

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public int Delete(int id);
    public int Update(BO.Task item);
    public BO.Task Read(int id);
    public IEnumerable<BO.Task> ReadAll();
}
