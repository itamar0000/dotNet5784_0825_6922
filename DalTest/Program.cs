namespace DalTest;
using DalList;
using Dal;
using DalApi;
using DO;
using System.Transactions;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependency? s_dalDependencys = new DependencyImplementation();

    static void Main(string[] args)
    {
        try
        {
           Initialization.Do(s_dalEngineer,s_dalDependencys, s_dalTask);
            int a =int.Parse(Console.ReadLine()!);
            Console.WriteLine("choose:\n"+"0.Exit\n"+"1.Open Engineer menu\n"+"2.Open Task menu\n"+"3.Open Dependency menu\n");
            switch (a)
            {
                case 1:
                    {
                        
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("choose:\n" +
                            "1.Exit\n"
                            "2.Create Task\n" + 
                            "3.Read Task\n" + 
                            "4.ReadAll Task\n" +
                            "5.Update Task\n" +
                            "6.Delete Task\n" +
                            "7.");
                        int b = int.Parse(Console.ReadLine()!);
                        switch (b)
                        {
                            case 1:
                                {
                                    Console.WriteLine("Enter Task's alias:");
                                    string? alias = Console.ReadLine();
                                    Console.WriteLine("Enter Task's description:");
                                    string? description = Console.ReadLine();
                                    Console.WriteLine("Enter Task's start date:");
                                    DateTime start = DateTime.Parse(Console.ReadLine()!);
                                    Console.WriteLine("Enter Task's end date:");
                                    DateTime end = DateTime.Parse(Console.ReadLine()!);
                                    break;
                                }
                        }


                        break;
                    }
            }
 

        }
        catch (Exception)
        {

        }
    }
    private void MenuEngineer()
    {
        Console.WriteLine("choose:\n" + "1.Create Engineer\n" + "2.Read Engineer\n" + "3.ReadAll Engineer\n" + "4.Update Engineer\n" + "5.Delete Engineer\n" + "6.Exit");
        int b = int.Parse(Console.ReadLine()!);
        switch (b)
        {
            case 1:
                {
                    Console.WriteLine("Enter your ID:");
                    int id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter your name:");
                    string username = Console.ReadLine()!;
                    Console.WriteLine("Enter your email:");
                    string useremail = Console.ReadLine()!;
                    Console.WriteLine("Enter your experience:");
                    int exp = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter your salary:");
                    int cost = int.Parse(Console.ReadLine()!);
                    Engineer engineer = new(id, username, useremail, cost, (DO.EngineerExperience)exp);
                    s_dalEngineer!.Create(engineer);
                    break;
                }
            case 2:
                {
                    Console.WriteLine("Enter your ID:");
                    Engineer? check = s_dalEngineer!.Read(int.Parse(Console.ReadLine()!));
                    if (check != null)
                    {
                        Console.WriteLine(check);
                    }
                    else
                    {
                        Console.WriteLine("Engineer with this ID does not exist");
                    }
                    break;
                }
            case 3:
                {
                    List<DO.Engineer> newEngineers = new();
                    newEngineers = s_dalEngineer!.ReadAll();
                    break;
                }
            case 4:
                {
                    Console.WriteLine("Enter your ID:");
                    int id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter your name:");
                    string username = Console.ReadLine()!;
                    Console.WriteLine("Enter your email:");
                    string useremail = Console.ReadLine()!;
                    Console.WriteLine("Enter your experience:");
                    int exp = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter your salary:");
                    int cost = int.Parse(Console.ReadLine()!);
                    Engineer engineer = new(id, username, useremail, cost, (DO.EngineerExperience)exp);
                    s_dalEngineer!.Update(engineer);
                    break;
                }
            case 0:
                break;
        }
    }
}

