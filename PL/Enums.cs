namespace PL;
using System.Collections;
internal class EngineerCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_enums =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
internal class TaskCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status>? s_enums =
        (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>);
    public IEnumerator GetEnumerator()=>s_enums!.GetEnumerator();
}


