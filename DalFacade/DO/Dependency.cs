namespace DO;
/// <summary>
/// 
/// </summary>
public record Dependency
(
    int id,
    int? DependentTask = null,
    int? DependensOnTask = null
)
{
  public Dependency() : this(0) { }
}

