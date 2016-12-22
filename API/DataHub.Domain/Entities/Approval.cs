using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Approval")]
    public partial class Approval
    {
        public Guid Id { get; set; }

        [Column("Request_Id")]
        [Required]
        public Guid RequestId { get; set; }

        [Column("Request_Type")]
        [Required]
        [StringLength(50)]
        public string RequestType { get; set; }

        [Column("Approval_Seq")]
        public int ApprovalSeq { get; set; }

        [Column("Approval_Type")]
        [Required]
        [StringLength(50)]
        public string ApprovalType { get; set; }

        [Column("Approval_Status")]
        [Required]
        [StringLength(50)]
        public string ApprovalStatus { get; set; }

        [Column("Approver_Id")]
        [Required]
        [StringLength(50)]
        public string ApproverId { get; set; }

        [Column("Approver_Full_Name")]
        [Required]
        [StringLength(255)]
        public string ApproverFullName { get; set; }

        [Column("Approver_Email")]
        [Required]
        [StringLength(255)]
        public string ApproverEmail { get; set; }
    }
}
