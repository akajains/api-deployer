using System;

namespace DataHub.Api.Dtos
{
    public class Approval
    {
        public Guid Id { get; set; }
        public string Href { get; set; }
        public string ApprovalType { get; set; }
        public Status Status { get; set; }
        public int Sequence { get; set; }
        public ApprovalLinks Links { get; set; }
    }
}
