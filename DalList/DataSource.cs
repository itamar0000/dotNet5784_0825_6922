namespace Dal;

internal static class DataSource
{
    internal static List<DO.Engineer> Tasks { get; } = new();
    internal static List<DO.Dependency>Dependencys { get; } = new();
    internal static List<DO.Task>Tasks { get; } = new();
    internal static class Config
    {
        internal const int startTaskId = 0; 
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal const int startDependencyId = 0;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }
}
