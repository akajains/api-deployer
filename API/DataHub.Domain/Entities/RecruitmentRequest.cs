using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Recruitment_Request")]
    public partial class RecruitmentRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RecruitmentRequest()
        {
            RecruitmentDirectEmployees = new HashSet<RecruitmentDirectEmployee>();
            RecruitmentDirectExternals = new HashSet<RecruitmentDirectExternal>();
        }

        [Required]
        public Guid Id { get; set; }

        [Column("No_Of_Positions")]
        public int NoOfPositions { get; set; }

        [Column("Role_Name")]
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Column("Role_Description")]
        [Required]
        [StringLength(255)]
        public string RoleDescription { get; set; }

        [Column("Request_Type")]
        [Required]
        [StringLength(50)]
        public string RequestType { get; set; }

        [Column("Role_Type")]
        [Required]
        [StringLength(50)]
        public string RoleType { get; set; }

        [Column("Start_Date", TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column("End_Date", TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Column("Is_Fte")]
        public bool IsFte { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

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
        public virtual ICollection<RecruitmentDirectEmployee> RecruitmentDirectEmployees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecruitmentDirectExternal> RecruitmentDirectExternals { get; set; }
    }
}
