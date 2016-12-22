using System.Collections.Generic;
using System.Globalization;
using DataHub.Domain.Entities;

namespace DataHub.Api.Factories
{
    public class EmployeeFactory
    {
        public Dtos.Employee CreateEmployee(Employee employee)
        {

            var links = new Dtos.EmployeeLinks();

            if (employee.Manager != null)
            {
                links.Manager = new Dtos.EmployeeManager
                {
                    Employee = new Dtos.EmployeeManagerEmployee
                    {
                        Id = employee.Manager.Id,
                        FullName = employee.Manager.FullName,
                        Href = null
                    }
                };
            }

            var orgUnit = new Dtos.OrganizationUnit();

            if (employee.OrgUnit != null)
            {
                orgUnit.Id = int.Parse(employee.OrgUnit.Id);
                orgUnit.Href = null;
                orgUnit.Name = employee.OrgUnit.Title;
                orgUnit.CostCentre = employee.OrgUnit.CostCentre;
                orgUnit.FullHierarchy = new Dtos.OrganizationUnitHierarchy
                {
                    NameBased = employee.OrgUnit.NameBasedHierarchy,
                    IdBased = employee.OrgUnit.IdBasedHierarchy
                };
            }

            var holds = new List<Dtos.EmployeeLinksHold>
            {
                new Dtos.EmployeeLinksHold
                {
                    StartDate = employee.PositionStartDate?.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
                    Position = new Dtos.EmployeePosition
                    {
                        Id = employee.PositionId,
                        Href = null,
                        Title = employee.PositionTitle,
                        Links = new Dtos.EmployeePositionLinks
                        {
                            BelongsTo = new Dtos.EmployeePositionLinksBelongsTo
                            {
                                OrganizationUnit = orgUnit
                            },
                            BasedOn = new Dtos.EmployeePositionLinksBasedOn
                            {
                                JobProfile = new Dtos.EmployeePositionBasedOnJobProfile
                                {
                                    Id = employee.JobCodeId,
                                    Href = null,
                                    Code = employee.WorkCode,
                                    Title = employee.JobTitle,
                                    Band = employee.Band
                                }
                            }
                        }
                    }
                }
            };

            links.Holds = holds;

            var dtoEmployee = new Dtos.Employee
            {
                Id = employee.Id,
                Href = string.Empty,
                UserName = employee.UserName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                PreferredName = employee.PreferredName,
                FullName = employee.FullName,
                Addresses = new List<Dtos.EmployeeAddress>
                {
                    new Dtos.EmployeeAddress
                    {
                        Type = "work",
                        StreetHouseNo = employee.WorkStreetHouseNo,
                        Postcode = employee.WorkPostcode,
                        Suburb = employee.WorkSuburb,
                        State = employee.WorkState,
                        CountryCode = employee.WorkCountryCode
                    },
                    new Dtos.EmployeeAddress
                    {
                        Type = "work_mailing",
                        StreetHouseNo = employee.WorkMailingStreetHouseNo,
                        Postcode = employee.WorkMailingPostcode,
                        Suburb = employee.WorkMailingSuburb,
                        State = employee.WorkMailingState,
                        CountryCode = employee.WorkMailingCountryCode
                    }
                },
                Contacts = new List<Dtos.EmployeeContact>
                {
                    new Dtos.EmployeeContact
                    {
                        Type = "work",
                        TelephoneNo = employee.WorkTelephoneNo,
                        MobileNo = employee.WorkMobileNo,
                        EmailAddress = employee.WorkEmailAddress
                    },
                    new Dtos.EmployeeContact
                    {
                        Type = "home",
                        TelephoneNo = employee.HomeTelephoneNo,
                        MobileNo = employee.HomeMobileNo
                    }
                },
                BusinessUnit = employee.BusinessUnit,
                WorkLocation = employee.WorkLocation,
                WorkSubLocation = employee.WorkSubLocation,
                Group = employee.Group,
                SubGroup = employee.SubGroup,
                ContractType = employee.ContractType,
                TimesheetScenarioCode = employee.TimesheetScenarioCode,
                CostCentre = employee.CostCentre,
                Links = links
            };

            return dtoEmployee;
        }
    }
}
