namespace DataHub.Api.Dtos
{
    public class EmployeeAddress
    {
        public string Type { get; set; }
        public string Formatted { get; set; }
        public string StreetHouseNo { get; set; }
        public string Postcode { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
    }
}