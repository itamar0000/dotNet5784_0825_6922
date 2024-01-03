namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <param name="DependentTask">current task</param>
/// <param name="DependensOnTask">current task depends on this task</param>
public record Dependency
(
    int id,
    int? DependentTask = null,
    int? DependensOnTask = null
)
{
  public Dependency() : this(0) { }
}

