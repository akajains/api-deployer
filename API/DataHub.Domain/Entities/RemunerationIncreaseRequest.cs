using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Remuneration_Increase_Request")]
    public partial class RemunerationIncreaseRequest
    {
        public Guid Id { get; set; }

        [Column("Current_Rem_Fixed_Amount")]
        public decimal? CurrentRemFixedAmount { get; set; }

        [Column("Current_Rem_Risk_Percentage")]
        public decimal? CurrentRemRiskPercentage { get; set; }

        [Column("Current_Rem_Currency_Code")]
        [StringLength(50)]
        public string CurrentRemCurrencyCode { get; set; }

        [Column("Proposed_Rem_Fixed_Amount")]
        public decimal? ProposedRemFixedAmount { get; set; }

        [Column("Proposed_Rem_Risk_Percentage")]
        public decimal? ProposedRemRiskPercentage { get; set; }

        [Column("Proposed_Rem_Currency_Code")]
        [StringLength(50)]
        public string ProposedRemCurrencyCode { get; set; }

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
