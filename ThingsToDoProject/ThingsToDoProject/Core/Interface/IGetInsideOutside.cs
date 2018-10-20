using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Interface
{
    public interface IGetInsideOutside
    {
        Task<List<DataAttributes>> GetInsideOutsideData(LocationAttributes Position, String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest);
    }
}
