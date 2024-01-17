namespace DO;

/// <summary>
/// Exception for variable that does not exist
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Exception for variable that already exist
/// </summary>
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

/// <summary>
/// Exception for variable that must not be deleted
/// </summary>
[Serializable]
public class DalDeletionImpossible : Exception
{   //this exception is not needed in this level !!!!
    public DalDeletionImpossible(string? message) : base(message) { }
}

/// <summary>
/// 
/// </summary>
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}