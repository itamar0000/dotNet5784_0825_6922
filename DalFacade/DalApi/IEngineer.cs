namespace DalApi;
using DO;

public interface IEngineer
{
    /// <summary>
    /// Creates new entity object in DAL
    /// </summary>
    int Create(Engineer item);

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    Engineer? Read(int id);

    /// <summary>
    ///  stage 1 only, Reads all entity objects
    /// </summary>
    List<Engineer> ReadAll();

    /// <summary>
    /// Updates entity object
    /// </summary>
    void Update(Engineer item);

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    void Delete(int id); 
}
