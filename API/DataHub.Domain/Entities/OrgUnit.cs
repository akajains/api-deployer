using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("OrgUnit")]
    public partial class OrgUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrgUnit()
        {
            //Positions = new HashSet<Position>();
        }

        [Column("OuID")]
        [Key]
        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Column("costCentre")]
        [Required]
        [StringLength(50)]
        public string CostCentre { get; set; }

        [Column("ssuStatus")]
        [Required]
        [StringLength(50)]
        public string SsuCode { get; set; }

        [Column("PathName")]
        [Required]
        [StringLength(255)]
        public string NameBasedHierarchy { get; set; }

        [Column("PathId")]
        [Required]
        [StringLength(255)]
        public string IdBasedHierarchy { get; set; }

        [Column("managerId")]
        [StringLength(50)]
        public string ManagerId { get; set; }

        [Column("PARENTOuID")]
        [StringLength(50)]
        public string ParentOrgUnitId { get; set; }

        public virtual OrgUnit ParentOrgUnit { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Position> Positions { get; set; }
    }
}
