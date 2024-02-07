namespace BO;
using System.Reflection;
using System.Collections;
static public class Tools
{
    public static string ToStringProperty<T>(this T obj)
    {
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        string result = type.Name + " {" + Environment.NewLine;

        foreach (var property in properties)
        {
            result += "  " + property.Name + ": ";
            object value = property.GetValue(obj);

            if (value is List<TaskInList> collection)
            {
                result += "[" + Environment.NewLine;
                foreach (var item in collection)
                {
                    result += "    " + item + "," + Environment.NewLine;
                }
                result += "  ]" + Environment.NewLine;
            }
            else
            {
                result += value + "," + Environment.NewLine;
            }
        }

            result += "}" + Environment.NewLine;
            return result;
        
     
    }
}

