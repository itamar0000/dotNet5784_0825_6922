namespace DO;

public record Engineer
(
    int Id,
    string Name,
    string Email,
    double Cost,
    DO.EngineerExperience Level = DO.EngineerExperience.Beginner
)
{
     public Engineer() : this(0, "", "", 0) { }
}