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
            int a = 0;
            do
            {
                Console.WriteLine("choose:\n" + "0.Exit\n" + "1.Open Engineer menu\n" + "2.Open Task menu\n" + "3.Open Dependency menu\n");
                a = int.Parse(Console.ReadLine()!);
                switch (a)
                {
                    case 1:
                        {
                            MenuEngineer();
                            break;
                        }
                    case 2:
                        {
                            MenuTask();
                            break;
                        }
                    case 3:
                        {
                            MenuDependency();
                            break;
                        }
                }
            }
            while (a != 0);
        }
        catch (Exception)
        {

        }
    }

    private static void MenuTask()
    {
        Console.WriteLine("choose:\n" +
            "1.Exit\n" +
            "2.Create Task\n" +
            "3.Read Task\n" +
            "4.ReadAll Task\n" +
            "5.Update Task\n" +
            "6.Delete Task");
        int b = int.Parse(Console.ReadLine()!);
        switch (b)
        {
            case 1:
                {
                    break;
                }
            case 2:
                {
                    Task item = InputTask();
                    s_dalTask!.Create(item);
                    break;
                }
            case 3:
                {
                    Console.WriteLine("Enter Task's id:\n");
                    int id = int.Parse(Console.ReadLine()!);
                    Task? item = s_dalTask!.Read(id);
                    if (item != null)
                        Console.WriteLine(item);
                    else Console.WriteLine("Task with this ID does not exist");
                    break;
                }
            case 4:
                {
                    List<Task> newTasks = new();
                    newTasks = s_dalTask!.ReadAll();
                    foreach(Task item in newTasks)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                }
            case 5:
                {
                    int id = int.Parse(Console.ReadLine()!);
                    Task item = InputTask();
                    s_dalTask!.Update(item);
                    break;
                }
            case 6:
                {
                    Console.WriteLine("Enter ID of task to delete: ");
                    int id = int.Parse(Console.ReadLine()!);
                    s_dalTask!.Delete(id);
                    break;
                }
        }
    }

    private static Task InputTask()
    {
        Console.WriteLine("Enter Task's alias:\n");
        string? alias = Console.ReadLine();
        Console.WriteLine("Enter Task's description:\n");
        string? description = Console.ReadLine();
        Console.WriteLine("Enter Task's complexity:\n");
        DO.EngineerExperience? Complexity = (DO.EngineerExperience)(int.Parse(Console.ReadLine()));
        Console.WriteLine("Enter Task's scheduled date:\n");
        DateTime scheduledDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Task's start date:\n");
        DateTime start = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Task's required effort time:\n");
        TimeSpan required = TimeSpan.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Task's deadline date:\n");
        DateTime deadline = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Task's deliverables:\n");
        string? deliverables = Console.ReadLine();
        Console.WriteLine("Enter Task's remarks:\n");
        string? remarks = Console.ReadLine();
        Console.WriteLine("Enter Task's engineer Id:\n");
        int? engineerId = int.Parse(Console.ReadLine());
        Task item = new(0, alias, description, DateTime.Now, false, Complexity,
                        scheduledDate, start, required, deadline, null, deliverables,
                        remarks, engineerId);
        return item;
    }

    private static void MenuEngineer()
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
                    foreach(Engineer item in newEngineers)
                    {
                        Console.WriteLine(item);
                    }
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
    private static void MenuDependency()
    {
        Console.WriteLine("0.Exit\n" + "choose:\n" + "1.Create Dependency\n" + "2.Read Dependency\n" + "3.ReadAll Dependency\n" + "4.Update Dpenedency\n" + "5.Delete Dependency");
        int b = int.Parse(Console.ReadLine()!);
        switch (b)
        {
            case 1:
                {
                    Dependency dependency = getInputDependency();
                    s_dalDependencys.Create(dependency);
                    break;
                }
            case 2:
                {
                    Console.WriteLine("Enter Dependency ID");
                    Dependency? check = s_dalDependencys!.Read(int.Parse(Console.ReadLine()!));
                    if (check != null)
                    {
                        Console.WriteLine(check);
                    }
                    else
                    {
                        Console.WriteLine("Dependency with this ID does not exist");
                    }
                    break;
                }
            case 3:
                {
                    List<DO.Dependency> newDependencies = new();
                    newDependencies = s_dalDependencys!.ReadAll();
                    foreach(Dependency dependency in newDependencies)
                    {
                        Console.WriteLine(dependency);
                    }
                    break;
                }
            case 4:
                {

                    Console.WriteLine("Enter Dependency ID");
                    int id = int.Parse(Console.ReadLine()!);
                    Dependency a = s_dalDependencys.Read(id);
                    Console.WriteLine(a);
                    Dependency x = getInputDependency();
                    s_dalDependencys.Update(x);
                    break;
                }
            case 0:
                break;
        }

    }

    private static Dependency getInputDependency()
    {
        Console.WriteLine("Enter task that depends ID");
        int? id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the tas it depends on ID");
        int? id2 = int.Parse(Console.ReadLine());
        Dependency dependency = new(0, id, id2);
        return dependency;
    }
}

