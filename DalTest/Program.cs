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
        
           Initialization.Do(s_dalEngineer,s_dalDependencys, s_dalTask);
            int a = 0;
            do
            {
            try
            {
                Console.WriteLine("choose:\n" + "0.Exit\n" + "1.Open Engineer menu\n" + "2.Open Task menu\n" + "3.Open Dependency menu\n");
                a = int.Parse(Console.ReadLine()!);
                switch (a)
                {
                    case 0:
                        {//exit
                            break;
                        }
                    case 1:
                        {//engineer menu
                            MenuEngineer();
                            break;
                        }
                    case 2:
                        {//Task menu
                            MenuTask();
                            break;
                        }
                    case 3:
                        {//dependency menu
                            MenuDependency();
                            break;
                        }
                    default:
                        {//invalid input
                            Console.WriteLine("Invalid input");
                            break;
                        }
                }
            }
            catch (Exception e)
            {//catch the exceptions that was thrown threw the try and print the message
                Console.WriteLine(e.Message);
            }
            }
            while (a != 0);
      
    }
   /// <summary>
   /// offers the user to choose what he wants to do with the task
   /// 
   /// </summary>
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
                {//exit
                    break;
                }
            case 2:
                {//create
                    Task item = InputTask();
                    s_dalTask!.Create(item);
                    break;
                }
            case 3:
                {// read checks if task with this id exists and print the task if it exists and print error message if it doesnt
                    Console.WriteLine("Enter Task's id:\n");
                    int id = int.Parse(Console.ReadLine()!);
                    Task? item = s_dalTask!.Read(id);
                    if (item != null)
                        Console.WriteLine(item);
                    else Console.WriteLine("Task with this ID does not exist");
                    break;
                }
            case 4:
                {// read all and print all the tasks
                    List<Task> newTasks = new();
                    newTasks = s_dalTask!.ReadAll();
                    foreach(Task item in newTasks)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                }
            case 5:
                {// update checks if task with this id exists and if it does it updates it with the new values
                    int id = int.Parse(Console.ReadLine()!);
                    Task item = InputTask();
                    s_dalTask!.Update(item);
                    break;
                }
            case 6:
                {// delete checks if task with this id exists and if it does it deletes it
                    Console.WriteLine("Enter ID of task to delete: ");
                    int id = int.Parse(Console.ReadLine()!);
                    s_dalTask!.Delete(id);
                    break;
                }
            default:
                {// invalid input
                    Console.WriteLine("Invalid input");
                    break;
                }
        }
    }
    /// <summary>
    /// the function read all the values for the task 
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// offers the user to choose what he wants to do with the engineer
    /// </summary>
    private static void MenuEngineer()
    {
        Console.WriteLine("choose:\n" + "1.Create Engineer\n" + "2.Read Engineer\n" + "3.ReadAll Engineer\n" + "4.Update Engineer\n" + "5.Delete Engineer\n" + "6.Exit");
        int b = int.Parse(Console.ReadLine()!);
        switch (b)
        {
            case 1:
                {// create
                    InputEngineer();
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
                    updateEngineer();
                    break;
                }
            case 5:
                {
                    Console.WriteLine("Enter Engineer's ID to delete:");
                    int del=int.Parse(Console.ReadLine()!);
                    s_dalEngineer!.Delete(del);
                    break;
                }

            case 0:
                break;
            default:
                {
                    Console.WriteLine("Invalid input");
                    break;
                }
        }
    }

    private static void updateEngineer()
    {
        Console.WriteLine("Enter your ID:");
        int id = int.Parse(Console.ReadLine()!);
        Engineer? a = s_dalEngineer.Read(id);
        if(a!=null)
        {
            Console.WriteLine(a);
        }
        else
        {
            Console.WriteLine("Engineer with this ID does not exist");
        }
        Console.WriteLine("Enter your name:");
        string username = Console.ReadLine()!;
        if (username == "")
        {
            username = s_dalEngineer!.Read(id).Name;
        }
        Console.WriteLine("Enter your email:");
        string useremail = Console.ReadLine()!;
        if (useremail == "")
        {
            useremail = s_dalEngineer!.Read(id).Email;
        }
        Console.WriteLine("Enter your experience:");
         int exp= (int.Parse(Console.ReadLine()));
          Console.WriteLine("Enter your salary:");
        int cost = int.Parse(Console.ReadLine()!);
        Engineer engineer = new(id, username, useremail, cost, (DO.EngineerExperience)exp);
        s_dalEngineer!.Update(engineer);
    }
    /// <summary>
    /// get the values foe the engineer
    /// </summary>
    private static void InputEngineer()
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
    }
    /// <summary>
    /// offer the user action to do on the Dependency
    /// </summary>
    private static void MenuDependency()
    {
        Console.WriteLine("0.Exit\n" + "choose:\n" + "1.Create Dependency\n" + "2.Read Dependency\n" + "3.ReadAll Dependency\n" + "4.Update Dpenedency\n" + "5.Delete Dependency");
        int b = int.Parse(Console.ReadLine()!);
        switch (b)
        {
            case 1:
                {//create
                    Dependency dependency = getInputDependency();
                    s_dalDependencys.Create(dependency);
                    break;
                }
            case 2:
                {// checks if dependency with this id exists if it is output it else write a message
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
                {// print all the dependencies
                    List<DO.Dependency> newDependencies = new();
                    newDependencies = s_dalDependencys!.ReadAll();
                    foreach (Dependency dependency in newDependencies)
                    {
                        Console.WriteLine(dependency);
                    }
                    break;
                }
            case 4:
                {
                    // checks if dependency with this id exists if it is update its values
                    Console.WriteLine("Enter ID of task to update");
                    int id = int.Parse(Console.ReadLine()!);
                    Dependency x = s_dalDependencys!.Read(id);
                    Console.WriteLine("Enter task that depends ID");
                    bool flag= int.TryParse(Console.ReadLine()!,out int id1);
                    if (!flag)
                    {
                       id1= x.Id;
                    }
                    Console.WriteLine("Enter the task it depends on ID");
                    flag= int.TryParse(Console.ReadLine()!,out int id2);
                    if (!flag)
                    {
                        id2 = x.Id;
                    }
                    Dependency dependency = new(x.Id, id1, id2);
                    s_dalDependencys.Update(dependency);
                    break;
                }
            case 5:
                {// delete the dependency with this id if exsits
                    Console.WriteLine("Enter Dependency's ID to delete:");
                    int del = int.Parse(Console.ReadLine()!);
                    s_dalDependencys!.Delete(del);
                    break;
                }
            case 0:
                {//exit
                    break;
                }
            default:
                {//invalid input
                    Console.WriteLine("Invalid input");
                    break;
                }

        }

    }
    /// <summary>
    /// get the values for the dependency
    /// </summary>
    /// <returns></returns>
    private static Dependency getInputDependency()
    {
        Console.WriteLine("Enter task that depends ID");
        int id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter the tas it depends on ID");
        int id2 = int.Parse(Console.ReadLine()!);
        Dependency dependency = new(0, id, id2);
        return dependency;
    }


   
}