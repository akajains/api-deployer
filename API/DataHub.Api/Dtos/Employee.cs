using System.Collections.Generic;

namespace DataHub.Api.Dtos
{
    public class Employee
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PreferredName { get; set; }
        public string FullName { get; set; }
        public IEnumerable<EmployeeAddress> Addresses { get; set; }
        public IEnumerable<EmployeeContact> Contacts { get; set; }
        public string BusinessUnit { get; set; }
        public string WorkLocation { get; set; }
        public string WorkSubLocation { get; set; }
        public string Group { get; set; }
        public string SubGroup { get; set; }
        public string ContractType { get; set; }
        public string TimesheetScenarioCode { get; set; }
        public string CostCentre { get; set; }
        public EmployeeLinks Links { get; set; }
    }
}