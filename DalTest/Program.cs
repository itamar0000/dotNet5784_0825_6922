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
            Console.WriteLine("choose:\n"+"1.Open Engineer menu\n"+"2.Open Task menu\n"+"3.Open Dependency menu\n"+"4.Exit");
            switch (a)
            {
                case 1:
                        Console.WriteLine("choose:\n" + "1.Create Engineer\n" + "2.Read Engineer\n" + "3.ReadAll Engineer\n" + "4.Update Engineer\n" + "5.Delete Engineer\n");
                        int b = int.Parse(Console.ReadLine()!);
                        switch (b)
                    {
                            case 1:
                            {
                                Console.WriteLine("Enter your ID:");
                                int id = int.Parse(Console.ReadLine()!);
                                Console.WriteLine("Enter your name:");
                                string? username = Console.ReadLine();
                                Console.WriteLine("Enter your email:");
                                string? useremail = Console.ReadLine();
                                Console.WriteLine("Enter your experience:");
                                int exp = int.Parse(Console.ReadLine()!);
                                Console.WriteLine("Enter your salary:");
                                int cost = int.Parse(Console.ReadLine()!);
                                Engineer engineer = new (id,username, useremail,cost,(DO.EngineerExperience)exp);   
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Enter your ID:");
                                Engineer? check=s_dalEngineer!.Read(int.Parse(Console.ReadLine()!));
                                if(check!=null)
                                {
                                    Console.WriteLine(check);
                                }
                                else
                                {
                                    Console.WriteLine("Engineer with this ID does not exist");
                                }
                                break;
                            }
                    }
                    break;
            }

        }
        catch (Exception)
        {

        }
    }
}

