
namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public double Cost { get; set; }
    public EngineerExperience Level { get; init; }
    public TaskInEngineer? Task { get; set; }
    public override string ToString() => this.ToStringProperty();
}
