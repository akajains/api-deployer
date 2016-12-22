using System.Linq;
using DataHub.Domain.Entities;

namespace DataHub.Api.Factories
{
    public class JobcodeChangeRequestFactory
    {
        private readonly ApprovalFactory _approvalFactory = new ApprovalFactory();

        public Dtos.JobcodeChangeRequest CreateJobcodeChangeRequest(JobcodeChangeRequest request)
        {
            var requestDto = new Dtos.JobcodeChangeRequest
            {
                Id = request.Id,
                Href = null,
                Status = new Dtos.Status
                {
                    Code = request.Status,
                    Comment = request.StatusComment
                },
                Links = new Dtos.JobcodeChangeRequestLinks
                {
                    Requester = new Dtos.Requester
                    {
                        Employee = new Dtos.EmployeeSummary
                        {
                            Id = request.RequesterId,
                            FullName = request.RequesterFullName,
                            EmailId = request.RequesterEmail
                        }
                    },
                    //Approvals = request.Approvals.Select(a => _approvalFactory.CreateApproval(a)).ToList(),
                    TrackedBy = new Dtos.TrackedBy
                    {
                        Ticket = new Dtos.Ticket
                        {
                            Id = request.TrackedByTicketId,
                            Href = request.TrackedByTicketUri
                        }
                    },
                    Amendment = new Dtos.JobcodeChangeRequestAmendment
                    {
                        EffectiveFrom = request.EffectiveFrom,
                        Comment = request.Comment,
                        Action = request.Action,
                        Position = new Dtos.Position
                        {
                            Id = request.PositionId,
                            Href = null,
                            BasedOn = new Dtos.PositionBasedOn
                            {
                                Jobprofile = new Dtos.JobProfile
                                {
                                    Id = "not sure if we have this",
                                    Href = null
                                }
                            }
                        }
                    }
                }
            };

            return requestDto;
        }
    }
}
