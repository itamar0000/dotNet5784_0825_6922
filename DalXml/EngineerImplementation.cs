namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation:IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer item)
    {
        // get the next id from the XML file

        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        engineersList.Add(item);

        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, s_engineers_xml);

        // return the id of the new Task
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Engineer> engineerslist = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineer? item = engineerslist.Find(Engineer => Engineer.Id == id);

        if (item == null)
            throw new DalDoesNotExistException($"Engineer with ID = {id} does not exist");
       
        engineerslist.Remove(item);

        XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
    }

    public Engineer? Read(int id)
    {
        List<Engineer> engineerslist = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineer? item = engineerslist.FirstOrDefault(Engineer => Engineer.Id == id);

        if (item == null)
            throw new DalDoesNotExistException($"Engineer with ID = {id} does not exist");

        return item;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        IEnumerable<Engineer> engineerslist = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return engineerslist.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        IEnumerable<Engineer> engineerslist = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        if (filter == null)
        {
            return engineerslist;
        }
        else
        {
            return engineerslist.Where(filter);
        }
    }

    public void Update(Engineer item)
    {
        List<Engineer> engineerslist = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineer? itemToUpdate = engineerslist.Find(Engineer => Engineer.Id == item.Id);

        if (itemToUpdate == null)
            throw new DalDoesNotExistException($"Engineer with ID = {item.Id} does not exist");

        engineerslist.Remove(itemToUpdate);
        engineerslist.Add(item);

        XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
    }
}
