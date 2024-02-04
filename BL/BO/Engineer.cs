
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

    public Engineer(int Id, string Name, string Email, double Cost, DO.EngineerExperience Level)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.Cost = Cost;
    }
}
