
namespace Dal;
using DalApi;
using System;
using System.Xml.Linq;

internal class ClockImplementation : IClock
{
    static readonly string s_fileName = "data-config";
    public DateTime? GetEndDate()
    {
       XElement element= XMLTools.LoadListFromXMLElement(s_fileName).Element("StartDate")!;
        if (element.Value == "")
            return null;
        return DateTime.Parse(element.Value);
    }

    public DateTime? GetStartDate()
    {
        XElement element= XMLTools.LoadListFromXMLElement(s_fileName).Element("EndDate")!;
        if (element.Value == "")
            return null;
        return DateTime.Parse(element.Value);
    }

    public void SetEndDate(DateTime time)
    {
       XElement root=XMLTools.LoadListFromXMLElement(s_fileName);
        root.Element("EndDate")!.Value = time.ToString();
        XMLTools.SaveListToXMLElement(root, s_fileName);
    }

    public void SetStartDate(DateTime time)
    {
        XElement root=XMLTools.LoadListFromXMLElement(s_fileName);
        root.Element("StartDate")!.Value = time.ToString();
        XMLTools.SaveListToXMLElement(root, s_fileName);
        
    }
}
