using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("CEOLT_Jobs_File")]
    public partial class JobCode
    {
        [Column("Job code")]
        [Required]
        public double Id { get; set; }

        [Column("Work code")]
        [Required]
        [StringLength(255)]
        public string WorkCode { get; set; }

        [Column("Job title")]
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Band { get; set; }

        [Required]
        [StringLength(255)]
        public string Minimum { get; set; }

        [Required]
        [StringLength(255)]
        public string Maximum { get; set; }
    }
}
