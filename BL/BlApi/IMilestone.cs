

namespace BlApi;

public interface IMilestone
{
    public int Create(int id);
    public int Delete(int id);
    public int Update(BO.Milestone item);
    public BO.Milestone Read(int id);
    public IEnumerable<BO.Milestone> ReadAll();
}
