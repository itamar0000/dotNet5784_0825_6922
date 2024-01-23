namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineers_xml = "engineers";

    /// <summary>
    /// Creates a new engineer and inserts it into the XML file.
    /// </summary>
    /// <param name="item">The engineer to be created.</param>
    /// <returns>The ID of the newly created engineer.</returns>
    public int Create(Engineer item)
    {
        // Get the next ID from the XML file
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        engineersList.Add(item);

        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineers_xml);

        // Return the ID of the new engineer
        return item.Id;
    }

    /// <summary>
    /// Deletes an engineer with the specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the engineer to be deleted.</param>
    public void Delete(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineer? item = engineersList.Find(engineer => engineer.Id == id);

        if (item == null)
            throw new DalDoesNotExistException($"Engineer with ID = {id} does not exist");

        engineersList.Remove(item);

        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineers_xml);
    }

    /// <summary>
    /// Deletes all engineers from the XML file.
    /// </summary>
    public void DeleteAll()
    {
        List<Engineer> engineersList = new List<Engineer>();
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineers_xml);
    }

    /// <summary>
    /// Reads an engineer with the specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the engineer to be read.</param>
    /// <returns>The read engineer.</returns>
    public Engineer? Read(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineer? item = engineersList.FirstOrDefault(engineer => engineer.Id == id);

        return item;
    }

    /// <summary>
    /// Reads an engineer based on the provided filter from the XML file.
    /// </summary>
    /// <param name="filter">The filter predicate for reading the engineer.</param>
    /// <returns>The read engineer.</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        IEnumerable<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return engineersList.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all engineers from the XML file based on the provided filter.
    /// </summary>
    /// <param name="filter">The filter predicate for reading engineers.</param>
    /// <returns>The collection of read engineers.</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        IEnumerable<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        if (filter == null)
        {
            return engineersList;
        }
        else
        {
            return engineersList.Where(filter);
        }
    }

    /// <summary>
    /// Updates an existing engineer in the XML file.
    /// </summary>
    /// <param name="item">The engineer to be updated.</param>
    public void Update(Engineer item)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineer? itemToUpdate = engineersList.Find(engineer => engineer.Id == item.Id);

        if (itemToUpdate == null)
            throw new DalDoesNotExistException($"Engineer with ID = {item.Id} does not exist");

        engineersList.Remove(itemToUpdate);
        engineersList.Add(item);

        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineers_xml);
    }
}