using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        /// <summary>
        /// create an entity object in DAL
        /// </summary>
        /// <returns>the id of the new object</returns>
        int Create(T item);

        /// <summary>
        /// Reads entity object by its ID 
        /// </summary>
        /// <returns>copy of the object if was found else null</returns>
        T? Read(int id);

        /// <summary>
        /// Reads entity object by a predicate
        /// </summary>
        /// <returns>>copy of object if was found else null</returns>
        T? Read(Func<T, bool> filter);

        /// <summary>
        /// stage 1 only, Reads all entity objects
        /// </summary>
        /// <returns>an copy of the list with all the objects that suit the filter</returns>
        IEnumerable<T?> ReadAll(Func<T?,bool>? filter=null);

        /// <summary>
        /// Updates entity object
        /// </summary>
        void Update(T item);

        /// <summary>
        /// Deletes an object by its Id
        /// </summary>
        void Delete(int id); 
    }
}
