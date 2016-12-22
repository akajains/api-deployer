using System.Collections.Generic;

namespace DataHub.Api.Dtos
{
    public class JobcodeChangeRequestLinks
    {
        public Requester Requester { get; set; }
        public ICollection<Approval> Approvals { get; set; }
        public TrackedBy TrackedBy { get; set; } 
        public JobcodeChangeRequestAmendment Amendment { get; set; }
    }
}
