using System;

namespace DataHub.Api.Dtos
{
    public class JobcodeChangeRequest
    {
        public Guid Id { get; set; }
        public string Href { get; set; }
        public Status Status { get; set; }
        public JobcodeChangeRequestLinks Links { get; set; }
    }
}
