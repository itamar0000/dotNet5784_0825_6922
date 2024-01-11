﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        int Create(T item); //Creates new entity object in DAL
        T? Read(int id); //Reads entity object by its ID 
        T? Read(Func<T, bool> filter);//Reads entity object by a predicate
        IEnumerable<T?> ReadAll(Func<T?,bool> filter); //stage 1 only, Reads all entity objects
        void Update(T item); //Updates entity object
        void Delete(int id); //Deletes an object by its Id
    }
}
