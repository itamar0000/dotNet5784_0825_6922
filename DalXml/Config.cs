namespace Dal;

internal static class Config
{
    private static string s_data_config_xml = "data-config";

    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }

    internal static void ResetTaskId() => XMLTools.ResetNextId(s_data_config_xml, "NextTaskId","InitTaskId");
    internal static void ResetDependencyId() => XMLTools.ResetNextId(s_data_config_xml, "NextDependencyId","InitDependencyId");
}
