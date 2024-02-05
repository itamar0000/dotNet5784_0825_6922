using DalApi;
using DalTest;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
        {//clears all the data from the file and reinitializes it
            Initialization.Do(); //stage 4  
        }

        if(s_bl.Clock.GetStartDate()>DateTime.Now)
        {

        }
        if(s_bl.Clock.GetStartDate()<=DateTime.Now)
        {

        }
    }
}
