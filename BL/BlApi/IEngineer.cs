
namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer boEngineer);
    public void Delete(int id);
    public void Update(BO.Engineer boEngineer);
    public BO.Engineer Read(int id);
    public BO.Engineer? Read(Func<BO.Engineer?, bool>? filter);
    public IEnumerable<BO.Engineer>? ReadAll(Func<BO.Engineer?, bool>? filter = null);
    public void Assign(int engineerId, int taskId);
}
