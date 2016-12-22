namespace DataHub.Api.Dtos
{
    public class JobCode
    {
        public int Id { get; set; }
        public string Href { get; set; }
        public string WorkCode { get; set; }
        public string Title { get; set; }
        public string Band { get; set; }
        public decimal? Minimum { get; set; }
        public decimal? Maximum { get; set; }
    }
}
