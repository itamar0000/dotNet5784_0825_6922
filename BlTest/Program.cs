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

        if(s_bl.IClock.GetStartDate()>DateTime.Now)
        {

        }

        else if(s_bl.IClock.GetStartDate()<=DateTime.Now)
        {

        }
    }

    public void AssignTaskIntoEngineer()
    {
        Console.WriteLine("Please enter Id task and Id Engineer to assign");
        int IdT = int.Parse(Console.ReadLine()!);
        int IdE = int.Parse(Console.ReadLine()!);

        s_bl.Engineer.Assign(IdE, IdT);    
    }












    //------------------------------------------------------------------------------------------




    /// <summary>
    /// the function takes data from the user and create new task with that data and add it to the list
    /// </summary>
    private static void CreateTask()
    {
        Console.WriteLine("Enter Task's alias:\n");
        string alias = Console.ReadLine()!;

        Console.WriteLine("Enter Task's description:\n");
        string description = Console.ReadLine()!;

        Console.WriteLine("Enter Task's complexity:\n");
        DO.EngineerExperience? Complexity = (DO.EngineerExperience)(int.Parse(Console.ReadLine()!));

        Console.WriteLine("Enter Task's required effort time:\n");
        if (TimeSpan.TryParse(Console.ReadLine(), out TimeSpan required)) { }

        string? deliverables = Console.ReadLine();

        Console.WriteLine("Enter Task's remarks:\n");
        string? remarks = Console.ReadLine();


        BO.Task item = new()
        {
            Id = 0,
            Description = description,
            Alias = alias,
            CreatedAtDate = DateTime.Now,
            Complexity = (BO.EngineerExperience)Complexity, 
            RequiredEffortTime = required,
            Deliverables = deliverables,
            Remarks = remarks
        };

        Console.WriteLine(s_bl!.Task.Create(item));
    }

    /// <summary>
    /// gets the values for the task and check if it exists and update it
    /// </summary>
    private static void UpdateTask()
    {
        Console.WriteLine("Enter ID of task to update: ");
        int id = int.Parse(Console.ReadLine()!);

        BO.Task? found = s_bl.Task.Read(id);
        Console.WriteLine(found);

        if (found == null)//checks if the parse succseeded if it didnt reassign the old value
        {
            Console.WriteLine("Task with this ID does not exist");
            return;
        }

        Console.WriteLine("Enter Task's alias:\n");
        string alias = Console.ReadLine()!;
        if (alias == "")////checks if the input is valid else assign previous value
            alias = found.Alias;

        Console.WriteLine("Enter Task's description:\n");
        string description = Console.ReadLine()!;
        if (description == "")////checks if the input is valid else assign previous value
            description = found.Description;

        Console.WriteLine("Enter Task's complexity:\n");
        DO.EngineerExperience? Complexity = (DO.EngineerExperience)(int.Parse(Console.ReadLine()));

        Console.WriteLine("Enter Task's scheduled date:\n");
        DateTime? scheduledDate = found.ScheduledDate;
        bool flag = DateTime.TryParse(Console.ReadLine(), out DateTime temp);
        if (flag)//checks if the parse succseeded if it didnt reassign the old value
            scheduledDate = temp;

        Console.WriteLine("Enter Task's start date:\n");
        DateTime? start = found.StartDate;
        flag = DateTime.TryParse(Console.ReadLine(), out temp);
        if (flag)//checks if the parse succseeded if it didnt reassign the old value
            start = temp;

        Console.WriteLine("Enter Task's required effort time:\n");
        TimeSpan? required = found.RequiredEffortTime;
        flag = TimeSpan.TryParse(Console.ReadLine(), out TimeSpan tempTS);
        if (flag)//checks if the parse succseeded if it didnt reassign the old value
            required = tempTS;

        Console.WriteLine("Enter Task's deadline date:\n");
        DateTime? deadline = found.DeadlineDate;
        flag = DateTime.TryParse(Console.ReadLine(), out temp);
        if (flag)//checks if the parse succseeded if it didnt reassign the old value
            deadline = temp;

        Console.WriteLine("Enter Task's deliverables:\n");
        string? deliverables = Console.ReadLine();

        if (deliverables == "")//checks if the input is valid else assign previous value
            deliverables = found.Deliverables;

        Console.WriteLine("Enter Task's remarks:\n");
        string? remarks = Console.ReadLine();

        if (remarks == "")//checks if the input is valid else assign previous value
            remarks = found.Remarks;

        Console.WriteLine("Enter Task's engineer Id:\n");
        int? engineerId = found.EngineerId;
        flag = int.TryParse(Console.ReadLine(), out int tempInt);
        if (flag)//checks if the parse succseeded if it didnt reassign the old value
            engineerId = tempInt;

        Task item = new(Id: id,
            alias,
            description,
            DateTime.Now,
            IsMileStone: false,
            isActive: true,
             scheduledDate,
             start,
             RequiredEffortTime: required,
             deadline,
             CompleteDate: null,
             deliverables,
             remarks,
             engineerId);

        s_bl!.Task.Update(item);
    }

    /// <summary>
    /// offers the user to choose what he wants to do with the engineer
    /// </summary>
    private static void MenuEngineer()
    {
        Console.WriteLine("choose:\n" +
            "1.Create Engineer\n" +
            "2.Read Engineer\n" +
            "3.ReadAll Engineer\n" +
            "4.Update Engineer\n" +
            "5.Delete Engineer\n" +
            "6.Exit");

        int b = int.Parse(Console.ReadLine()!);
        switch (b)
        {
            case 1:
                {// create
                    InputEngineer();
                    break;
                }
            case 2:
                {// checks if engineer with this id exists if it is print it else print error message
                    Console.WriteLine("Enter your ID:");
                    Engineer? check = s_bl!.Engineer.Read(int.Parse(Console.ReadLine()!));

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
                {// print all the engineers
                    var newEngineers = s_bl!.Engineer.ReadAll();
                    foreach (Engineer? item in newEngineers)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                }
            case 4:
                {// checks if engineer with this id exists if it is update its values
                    UpdateEngineer();
                    break;
                }
            case 5:
                {// delete the engineer with this id if exsits
                    Console.WriteLine("Enter Engineer's ID to delete:");
                    int del = int.Parse(Console.ReadLine()!);
                    s_bl!.Engineer.Delete(del);
                    break;
                }

            case 6:
                break;
            default:
                {
                    Console.WriteLine("Invalid input");
                    break;
                }
        }
    }

    /// <summary>
    /// gets the values for the engineer and check if he exists and update it
    /// </summary>
    private static void UpdateEngineer()
    {
        Console.WriteLine("Enter your ID:");
        int id = int.Parse(Console.ReadLine()!);

        Engineer? a = s_bl.Engineer.Read(id);

        if (a != null)
        {//checks if the object exists if it is prints it
            Console.WriteLine(a);
        }
        else
        {// if it deosnt output a message
            Console.WriteLine("Engineer with this ID does not exist");
            return;
        }

        Console.WriteLine("Enter your name:");
        string username = Console.ReadLine()!;
        if (username == "")
        {// checks if the input is valid else assign previous value
            username = s_bl!.Engineer.Read(id).Name;
        }

        Console.WriteLine("Enter your email:");
        string useremail = Console.ReadLine()!;
        if (useremail == "")
        {// checks if the input is valid else assign previous value
            useremail = s_bl!.Engineer.Read(id).Email;
        }

        Console.WriteLine("Enter your experience:");
        int exp = (int.Parse(Console.ReadLine()!));

        Console.WriteLine("Enter your salary:");
        bool flag = double.TryParse(Console.ReadLine()!, out double cost);
        if (!flag)
        {// checks if the parse succseeded if it didnt reassign the old value
            cost = a.Cost;
        }

        Engineer engineer = new(id, username, useremail, cost, (DO.EngineerExperience)exp);
        s_bl!.Engineer.Update(engineer);
    }

    /// <summary>
    /// get the values for the engineer
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
        Console.WriteLine(s_bl!.Engineer.Create(engineer));
    }

    /// <summary>
    /// offer the user action to do on the Dependency
    /// </summary>
    private static void MenuDependency()
    {
        Console.WriteLine("choose:\n" +
            "0.Exit\n" +
            "1.Create Dependency\n" +
            "2.Read Dependency\n" +
            "3.ReadAll Dependency\n" +
            "4.Update Dpenedency\n" +
            "5.Delete Dependency");

        int b = int.Parse(Console.ReadLine()!);
        switch (b)
        {
            case 1:
                {//create
                    Dependency dependency = GetInputDependency();
                    Console.WriteLine(s_bl.Dependency.Create(dependency));
                    break;
                }
            case 2:
                {// checks if dependency with this id exists if it is output it else write a message
                    Console.WriteLine("Enter Dependency ID");
                    Dependency? check = s_bl!.Dependency.Read(int.Parse(Console.ReadLine()!));

                    if (check != null)
                    {//checks if the object exists if it is prnts it 
                        Console.WriteLine(check);
                    }
                    else
                    {//if it deosnt output a message
                        Console.WriteLine("Dependency with this ID does not exist");
                    }

                    break;
                }
            case 3:
                {// print all the dependencies
                    var newDependencies = s_bl!.Dependency.ReadAll();
                    foreach (Dependency? dependency in newDependencies)
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

                    Dependency x = s_bl!.Dependency.Read(id);

                    if (x == null)
                    {
                        Console.WriteLine($"\nDependency with this ID = {id} doesn't exists\n");
                        return;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine("Enter task that depends ID");
                    int? id3 = x.DependentTask, id4 = x.DependensOnTask;

                    bool flag = int.TryParse(Console.ReadLine()!, out int id1);
                    if (flag)
                    {// checks if the input is valid
                        id3 = id1;
                    }

                    Console.WriteLine("Enter the task it depends on ID");
                    flag = int.TryParse(Console.ReadLine()!, out int id2);
                    if (flag)
                    {// checks if the input is valid
                        id4 = id2;
                    }

                    Dependency dependency = new(x.Id, id3, id4);
                    s_bl.Dependency.Update(dependency);

                    break;
                }
            case 5:
                {// delete the dependency with this id if exsits
                    Console.WriteLine("Enter Dependency's ID to delete:");
                    int del = int.Parse(Console.ReadLine()!);
                    s_bl!.Dependency.Delete(del);
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
    private static Dependency GetInputDependency()
    {
        Console.WriteLine("Enter task that depends ID");
        int id = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter the task it depends on ID");
        int id2 = int.Parse(Console.ReadLine()!);

        Dependency dependency = new(0, id, id2);

        return dependency;
    }

   
}