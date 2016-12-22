using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Position")]
    public partial class Position
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Position()
        {
            EmployeePositions = new HashSet<EmployeePosition>();
        }

        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Column("Title")]
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Column("IsManagerial")]
        [Required]
        public bool IsManagerial { get; set; }

        [Column("OrgUnitId")]
        [Required]
        [StringLength(50)]
        public string OrgUnitId { get; set; }

        [Column("JobProfileId")]
        [Required]
        [StringLength(50)]
        public string JobProfileId { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }

        public virtual JobCode JodeCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }
    }
}
