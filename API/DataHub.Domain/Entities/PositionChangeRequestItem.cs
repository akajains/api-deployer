using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Position_Change_Request_Item")]
    public partial class PositionChangeRequestItem
    {
        public Guid Id { get; set; }

        [Column("Ghrom_Position_Change_Request_Id")]
        public Guid PositionChangeRequestId { get; set; }

        [Column("Effective_From", TypeName = "date")]
        public DateTime EffectiveFrom { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [Required]
        [StringLength(255)]
        public string Action { get; set; }

        [Column("Position_Id")]
        [Required]
        [StringLength(50)]
        public string PositionId { get; set; }

        [Column("Position_Title")]
        [Required]
        [StringLength(50)]
        public string PositionTitle { get; set; }

        [Column("Is_Position_Managerial")]
        public bool IsPositionManagerial { get; set; }

        [Column("Position_Org_Unit_Id")]
        [Required]
        [StringLength(50)]
        public string PositionOrgUnitId { get; set; }

        [Column("Position_Job_Profile_Id")]
        [Required]
        [StringLength(50)]
        public string PositionJobProfileId { get; set; }

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

        public virtual PositionChangeRequest PositionChangeRequest { get; set; }
    }
}
