namespace DataHub.Api.Dtos
{
    public class OrganizationUnit
    {
        public int Id { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public string CostCentre { get; set; }
        public OrganizationUnitHierarchy FullHierarchy { get; set; }
    }
}
