namespace Dal;
using DalApi;
using System;
using System.Xml.Linq;

internal class ClockImplementation : IClock
{
    static readonly string s_fileName = "data-config";
    public DateTime? GetEndDate()
    {
        string element = XMLTools.LoadListFromXMLElement(s_fileName).Element("endDate")!.Value;
        if (element == "")
            return null;
        return DateTime.Parse(element);
    }

    public DateTime? GetStartDate()
    {
        XElement element = XMLTools.LoadListFromXMLElement(s_fileName).Element("startDate")!;
        if (element.Value == "")
            return null;
        return DateTime.Parse(element.Value);
    }

    public void SetEndDate(DateTime? time)
    {
       XElement root=XMLTools.LoadListFromXMLElement(s_fileName);
        root.Element("endDate")!.Value = time.ToString();
        XMLTools.SaveListToXMLElement(root, s_fileName);
    }

    public void SetStartDate(DateTime? time)
    {
        XElement root=XMLTools.LoadListFromXMLElement(s_fileName);
        root.Element("startDate")!.Value = time.ToString();
        XMLTools.SaveListToXMLElement(root, s_fileName);

    }
}
