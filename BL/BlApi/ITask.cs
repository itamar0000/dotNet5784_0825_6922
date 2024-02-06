

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public int Delete(int id);
    public void Update(BO.Task item);
    public void Update(int id, DateTime? date);
    public BO.Task? Read(int id);
    public BO.Task? Read(Func<BO.Task?, bool>? filter = null);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter = null);
    public void SetScheduele(BO.Task item);
    
}
