﻿namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <param name="DependentTask">current task</param>
/// <param name="DependensOnTask">current task depends on this task</param>
public record Dependency
(
    int Id,
    int DependentTask ,
    int DependensOnTask
)
{
  public Dependency() : this(0,0,0) { }
}

