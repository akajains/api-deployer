using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Org_Unit_Change_Request_Item")]
    public partial class OrgUnitChangeRequestItem
    {
        public Guid Id { get; set; }

        [Column("Ghrom_Org_Unit_Change_Request_Id")]
        public Guid OrgUnitChangeRequestId { get; set; }

        [Column("Effective_From", TypeName = "date")]
        public DateTime EffectiveFrom { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [Required]
        [StringLength(255)]
        public string Action { get; set; }

        [Column("Org_Unit_Id")]
        [Required]
        [StringLength(50)]
        public string OrgUnitId { get; set; }

        [Column("Org_Unit_Title")]
        [Required]
        [StringLength(50)]
        public string OrgUnitTitle { get; set; }

        [Column("Is_Position_Managerial")]
        public bool IsPositionManagerial { get; set; }

        [Column("Parent_Org_Unit_Id")]
        [Required]
        [StringLength(50)]
        public string ParentOrgUnitId { get; set; }

        [Column("Org_Unit_Manager_Id")]
        [Required]
        [StringLength(50)]
        public string OrgUnitManagerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Column("Status_Comment")]
        [StringLength(255)]
        public string StatusComment { get; set; }

        [Column("Created_On")]
        public DateTimeOffset CreatedOn { get; set; }

        [Column("Created_By")]
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column("Updated_On")]
        public DateTimeOffset UpdatedOn { get; set; }

        [Column("Updated_By")]
        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public bool Enabled { get; set; }

        public virtual OrgUnitChangeRequest OrgUnitChangeRequest { get; set; }
    }
}
