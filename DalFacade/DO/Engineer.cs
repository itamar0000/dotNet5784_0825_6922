namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Cost">daily cost of the engineer, including salary, workplace, tools'</param>
/// <param name="Level">the level of the engineer</param>
public record Engineer
(
    int Id,
    string Name,
    string Email,
    double Cost,
    DO.EngineerExperience Level = DO.EngineerExperience.Beginner,
    string? ImagePath = null
)
{
     public Engineer() : this(0, "", "", 0) { }
}