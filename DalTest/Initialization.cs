namespace DalTest;
using DalApi;
using DO;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Net.Security;
using System.Threading.Tasks;

public static class Initialization
{
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependency? s_dalDependency; //stage 1
    private static ITask? s_dalTask; //stage 1

    private static readonly Random s_rand = new();

    private static void createEngineer()
    {
        string[] EngineerNames =
        {
            "Alex Turner", "Elena Rodriguez", "Oscar Chang", "Mia Patel", "Dylan Sullivan", "Grace Wang", "Jordan Foster", "Isabella Kim", "Nathan Carter", "Sophie Nguyen", "Ethan Murphy", "Ava Martinez", "Liam Williams", "Olivia Lee", "Logan Davis", "Emily Hernandez", "Caleb Brown", "Zoe Smith", "Mason Johnson", "Ella Jones"
        };
        string[] Engineeremails =
        {
            "alex.turner@example.com",
    "elena.rodriguez@example.com",
    "oscar.chang@example.com",
    "mia.patel@example.com",
    "dylan.sullivan@example.com",
    "grace.wang@example.com",
    "jordan.foster@example.com",
    "isabella.kim@example.com",
    "nathan.carter@example.com",
    "sophie.nguyen@example.com",
    "ethan.murphy@example.com",
    "ava.martinez@example.com",
    "liam.williams@example.com",
    "olivia.lee@example.com",
    "logan.davis@example.com",
    "emily.hernandez@example.com",
    "caleb.brown@example.com",
    "zoe.smith@example.com",
    "mason.johnson@example.com",
    "ella.jones@example.com"
        };

        for (int i = 0; i < EngineerNames.Length; i++)
        {
            int id;
            do
                id = s_rand.Next(100000000, 999999999);
            while (s_dalEngineer!.Read(id) != null);

            int level = s_rand.Next(5);
            Engineer newEng = new(id, EngineerNames[i], Engineeremails[i], level * 10000+1000, (DO.EngineerExperience)level);

            s_dalEngineer!.Create(newEng);
        }
    }
    private static void createDependency()
    {
        int?[] dependentask = { 1, 2, 3, 4, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18, 19, 19,19 };
        int?[] depenedsontask = { 0, 1, 2, 3, 3, 4, 5, 6, 5, 7, 5, 8, 5, 9, 7, 10, 7, 11, 5, 12, 7, 13, 5, 14, 9, 15, 13, 16, 14, 5, 7, 4, 2, 7 };
        for (int i = 0; i < dependentask.Length; i++)
        {
            Dependency newDep = new(0, dependentask[i], depenedsontask[i]);
            s_dalDependency!.Create(newDep);
        }
    }
    private static void createTask()
    {
        string[] tasks = {"Requirement Analysis",
    "Feasibility Study",
    "System Architecture Design",
    "Component Specification",
    "Hardware Procurement",
    "Software Development",
    "Prototyping",
    "Integration Testing",
    "Quality Assurance Testing",
    "User Training",
    "System Deployment",
    "Performance Testing",
    "Bug Fixing",
    "User Acceptance Testing",
    "Final Documentation",
    "Project Sign-off",
    "Post-Implementation Review",
    "Project Closure",
    "Security Audit",
    "Capacity Planning"
        };
        string[] descriptions = { "Understand what needs to be done in the project.",
    "Check if it's possible and makes sense to do the project.",
    "Plan how the system will be built.",
    "Describe in detail what each part of the system should be like.",
    "Buy the necessary hardware for the system.",
    "Write the actual code for the software components.",
    "Make a simple version of the system to test its basic features.",
    "Put all the pieces together and make sure they work well.",
    "Test the system thoroughly to find and fix any problems.",
    "Create all the necessary documents about the system.",
    "Teach the users how to use the system.",
    "Put the system into use for everyone.",
    "Test how well the system performs in different situations.",
    "Fix any issues or problems in the system.",
    "Let users try out the system to make sure they like it.",
    "Finish all the documentation for the system.",
    "Get formal approval to finish the project.",
    "Look back at the project and see what went well and what can be improved.",
    "Officially finish the project, making sure everything is done.",
    "Check the security of the software.",
    "Plan for the system's capacity and resources."};

        int[] complexity = {0,  // Requirement Analysis
    1,  // Feasibility Study
    2,  // System Architecture Design
    2,  // Component Specification
    0,  // Hardware Procurement
    2,  // Software Development
    1,  // Prototyping
    2,  // Integration Testing
    2,  // Quality Assurance Testing
    2,  // User Training
    2,  // System Deployment
    3,  // Performance Testing
    2,  // Bug Fixing
    2,  // User Acceptance Testing
    2,  // Final Documentation
    2,  // Project Sign-off
    3,  // Post-Implementation Review
    2,  // Project Closure
    3,  // Security Audit
    2  // Capacity Planning
        };
        for (int i = 0; i < tasks.Length; i++)
        {
            int level = s_rand.Next(5);
            DO.Task newTask = new(0, tasks[i], descriptions[i], DateTime.Now, false, (DO.EngineerExperience)complexity[i], DateTime.Now.AddDays(s_rand.Next(60)), DateTime.Now.AddDays(s_rand.Next(60)), TimeSpan.FromDays(s_rand.Next(30, 60)), DateTime.Now.AddDays(120));
            s_dalTask!.Create(newTask);
        }

    }
    public static void Do(IEngineer? dalEngineer, IDependency? dalDependency, ITask? dalTask)
    {
        s_dalEngineer = dalEngineer;
        s_dalDependency = dalDependency;
        s_dalTask = dalTask;
        createDependency();
        createEngineer();
        createTask();
    }
}






