namespace DataHub.Api.Dtos
{
    public class EmployeePositionLinks
    {
        public EmployeePositionLinksBelongsTo BelongsTo { get; set; }
        public EmployeePositionLinksBasedOn BasedOn { get; set; }
    }
}