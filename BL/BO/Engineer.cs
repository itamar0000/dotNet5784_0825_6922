
namespace BO;

internal class Engineer
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public EngineerExperience Level { get; init; }
    public double Cost { get; set; }
    public TaskInEngineer? Task { get; set; }
}
