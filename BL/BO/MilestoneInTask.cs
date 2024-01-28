

namespace BO;

public class MilestoneInTask
{
    public int Id { get; init; }
    public string Description { get; init; }
    public string Alias { get; init; }
    public DateTime CreatedAtDate { get; init; }
    public Status Status { get; init; }
    public DateTime ForecastDate { get; set; }
    public DateTime DeadlineDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public double CompletionPercentage { get; set; }
    public string Remarks { get; set; }
    public List<BO.TaskInList> Dependencies { get; set; }
}
