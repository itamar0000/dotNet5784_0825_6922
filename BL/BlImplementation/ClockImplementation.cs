
using BlApi;
using System.Data.Common;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BlImplementation;

internal class ClockImplementation : BlApi.IClock
{
    static readonly string s_dal = "dal-config";
    public DateTime? GetEndDate()
    {
      
    }

    public DateTime? GetStartDate()
    {
        throw new NotImplementedException();
    }

    public void SetEndDate(DateTime time)
    {
        throw new NotImplementedException();
    }

    public void SetStartDate(DateTime time)
    {
        throw new NotImplementedException();
    }
}
