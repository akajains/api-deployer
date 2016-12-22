using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Ghrom_Recruitment_Direct_External")]
    public partial class RecruitmentDirectExternal
    {
        public Guid Id { get; set; }

        [Column("Ghrom_Recruitment_Request_Id")]
        public Guid RecruitmentRequestId { get; set; }

        [Column("Candidate_Full_Name")]
        [Required]
        [StringLength(255)]
        public string CandidateFullName { get; set; }

        [Column("Candidate_Email")]
        [Required]
        [StringLength(255)]
        public string CandidateEmail { get; set; }

        [Column("Candidate_Contact_No")]
        [Required]
        [StringLength(50)]
        public string CandidateContactNo { get; set; }

        [Column("Candidate_Agency_Name")]
        [Required]
        [StringLength(100)]
        public string CandidateAgencyName { get; set; }

        public virtual RecruitmentRequest RecruitmentRequest { get; set; }
    }
}
