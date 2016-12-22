using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataHub.Domain.Entities;

namespace DataHub.Data
{
    public interface IDataHubEfRepository
    {
        #region PEX

        Task<Employee> GetEmployeeAsync(string id, params Expression<Func<Employee, object>>[] includeProperties);

        Task<IEnumerable<Employee>> GetEmployeeManagerHierarchyAsync(int id, int level);

        Task<IEnumerable<Employee>> GetEmployeeReporteeHierarchyAsync(int id, int level);

        Task<IEnumerable<Employee>> GetEmployeesAsync(string name, string emailAddress, string orgUnitId, string positionId, string managerId, params Expression<Func<Employee, object>>[] includeProperties);

        Task<JobCode> GetJobCodeAsync(int id);

        Task<IEnumerable<JobCode>> GetJobCodesAsync(string band, string title);

        Task<OrgUnit> GetOrganizationUnitAsync(string id, params Expression<Func<OrgUnit, object>>[] includeProperties);

        Task<IEnumerable<OrgUnit>> GetOrganizationUnitsAsync(params Expression<Func<OrgUnit, object>>[] includeProperties);

        Task<Position> GetPositionAsync(int id);

        #endregion

        #region Simplified Interactions

        Task<Approval> GetApprovalAsync(Guid id);

        Task<JobcodeChangeRequest> GetJobcodeChangeRequestAsync(Guid id);

        Task<IEnumerable<JobcodeChangeRequest>> GetJobcodeChangeRequestsAsync(string requesterId, DateTime? creationDateFrom, DateTime? creationDateTo, string status, string approverId);

        Task<IEnumerable<OrgUnitChangeRequest>> GetOrgUnitChangeRequestsAsync();

        #endregion
    }
}