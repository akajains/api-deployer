using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Org_Unit_Change_Request")]
    public partial class OrgUnitChangeRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrgUnitChangeRequest()
        {
            OrgUnitChangeRequestItems = new HashSet<OrgUnitChangeRequestItem>();
        }

        public Guid Id { get; set; }

        [Column("Requester_Id")]
        [Required]
        [StringLength(50)]
        public string RequesterId { get; set; }

        [Column("Requester_Full_Name")]
        [Required]
        [StringLength(255)]
        public string RequesterFullName { get; set; }

        [Column("Requester_Email")]
        [Required]
        [StringLength(255)]
        public string RequesterEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Column("Status_Comment")]
        [StringLength(255)]
        public string StatusComment { get; set; }

        [Column("Tracked_By_Ticket_Id")]
        [Required]
        [StringLength(50)]
        public string TrackedByTicketId { get; set; }

        [Column("Tracked_By_Ticket_Uri")]
        [Required]
        [StringLength(255)]
        public string TrackedByTicketUri { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrgUnitChangeRequestItem> OrgUnitChangeRequestItems { get; set; }
    }
}
