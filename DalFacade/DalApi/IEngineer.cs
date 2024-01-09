namespace DalApi;
using DO;

public interface IEngineer
{
    /// <summary>
    /// Creates new entity object in DAL
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    int Create(Engineer item);
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Engineer? Read(int id); //Reads entity object by its ID 
    /// <summary>
    ///  Reads all entity objects
    /// </summary>
    /// <returns></returns>
    List<Engineer> ReadAll(); //stage 1 only,
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item"></param>
    void Update(Engineer item);
    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id"></param>
    void Delete(int id); 

}
