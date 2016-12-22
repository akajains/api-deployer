using System;

namespace DataHub.Api.Dtos
{
    public class OrgUnitChangeRequestDto
    {
        public Guid Id;
        public Status Status;
        public OrgUnitChangeRequestLinksDto Links;
    }

    public class OrgUnitChangeRequestLinksDto
    {
        public OrgUnitChangeRequestRequesterDto Requester;
    }

    public class OrgUnitChangeRequestRequesterDto
    {
        public string EmployeeId;
    }
}