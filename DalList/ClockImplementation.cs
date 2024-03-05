using DalApi;

namespace Dal;

internal class ClockImplementation : IClock
{
    public DateTime? GetCurrentDate()
    {
        return DataSource.Config.currentDate;
    }

    public void SetCurrentDate(DateTime time)
    {
        DataSource.Config.startDate = time;
    }

    public void SetEndDate(DateTime? time)
    {
       DataSource.Config.startDate = time;
    }

    public void SetStartDate(DateTime? time)
    {
        DataSource.Config.endDate = time;
    }

    DateTime? IClock.GetEndDate()
    {
        return DataSource.Config.endDate;
    }

    DateTime? IClock.GetStartDate()
    {
        return DataSource.Config.startDate;
    }
}
