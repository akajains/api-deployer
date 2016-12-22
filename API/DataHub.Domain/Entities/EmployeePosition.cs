using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("EmployeePosition")]
    public partial class EmployeePosition
    {
        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Column("EmployeeId")]
        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Column("StartDate", TypeName = "date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column("EndDate", TypeName = "date")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column("PositionId")]
        [Required]
        [StringLength(50)]
        public string PositionId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Position Position { get; set; }
    }
}
