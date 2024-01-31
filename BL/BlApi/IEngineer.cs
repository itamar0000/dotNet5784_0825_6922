
namespace BlApi;

public interface IEngineer
{
    public int Create(int id);
    public int Delete(int id);
    public int Update(BO.Engineer item);
    public BO.Engineer Read(int id);
    public IEnumerable<BO.Engineer> ReadAll();
}
