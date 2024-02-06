﻿namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public ITask Task =>  new TaskImplementation();


    public IEngineer Engineer =>  new EngineerImplementation();

    public IMilestone Milestone => throw new NotImplementedException();

    public IClock Clock => throw new NotImplementedException();
}
