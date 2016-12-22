using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Data
{
    public class DataHubEfRepository : IDataHubEfRepository
    {
        private readonly DataHubContext _ctx;

        public DataHubEfRepository(DataHubContext ctx)
        {
            _ctx = ctx;
        }

        #region PEX

        public async Task<Employee> GetEmployeeAsync(string id, params Expression<Func<Employee, object>>[] includeProperties)
        {
            IQueryable<Employee> queryable = _ctx.Employees.AsNoTracking();

            var queryableWithIncludes = includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));

            return await queryableWithIncludes.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeManagerHierarchyAsync(int employeeId, int level)
        {
            return
                await _ctx.Employees.FromSql("SELECT * FROM dbo.GetManagerHierarchy({0}, {1})", employeeId, level)
                    .Include(e => e.Manager)
                    .Include(e => e.OrgUnit)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeReporteeHierarchyAsync(int employeeId, int level)
        {
            return
                await _ctx.Employees.FromSql("SELECT * FROM dbo.GetReporteeHierarchy({0}, {1})", employeeId, level)
                    .Include(e => e.Manager)
                    .Include(e => e.OrgUnit)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string name, string emailAddress, string orgUnitId, string positionId, string managerId, params Expression<Func<Employee, object>>[] includeProperties)
        {
            var employees = _ctx.Employees.AsNoTracking()
                .Where(e => string.IsNullOrEmpty(name) || e.FullName.Contains(name))
                .Where(e => string.IsNullOrEmpty(emailAddress) || e.WorkEmailAddress == emailAddress)
                .Where(e => string.IsNullOrEmpty(orgUnitId) || e.OrgUnitId == orgUnitId)
                .Where(e => string.IsNullOrEmpty(positionId) || e.PositionId == positionId)
                .Where(e => string.IsNullOrEmpty(managerId) || e.ManagerId == managerId);

            return await includeProperties.Aggregate(employees, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public async Task<JobCode> GetJobCodeAsync(int id)
        {
            // TODO: Get database key changed to int or long from float.
            return await _ctx.JobCodes.AsNoTracking().SingleOrDefaultAsync(jc => jc.Id == id);
        }

        public async Task<IEnumerable<JobCode>> GetJobCodesAsync(string band, string title)
        {
            return await _ctx.JobCodes.AsNoTracking()
                .Where(jc => string.IsNullOrEmpty(band) || jc.Band == band)
                .Where(jc => string.IsNullOrEmpty(title) || jc.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<OrgUnit> GetOrganizationUnitAsync(string id, params Expression<Func<OrgUnit, object>>[] includeProperties)
        {
            IQueryable<OrgUnit> orgUnits = _ctx.OrgUnits.AsNoTracking();
            return await includeProperties.Aggregate(orgUnits, (current, includeProperty) => current.Include(includeProperty)).SingleOrDefaultAsync(ou => ou.Id == id);
        }

        public async Task<IEnumerable<OrgUnit>> GetOrganizationUnitsAsync(params Expression<Func<OrgUnit, object>>[] includeProperties)
        {
            IQueryable<OrgUnit> queryable = _ctx.OrgUnits.AsNoTracking();
            return await includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public async Task<Position> GetPositionAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Simplified Interactions

        public async Task<Approval> GetApprovalAsync(Guid id)
        {
            return await _ctx.Approvals.AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<JobcodeChangeRequest> GetJobcodeChangeRequestAsync(Guid id)
        {
            return await _ctx.JobcodeChangeRequests.AsNoTracking()
                .Include(jccr => jccr.Approvals)
                .SingleOrDefaultAsync(jccr => jccr.Id == id);
        }

        public async Task<IEnumerable<JobcodeChangeRequest>> GetJobcodeChangeRequestsAsync(string requesterId, DateTime? creationDateFrom, DateTime? creationDateTo, string status, string approverId)
        {
            var jobcodeChangeRequests = _ctx.JobcodeChangeRequests.AsNoTracking();

            if (!string.IsNullOrEmpty(requesterId))
            {
                jobcodeChangeRequests = jobcodeChangeRequests.Where(jccr => jccr.RequesterId == requesterId);
            }

            if (creationDateFrom != null)
            {
                jobcodeChangeRequests = jobcodeChangeRequests.Where(jccr => jccr.CreatedOn >= creationDateFrom.Value);
            }

            if (creationDateTo != null)
            {
                jobcodeChangeRequests = jobcodeChangeRequests.Where(jccr => jccr.CreatedOn <= creationDateTo.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                jobcodeChangeRequests = jobcodeChangeRequests.Where(jccr => jccr.Status == status);
            }

            if (!string.IsNullOrEmpty(approverId))
            {
                jobcodeChangeRequests = jobcodeChangeRequests.Where(jccr => jccr.Approvals.Any(a => a.ApproverId == approverId));
            }

            return await jobcodeChangeRequests.Include(jccr => jccr.Approvals).ToListAsync();
        }

        public async Task<IEnumerable<OrgUnitChangeRequest>> GetOrgUnitChangeRequestsAsync()
        {
            return await _ctx.OrgUnitChangeRequests.AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
