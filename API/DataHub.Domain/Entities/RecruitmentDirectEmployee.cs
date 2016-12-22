using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Recruitment_Direct_Employee")]
    public partial class RecruitmentDirectEmployee
    {
        public Guid Id { get; set; }

        [Column("Ghrom_Recruitment_Request_Id")]
        public Guid RecruitmentRequestId { get; set; }

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

        public virtual RecruitmentRequest RecruitmentRequest { get; set; }
    }
}
