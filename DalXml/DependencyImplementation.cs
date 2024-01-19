namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";

    public int Create(Dependency item)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement newItem = new XElement("dependency");

        newItem.Add(new XElement("Id", Config.NextDependencyId));

        if (item.DependentTask != null)
            newItem.Add(new XElement("DependentTask", item.DependentTask));

        if (item.DependensOnTask != null)   
            newItem.Add(new XElement("DependensOnTask", item.DependensOnTask));

        dependencysList.Add(newItem);
        XMLTools.SaveListToXMLElement(dependencysList, s_dependencys_xml);

        return int.Parse(newItem.Element("Id").Value);
    }

    public void Delete(int id)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);

        dependencysList.Elements("dependency").Where(dependency
        => int.Parse(dependency.Element("Id").Value) == id).FirstOrDefault()?.Remove();

        XMLTools.SaveListToXMLElement(dependencysList, s_dependencys_xml);
    }

    public Dependency? Read(int id)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? dependencyElement = dependencysList.Elements("dependency").Where(dependency
                   => int.Parse(dependency.Element("Id").Value) == id).FirstOrDefault();

        if (dependencyElement == null)
            throw new DalDoesNotExistException($"Dependency with ID = {id} does not exist");

        return new Dependency(
            Id: int.Parse(dependencyElement.Element("Id")!.Value),
            DependentTask: int.Parse(dependencyElement.Element("DependentTask")?.Value!),
            DependensOnTask: int.Parse(dependencyElement.Element("DependensOnTask")?.Value!)
        );

    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? dependencyElement = dependencysList.Elements("dependency").Where(dependency
                 => filter(new Dependency(
                 Id: int.Parse(dependency.Element("Id")!.Value),
                 DependentTask: int.Parse(dependency.Element("DependentTask")?.Value!),
                 DependensOnTask: int.Parse(dependency.Element("DependensOnTask")?.Value!)
                  ))).FirstOrDefault();
        Dependency found = GetDependency(dependencyElement);
        return found;
    }
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        if(filter == null)
        {
            return dependencysList.Elements("dependency").Select(dependencyElement => GetDependency(dependencyElement));
        }
        else
        {
            return dependencysList.Elements("dependency").Where(dependencyElement
            => filter(GetDependency(dependencyElement))).Select(dependencyElement => GetDependency(dependencyElement));
        }
    }

    public void Update(Dependency item)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? dependencyElement = dependencysList.Elements("dependency").Where(dependency
                              => int.Parse(dependency.Element("Id").Value) == item.Id).FirstOrDefault();

        if (dependencyElement == null)
            throw new DalDoesNotExistException($"Dependency with ID = {item.Id} does not exist");

        dependencysList.Elements("dependency").Where(dependency
        => int.Parse(dependency.Element("Id").Value) == item.Id).FirstOrDefault()?.Remove();

        dependencyElement.Element("DependentTask")!.Value = item.DependentTask.ToString();
        dependencyElement.Element("DependensOnTask")!.Value = item.DependensOnTask.ToString();

        dependencysList.Add(dependencyElement);

        XMLTools.SaveListToXMLElement(dependencysList, s_dependencys_xml);
    }

    Dependency GetDependency(XElement dependencyElement)
    {
        return new
            (
              Id: int.Parse(dependencyElement.Element("Id")!.Value),
              DependentTask: int.Parse(dependencyElement.Element("DependentTask")?.Value!),
              DependensOnTask: int.Parse(dependencyElement.Element("DependensOnTask")?.Value!)
            );
    }
}