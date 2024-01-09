namespace DalTest;
using DalApi;
using DO;

/// <summary>
/// 
/// </summary>
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
            "Alex Turner", "Elena Rodriguez", "Oscar Chang", "Mia Patel",
            "Dylan Sullivan", "Grace Wang", "Jordan Foster", "Isabella Kim",
            "Nathan Carter", "Sophie Nguyen", "Ethan Murphy", "Ava Martinez",
            "Liam Williams", "Olivia Lee", "Logan Davis", "Emily Hernandez",
            "Caleb Brown", "Zoe Smith", "Mason Johnson", "Ella Jones"
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
                id = s_rand.Next(200000000, 400000000);
            while (s_dalEngineer!.Read(id) != null);

            int level = s_rand.Next(5);
            Engineer newEng = new(id,
                EngineerNames[i],
                Engineeremails[i],
                level * 100 + 30,
                (DO.EngineerExperience)level);

            s_dalEngineer!.Create(newEng);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private static void createDependency()
    {
        
        
        int?[] dependentask = new int?[] { 1, 2, 3, 4, 4, 4, 5, 5, 5, 6, 7, 7, 8,
            9, 10, 10, 11, 11, 11, 12, 13, 14, 14, 18, 19, 19, 19, 20, 20, 20, 23,
            23, 23, 24, 24, 25, 25, 25, 26, 26, 27, 28, 28, 28, 29, 29, 29, 30, 30, 30, 30, 30 };

        int?[] depenedsontask = new int?[] { 0, 1, 1, 1, 2, 3, 1, 2, 3, 5, 4, 5, 6,
            6, 7, 8, 7, 8, 9, 10, 11, 11, 12, 13, 13, 14, 15, 13, 14, 16, 16, 17, 18,
            17, 18, 18, 19, 20, 20, 21, 21, 21, 22, 23, 24, 25, 26, 25, 26, 27, 28, 29 };

        for (int i = 0; i < dependentask.Length; i++)
        {
            Dependency newDep = new(0, dependentask[i], depenedsontask[i]);
            s_dalDependency!.Create(newDep);
        }
    }
    /// <summary>
    /// 
    /// </summary>
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
  "Capacity Planning",
  "Usability Testing",
  "Regulatory Compliance Assessment",
  "Data Migration",
  "Continuous Integration/Continuous Deployment (CI/CD) Implementation",
  "Change Management",
  "Stakeholder Communication",
  "User Feedback Collection",
  "Disaster Recovery Planning",
  "Performance Monitoring and Optimization",
  "Technology Stack Evaluation",
  "Technology Stack Adjustment"
        };
        string[] descriptions = {  "Understand what needs to be done in the project.",
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
  "Plan for the system's capacity and resources.",
  "Evaluate the system's user interface and overall user experience.",
  "Verify that the system complies with relevant regulations, standards, and legal requirements.",
  "Plan and execute the transfer of data from existing systems to the newly developed system.",
  "Establish automated processes for code integration, testing, and deployment.",
  "Develop strategies and processes to manage and communicate changes effectively.",
  "Maintain regular and transparent communication with project stakeholders.",
  "Set up mechanisms to collect feedback from end-users during and after system deployment.",
  "Develop a comprehensive plan for recovering the system in the event of unexpected disruptions.",
  "Implement tools and processes to monitor the system's performance in real-time.",
  "Assess and choose the appropriate technologies, frameworks, and tools for system development.",
  "Adjust the technology stack based on evaluation and project requirements."};

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
    2,  // Capacity Planning
    3,  //Usability Testing
    4,  //Regulatory Compliance Assessment
    4,  //Data Migration
    0,  //Continuous Integration/Continuous Deployment (CI/CD) Implementation
    2,  //Change Management
    1,  //Stakeholder Communication
    3,  //User Feedback Collection
    4,  //Disaster Recovery Planning
    3,  //Performance Monitoring and Optimization
    1,  //Technology Stack Evaluation
    4,  //Technology Stack Adjustment
        };
        for (int i = 0; i < tasks.Length; i++)
        {
            int level = s_rand.Next(5);
            DO.Task newTask = new(0,
                tasks[i],
                descriptions[i],
                DateTime.Now.AddDays(-s_rand.Next(60)),
                false,
                (DO.EngineerExperience)complexity[i]);

            s_dalTask!.Create(newTask);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dalEngineer"></param>
    /// <param name="dalDependency"></param>
    /// <param name="dalTask"></param>
    /// <exception cref="NullReferenceException"></exception>
    public static void Do(IEngineer? dalEngineer, IDependency? dalDependency, ITask? dalTask)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createDependency();
        createEngineer();
        createTask();
    }
}