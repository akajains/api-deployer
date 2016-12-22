using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Voluntary_Redundancy_Request")]
    public partial class VoluntaryRedundancyRequest
    {
        public Guid Id { get; set; }

        [Column("Is_Interested_In_Redeployment")]
        public bool IsInterestedInRedeployment { get; set; }

        [Column("Is_Manager_Consulted")]
        public bool IsManagerConsulted { get; set; }

        [Column("Is_Participating_To_Job_Reduction")]
        public bool IsParticipatingToJobReduction { get; set; }

        [Column("Supporting_Comment")]
        [StringLength(255)]
        public string SupportingComment { get; set; }

        [Column("Employee_Id")]
        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Column("Employee_Full_Name")]
        [Required]
        [StringLength(255)]
        public string EmployeeFullName { get; set; }

        [Column("Employee_Email")]
        [Required]
        [StringLength(255)]
        public string EmployeeEmail { get; set; }

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
    }
}
