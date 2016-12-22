using System.Collections.Generic;

namespace DataHub.Api.Dtos
{
    public class EmployeeLinks
    {
        public EmployeeManager Manager { get; set; }
        public IEnumerable<EmployeeLinksHold> Holds { get; set; }
    }
}