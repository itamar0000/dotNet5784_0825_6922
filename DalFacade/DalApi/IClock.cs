namespace DalApi;

public interface IClock
{
    public void SetStartDate(DateTime? time);
    public void SetEndDate(DateTime? time);
    public DateTime? GetStartDate();
    public DateTime? GetEndDate();



}