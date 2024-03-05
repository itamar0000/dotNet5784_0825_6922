namespace DalApi;

public interface IClock
{
    public void SetStartDate(DateTime? time);
    public void SetEndDate(DateTime? time);
    public DateTime? GetStartDate();
    public DateTime? GetEndDate();

    public DateTime? GetCurrentDate();
    public void SetCurrentDate(DateTime time);

}