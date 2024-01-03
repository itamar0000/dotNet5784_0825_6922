namespace DO;

public record Engineer
{
    private int id;
    private string engLevel;
    private string? fullName = null;
    private string? email = null;
    private double? costPerHour = null;
    private Engineer() : this(0, "", "") { }
    private Engineer(int id1, string fullName1, string email1, string engLevel1 = "Beginner", double costPerHour1 = 0)
    {
        this.id = id1;
        this.fullName = fullName1;
        this.email = email1;
        this.engLevel = engLevel1;
        this.costPerHour = costPerHour1;
    }
}
