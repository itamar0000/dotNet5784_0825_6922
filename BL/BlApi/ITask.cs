

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public int Delete(int id);
    public void Update(BO.Task item);
    public void Update(int id, DateTime date);
    public BO.Task Read(int id);
    public IEnumerable<BO.Task> ReadAll();
}
