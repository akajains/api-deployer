namespace DataHub.Api.Dtos
{
    public class EmployeePosition
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }
        public EmployeePositionLinks Links { get; set; }
    }
}