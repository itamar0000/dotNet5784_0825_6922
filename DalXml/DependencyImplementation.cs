namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";
    readonly string s_config_xml = "data-config";

    /// <summary>
    /// Creates a new dependency in the XML file.
    /// </summary>
    /// <param name="item">The dependency to be created.</param>
    /// <returns>The ID of the newly created dependency.</returns>
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

        return int.Parse(newItem.Element("Id")!.Value);
    }

    /// <summary>
    /// Deletes a dependency with the specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the dependency to be deleted.</param>
    public void Delete(int id)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);

        dependencysList.Elements("dependency").Where(dependency =>
            int.Parse(dependency.Element("Id")!.Value) == id).FirstOrDefault()?.Remove();

        XMLTools.SaveListToXMLElement(dependencysList, s_dependencys_xml);
    }

    /// <summary>
    /// Deletes all dependencies from the XML file.
    /// </summary>
    public void DeleteAll()
    {
        XElement configlist = XMLTools.LoadListFromXMLElement(s_config_xml);

        XElement? root = XMLTools.LoadListFromXMLElement(s_dependencys_xml);

        IEnumerable<XElement?> dependencys = (from dep in root.Elements() select dep);
        // Remove all the dependencies
        dependencys.Remove();
        // Save the updated XML back to the file
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
        configlist.Element("NextDependencyId").Value = "1000";
        XMLTools.SaveListToXMLElement(configlist, s_config_xml);
    }

    /// <summary>
    /// Reads a dependency with the specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the dependency to be read.</param>
    /// <returns>The read dependency.</returns>
    /// <exception cref="DalDoesNotExistException">Thrown if the dependency with the specified ID does not exist.</exception>
    public Dependency? Read(int id)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? dependencyElement = dependencysList.Elements("dependency").Where(dependency =>
            int.Parse(dependency.Element("Id")!.Value) == id).FirstOrDefault();

        if (dependencyElement == null)
            throw new DalDoesNotExistException($"Dependency with ID = {id} does not exist");

        return new Dependency(
            Id: int.Parse(dependencyElement.Element("Id")!.Value),
            DependentTask: int.Parse(dependencyElement.Element("DependentTask")?.Value!),
            DependensOnTask: int.Parse(dependencyElement.Element("DependensOnTask")?.Value!)
        );
    }

    /// <summary>
    /// Reads a dependency based on the provided filter from the XML file.
    /// </summary>
    /// <param name="filter">The filter predicate for reading the dependency.</param>
    /// <returns>The read dependency.</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? dependencyElement = dependencysList.Elements("dependency").Where(dependency =>
            filter(new Dependency(
                Id: int.Parse(dependency.Element("Id")!.Value),
                DependentTask: int.Parse(dependency.Element("DependentTask")?.Value!),
                DependensOnTask: int.Parse(dependency.Element("DependensOnTask")?.Value!)
            ))).FirstOrDefault();
        Dependency found = GetDependency(dependencyElement!);
        return found;
    }

    /// <summary>
    /// Reads all dependencies from the XML file based on the provided filter.
    /// </summary>
    /// <param name="filter">The filter predicate for reading dependencies.</param>
    /// <returns>The collection of read dependencies.</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        if (filter == null)
        {
            return dependencysList.Elements("dependency").Select(dependencyElement => GetDependency(dependencyElement));
        }
        else
        {
            return dependencysList.Elements("dependency").Where(dependencyElement =>
                filter(GetDependency(dependencyElement))).Select(dependencyElement => GetDependency(dependencyElement));
        }
    }

    /// <summary>
    /// Updates an existing dependency in the XML file.
    /// </summary>
    /// <param name="item">The dependency to be updated.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if the dependency with the specified ID does not exist.</exception>
    public void Update(Dependency item)
    {
        XElement dependencysList = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? dependencyElement = dependencysList.Elements("dependency").Where(dependency =>
            int.Parse(dependency.Element("Id")!.Value) == item.Id).FirstOrDefault();

        if (dependencyElement == null)
            throw new DalDoesNotExistException($"Dependency with ID = {item.Id} does not exist");

        dependencysList.Elements("dependency").Where(dependency =>
            int.Parse(dependency.Element("Id")!.Value) == item.Id).FirstOrDefault()?.Remove();

        dependencyElement.Element("DependentTask")!.Value = item.DependentTask.ToString()!;
        dependencyElement.Element("DependensOnTask")!.Value = item.DependensOnTask.ToString()!;

        dependencysList.Add(dependencyElement);

        XMLTools.SaveListToXMLElement(dependencysList, s_dependencys_xml);
    }

    /// <summary>
    /// Constructs a Dependency object from the provided XElement.
    /// </summary>
    /// <param name="dependencyElement">The XElement representing a dependency.</param>
    /// <returns>The constructed Dependency object.</returns>
    Dependency GetDependency(XElement dependencyElement)
    {
        return new Dependency(
            Id: int.Parse(dependencyElement.Element("Id")!.Value),
            DependentTask: int.Parse(dependencyElement.Element("DependentTask")?.Value!),
            DependensOnTask: int.Parse(dependencyElement.Element("DependensOnTask")?.Value!)
        );
    }
}