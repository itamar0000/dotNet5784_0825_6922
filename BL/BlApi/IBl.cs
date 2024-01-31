
namespace BlApi;

public interface IBl
{
    public ITask Task { get; }
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public IEngineerInTask EngineerInTask { get; }
    public IMilestoneInTask MilestoneInTask { get; }
    public ITaskInEngineer TaskInEngineer { get; }
    public ITaskInList ITaskInList { get; }
    public IMilestoneInList IMilestoneInList { get; }
}
