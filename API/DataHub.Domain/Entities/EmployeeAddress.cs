using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("EmployeeAddress")]
    public partial class EmployeeAddress
    {
        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Column("EmployeeId")]
        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Column("AddressType")]
        [Required]
        [StringLength(50)]
        public string AddressType { get; set; }

        [Column("Formatted")]
        [StringLength(255)]
        public string Formatted { get; set; }

        [Column("StreetHouseNo")]
        [StringLength(255)]
        public string StreetHouseNo { get; set; }

        [Column("Postcode")]
        [StringLength(255)]
        public string Postcode { get; set; }

        [Column("Suburb")]
        [StringLength(255)]
        public string Suburb { get; set; }

        [Column("State")]
        [StringLength(255)]
        public string State { get; set; }

        [Column("CountryCode")]
        [StringLength(255)]
        public string CountryCode { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
