
using BlApi;
using BO;
using System.Data.Common;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BlImplementation;

internal class ClockImplementation : BlApi.IClock
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public DateTime? GetEndDate()
    {
        return _dal.Clock.GetEndDate();
    }

    public DateTime? GetStartDate()
    {
        return _dal.Clock.GetStartDate();
    }

    public void SetEndDate(DateTime time)
    {
        _dal.Clock.SetEndDate(time);
    }

    public void SetStartDate(DateTime time)
    {
        _dal.Clock.SetStartDate(time);
    }

    public ProjectStatus GetStatus()
    {
        if (DateTime.Now > GetEndDate())
            return ProjectStatus.end;
        else if (DateTime.Now < GetStartDate())
            return ProjectStatus.BeforeStart;
        return ProjectStatus.Start;
    }
}
