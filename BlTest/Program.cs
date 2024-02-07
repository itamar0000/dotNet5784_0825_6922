using BO;
using DalApi;
using DalTest;
using System;
using System.Security.Cryptography;

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
        //int a = 0;

        //do
        //{



        //    try
        //    {
        //        Console.WriteLine("choose:\n" +
        //            "0. Exit\n" +
        //            "1. Open Engineer menu\n" +
        //            "2. Open Task menu\n" +
        //            "3. assign date\n" +
        //            "4. ");
        //        a = int.Parse(Console.ReadLine()!);

        //        switch (a)
        //        {
        //            case 1:
        //                {
        //                    MenuEngineer();
        //                    break;
        //                }
        //            case 2:
        //                {

        //                    MenuTask();
        //                    break;
        //                }
        //            case 3:
        //                {
        //                    MenuClock();
        //                    break;
        //                }
        //            case 4:
        //                {
        //                    MenuAfterStart();
        //                    break;
        //                }
        //            default:
        //                {
        //                    Console.WriteLine("Invalid input");
        //                    break;
        //                }
        //        }
        //    }


        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        //while (a != 0);



        //int num = 1;

        switch (s_bl.Clock.GetStatus())
        {
            case ProjectStatus.BeforeStart:
                {
                    MenuBeforeStart();
                    break;
                }
            case ProjectStatus.Start:
                {
                    MenuAfterStart();
                    break;
                }
            case ProjectStatus.end:
                {
                    break;
                }
        }
    }

    private static void MenuBeforeStart()
    {
        int num = 1;

        while (num != 0)
        {
            Console.WriteLine("choose:\n" +
            "0. Exit\n" +
            "1. Open Engineer menu\n" +
            "2. Open Task menu\n" +
            "3. assign dates\n");

            num = int.Parse(Console.ReadLine()!);

            switch (num)
            {
                case 0:
                    {
                        break;
                    }
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
                        MenuClock();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
            }
        }
    }

    private static void MenuAfterStart()
    {
        foreach (var item in s_bl.Task.ReadAll())
        {
            if (!item.ScheduledDate.HasValue)
            {
                s_bl.Task.SetScheduele(item);
            }
        }
        int c = 0;
        do
        {
            Console.WriteLine("choose:\n" +
                    "0. Exit\n" +
                    "1. Assign Task Into Engineer\n" +
                    "2. Menu Engineer\n" +
                    "3. Menu Task");
            c = int.Parse(Console.ReadLine()!);

            switch (c)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        AssignTaskIntoEngineer();
                        break;
                    }
                case 2:
                    {
                        MenuEngineerAfterStart();
                        break;
                    }
                case 3:
                    {
                        MenuTaskAfterStart();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
            }

        } while (c != 0);
    }

    private static void MenuClock()
    {
        Console.WriteLine("enter the start date of the project");
        DateTime start = DateTime.Parse(Console.ReadLine()!);
        s_bl.Clock.SetStartDate(start);

        foreach (var item in s_bl.Task.ReadAll())
        {
            s_bl.Task.SetScheduele(item);
        }
    }


    public static void AssignTaskIntoEngineer()
    {
        Console.WriteLine("Please enter Id task to assign");
        int IdT = int.Parse(Console.ReadLine()!);
       
        Console.WriteLine("Please enter Id engineer to assign");
        int IdE = int.Parse(Console.ReadLine()!);

        s_bl.Engineer.Assign(IdE, IdT);
    }


    private static void MenuEngineerAfterStart()
    {
        int num = 1;

        do
        {
            Console.WriteLine("choose:\n" +
            "0. Exit\n" +
            "1. Create Engineer\n" +
            "2. Read Engineer\n" +
            "3. ReadAll Engineer\n" +
            "4. Delete Engineer\n");

            num = int.Parse(Console.ReadLine()!);

            switch(num)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        CreateEngineer();
                        break;
                    }
                case 2:
                    {
                        // checks if engineer with this id exists if it is print it else print error message
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
                    {
                        // print all the engineers
                        var newEngineers = s_bl!.Engineer.ReadAll();
                        foreach (Engineer? item in newEngineers)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case 4:
                    {
                        DeleteEngineer();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
            }
        } while (num != 0);

    } 
        

    private static void MenuTaskAfterStart()
    {
        int num = 1;

        do
        {
            Console.WriteLine("choose:\n" +
           "0. Exit\n" +
           "1. Read Task\n" +
           "2. ReadAll Task\n");

            num = int.Parse(Console.ReadLine()!);

            switch (num)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        // read checks if task with this id exists and print the task if it exists and print error message if it doesnt
                        Console.WriteLine("Enter Task's id:\n");
                        bool flag = int.TryParse(Console.ReadLine(), out int id);

                        BO.Task? item = s_bl!.Task.Read(id);

                        if (item != null)
                            Console.WriteLine(item);

                        else
                            Console.WriteLine("Task with this ID does not exist");

                        break;
                    }
                case 2:
                    {
                        // read all and print all the tasks
                        var newTasks = s_bl!.Task.ReadAll();
                        foreach (BO.Task? item in newTasks)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
            }
        } while (num != 0);

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
        BO.EngineerExperience? Complexity = (BO.EngineerExperience)(int.Parse(Console.ReadLine()!));

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
            Complexity = Complexity,
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

        Console.WriteLine("Enter Task's required effort time:\n");
        TimeSpan? required = found.RequiredEffortTime;
        bool flag = TimeSpan.TryParse(Console.ReadLine(), out TimeSpan tempTS);
        if (flag)//checks if the parse succseeded if it didnt reassign the old value
            required = tempTS;

        Console.WriteLine("Enter Task's deliverables:\n");
        string? deliverables = Console.ReadLine();

        if (deliverables == "")//checks if the input is valid else assign previous value
            deliverables = found.Deliverables;

        Console.WriteLine("Enter Task's remarks:\n");
        string? remarks = Console.ReadLine();

        if (remarks == "")//checks if the input is valid else assign previous value
            remarks = found.Remarks;



        Console.WriteLine("Enter dependencies to add. to end press Enter");
        List<BO.TaskInList>? depToAdd = DependenciesTo();

        while (depToAdd.Any())
        {
            TaskInList taskInList = depToAdd.FirstOrDefault();
            if (found.Dependencies.FirstOrDefault(item => item.Id == taskInList.Id) == null)
            {
                found.Dependencies.Add(taskInList);
            }
            depToAdd.Remove(taskInList);
        }

        Console.WriteLine("Enter dependencies to remove. to end press Enter");
        List<BO.TaskInList>? depToRemove = DependenciesTo();

        while (depToRemove.Any())
        {
            TaskInList taskInList = depToRemove.FirstOrDefault();
            if (found.Dependencies.FirstOrDefault(item => item.Id == taskInList.Id) != null)
            {
                found.Dependencies.Remove(taskInList);
            }
            depToRemove.Remove(taskInList);
        }



        BO.Task item = new()
        {
            Id = found.Id,
            Description = description,
            Alias = alias,
            CreatedAtDate = found.CreatedAtDate,
            Complexity = (BO.EngineerExperience)Complexity,
            RequiredEffortTime = required,
            Deliverables = deliverables,
            Remarks = remarks,
            Dependencies = found.Dependencies
        };

        s_bl!.Task.Update(item);
    }


    public static List<BO.TaskInList> DependenciesTo()
    {
        bool flag = int.TryParse(Console.ReadLine(), out int IdDepend);

        List<BO.TaskInList>? depTo = null;

        while (flag)
        {
            BO.Task task = s_bl.Task.Read(IdDepend);

            if (task == null)
                Console.WriteLine($"Task with ID = {IdDepend} does not exist");
            else
            {
                BO.TaskInList taskInList = new()
                {
                    Id = task.Id,
                    Description = task.Description,
                    Alias = task.Alias,
                    Status = task.Status
                };

                depTo?.Add(taskInList);
            }

            flag = int.TryParse(Console.ReadLine(), out IdDepend);
        }

        return depTo;
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
                    CreateEngineer();
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
                    DeleteEngineer();
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

    private static void DeleteEngineer()
    {
        Console.WriteLine("Enter Engineer's ID to delete:");
        int del = int.Parse(Console.ReadLine()!);
        s_bl!.Engineer.Delete(del);
    }

    /// <summary>
    /// gets the values for the engineer and check if he exists and update it
    /// </summary>
    private static void UpdateEngineer()
    {
        Console.WriteLine("Enter your ID: ");
        int id = int.Parse(Console.ReadLine()!);

        BO.Engineer? a = s_bl.Engineer.Read(id);

        if (a != null)
        {//checks if the object exists if it is prints it
            Console.WriteLine(a);
        }
        else
        {// if it deosnt output a message
            Console.WriteLine("Engineer with this ID does not exist");
            return;
        }

        Console.WriteLine("Enter your name: ");
        string username = Console.ReadLine()!;

        Console.WriteLine("Enter your email: ");
        string useremail = Console.ReadLine()!;


        Console.WriteLine("Enter your experience: ");
        int exp = (int.Parse(Console.ReadLine()!));

        Console.WriteLine("Enter your salary: ");
        bool flag = double.TryParse(Console.ReadLine()!, out double cost);
        if (!flag)
        {// checks if the parse succseeded if it didnt reassign the old value
            cost = a.Cost;
        }

        /* TaskInEngineer? task;
         Console.WriteLine("Enter engineer's task: ");
         flag = int.TryParse(Console.ReadLine()!, out int taskId);
         if (!flag)
         {// checks if the parse succseeded if it didnt reassign the old value
             task = a.Task;
         }
         else
         {
             if(s_bl.Task.Read(taskId) == null)
             {
                 Console.WriteLine($"Task with Id = {taskId} does not exist");
                 return;
             }

             TaskInEngineer? help = new()
             {
                 Id = taskId,
                 Alias = s_bl.Task.Read(taskId)!.Alias
             };

             task = help;
         }*/

        BO.Engineer engineer = new()
        {
            Id = a.Id,
            Name = username,
            Email = useremail,
            Cost = cost,
            Level = (BO.EngineerExperience)exp,
            //Task = task
        };

        s_bl!.Engineer.Update(engineer);
    }

    /// <summary>
    /// get the values for the engineer
    /// </summary>
    private static void CreateEngineer()
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

        BO.Engineer engineer = new()
        {
            Id = id,
            Name = username,
            Email = useremail,
            Cost = cost,
            Level = (BO.EngineerExperience)exp
        };
        Console.WriteLine(s_bl!.Engineer.Create(engineer));
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
                {//exit
                    break;
                }
            case 2:
                {//create
                    CreateTask();
                    break;
                }
            case 3:
                {// read checks if task with this id exists and print the task if it exists and print error message if it doesnt
                    Console.WriteLine("Enter Task's id:\n");
                    bool flag = int.TryParse(Console.ReadLine(), out int id);

                    BO.Task? item = s_bl!.Task.Read(id);

                    if (item != null)
                        Console.WriteLine(item);

                    else
                        Console.WriteLine("Task with this ID does not exist");

                    break;
                }
            case 4:
                {// read all and print all the tasks
                    var newTasks = s_bl!.Task.ReadAll();
                    foreach (BO.Task? item in newTasks)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                }
            case 5:
                {// update checks if task with this id exists and if it does it updates it with the new values
                    UpdateTask();
                    break;
                }
            case 6:
                {// delete checks if task with this id exists and if it does it deletes it
                    Console.WriteLine("Enter ID of task to delete: ");
                    bool flag = int.TryParse(Console.ReadLine(), out int id);
                    s_bl!.Task.Delete(id);
                    break;
                }
            default:
                {// invalid input
                    Console.WriteLine("Invalid input");
                    break;
                }
        }
    }
}



