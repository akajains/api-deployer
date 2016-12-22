using DataHub.Domain.Entities;

namespace DataHub.Api.Factories
{
    public class ApprovalFactory
    {
        public Dtos.Approval CreateApproval(Approval approval)
        {
            var dtoApproval = new Dtos.Approval
            {
                Id = approval.Id,
                Href = null,
                ApprovalType = approval.ApprovalType,
                Status = new Dtos.Status
                {
                    Code = approval.ApprovalStatus
                },
                Sequence = approval.ApprovalSeq,
                Links = new Dtos.ApprovalLinks
                {
                    Employee = new Dtos.EmployeeSummary
                    {
                        Id = approval.ApproverId,
                        FullName = approval.ApproverFullName,
                        EmailId = approval.ApproverEmail
                    }
                }
            };

            return dtoApproval;
        }
    }
}
