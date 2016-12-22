using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("EmployeeContact")]
    public partial class EmployeeContact
    {
        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Column("EmployeeId")]
        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Column("ContactType")]
        [Required]
        [StringLength(50)]
        public string ContactType { get; set; }

        [Column("TelephoneNo")]
        [StringLength(50)]
        public string TelephoneNo { get; set; }

        [Column("MobileNo")]
        [StringLength(50)]
        public string MobileNo { get; set; }

        [Column("EmailAddress")]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
