using System;

namespace DataHub.Api.Dtos
{
    public class JobcodeChangeRequestAmendment
    {
        public DateTime EffectiveFrom { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
        public Position Position { get; set; }
    }
}
