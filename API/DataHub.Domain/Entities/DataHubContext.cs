using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataHub.Domain.Entities
{
    public partial class DataHubContext : DbContext
    {

        public virtual DbSet<Approval> Approvals { get; set; }
        public virtual DbSet<JobcodeChangeRequestApproval> JobCodeChangeRequestApprovals { get; set; }
        public virtual DbSet<OrgUnitChangeRequest> OrgUnitChangeRequests { get; set; }
        public virtual DbSet<OrgUnitChangeRequestItem> OrgUnitChangeRequestItems { get; set; }
        public virtual DbSet<PositionChangeRequest> PositionChangeRequests { get; set; }
        public virtual DbSet<PositionChangeRequestItem> PositionChangeRequestItems { get; set; }
        public virtual DbSet<JobcodeChangeRequest> JobcodeChangeRequests { get; set; }
        public virtual DbSet<RecruitmentDirectEmployee> RecruitmentDirectEmployees { get; set; }
        public virtual DbSet<RecruitmentDirectExternal> RecruitmentDirectExternals { get; set; }
        public virtual DbSet<RecruitmentRequest> RecruitmentRequests { get; set; }
        public virtual DbSet<RemunerationIncreaseRequest> RemunerationIncreaseRequests { get; set; }
        public virtual DbSet<VoluntaryRedundancyRequest> VoluntaryRedundancyRequests { get; set; }

        // PEX tables - not finalised
        public virtual DbSet<Employee> Employees { get; set; }
        //public virtual DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        //public virtual DbSet<EmployeeContact> EmployeeContacts { get; set; }
        //public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }
        //public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<OrgUnit> OrgUnits { get; set; }
        public virtual DbSet<JobCode> JobCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DataHubContext"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobcodeChangeRequest>()
                .HasMany(jccr => jccr.Approvals)
                .WithOne(a => a.JobCodeChangeRequest)
                .HasForeignKey(a => a.JobcodeChangeRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrgUnitChangeRequest>()
                .HasMany(e => e.OrgUnitChangeRequestItems)
                .WithOne(e => e.OrgUnitChangeRequest)
                .HasForeignKey(e => e.OrgUnitChangeRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PositionChangeRequest>()
                .HasMany(e => e.PositionChangeRequestItems)
                .WithOne(e => e.PositionChangeRequest)
                .HasForeignKey(e => e.PositionChangeRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecruitmentRequest>()
                .HasMany(e => e.RecruitmentDirectEmployees)
                .WithOne(e => e.RecruitmentRequest)
                .HasForeignKey(e => e.RecruitmentRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecruitmentRequest>()
                .HasMany(e => e.RecruitmentDirectExternals)
                .WithOne(e => e.RecruitmentRequest)
                .HasForeignKey(e => e.RecruitmentRequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
