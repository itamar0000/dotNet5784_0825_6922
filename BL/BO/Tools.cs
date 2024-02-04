namespace BO;
using System.Reflection;
static public class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        IEnumerable<T>? enumerable= t as IEnumerable<T>;
        if (enumerable != null)
        {
            foreach (var item in enumerable)
            {
                item.ToStringProperty();
            }
        }
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
            str += "\n" + item.Name + ": " + item.GetValue(t, null);
        return (str);
    }
}