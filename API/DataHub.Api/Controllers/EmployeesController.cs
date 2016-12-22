using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataHub.Api.Factories;
using DataHub.Data;
using Ploeh.Hyprlinkr;

namespace DataHub.Api.Controllers
{
    /// <summary>
    /// Defines properties and methods for employees API controller.
    /// </summary>
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        private readonly IDataHubEfRepository _repository;
        private readonly EmployeeFactory _employeeFactory = new EmployeeFactory();

        /// <summary>
        /// Initialises a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public EmployeesController(IDataHubEfRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// GET: api/employees/5
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name = "GetEmployeeById")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            try
            {
                var employee = await _repository.GetEmployeeAsync(
                    id.ToString(),
                    e => e.Manager,
                    e => e.OrgUnit);

                if (employee == null)
                {
                    return NotFound();
                }

                var employeeDto = _employeeFactory.CreateEmployee(employee);

                var linker = new RouteLinker(Request);
                await SetLinksForEmployeeAsync(employeeDto, linker);

                return Ok(employeeDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// GET: api/employees
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="orgUnit"></param>
        /// <param name="positionId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> GetAsync(string name = null, string email = null, string orgUnit = null, string positionId = null, string managerId = null)
        {
            try
            {
                var employees = await _repository.GetEmployeesAsync(name, email, orgUnit, positionId, managerId,
                    e => e.Manager,
                    e => e.OrgUnit);

                if (employees == null)
                {
                    return NotFound();
                }

                var employeesDto = employees.Select(e => _employeeFactory.CreateEmployee(e)).ToList();

                var linker = new RouteLinker(Request);
                await SetLinksForEmployeesAsync(employeesDto, linker);

                return Ok(employeesDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// GET: api/employees/{id}/manager-hierarchy
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [Route("{id}/manager-hierarchy")]
        public async Task<IHttpActionResult> GetEmployeeManagerHierarchyAsync(int id, int level)
        {
            try
            {
                var employees = await _repository.GetEmployeeManagerHierarchyAsync(id, level);

                if (employees == null)
                {
                    return NotFound();
                }

                var employeesDto = employees.Select(e => _employeeFactory.CreateEmployee(e)).ToList();

                var linker = new RouteLinker(Request);
                await SetLinksForEmployeesAsync(employeesDto, linker);

                return Ok(employeesDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// GET: api/employees/{id}/reportee-hierarchy
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [Route("{id}/reportee-hierarchy")]
        public async Task<IHttpActionResult> GetEmployeeReporteeHierarchyAsync(int id, int level)
        {
            try
            {
                var employees = await _repository.GetEmployeeReporteeHierarchyAsync(id, level);

                if (employees == null)
                {
                    return NotFound();
                }

                var employeesDto = employees.Select(e => _employeeFactory.CreateEmployee(e)).ToList();

                var linker = new RouteLinker(Request);
                await SetLinksForEmployeesAsync(employeesDto, linker);

                return Ok(employeesDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// GET: api/employees/5/include-renumeration
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/include-renumeration")]
        public async Task<IHttpActionResult> GetIncludeRenumerationAsync(int id)
        {
            try
            {
                var employee = await _repository.GetEmployeeAsync(
                    id.ToString(),
                    e => e.Manager,
                    e => e.OrgUnit);

                if (employee == null)
                {
                    return NotFound();
                }

                // TODO: Change this (and the db call) to optionally return renumeration details once we know where they are in the Data Hub.
                var employeeDto = _employeeFactory.CreateEmployee(employee);

                var linker = new RouteLinker(Request);
                await SetLinksForEmployeeAsync(employeeDto, linker);

                return Ok(employeeDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        private static async Task SetLinksForEmployeeAsync(Dtos.Employee employeeDto, IResourceLinker linker)
        {
            if (employeeDto.Id != null)
            {
                employeeDto.Href =
                (await
                    linker.GetUriAsync<EmployeesController, IHttpActionResult>(
                        a => a.GetAsync(int.Parse(employeeDto.Id)))).ToString();
            }

            if (!string.IsNullOrEmpty(employeeDto.Links.Manager?.Employee?.Id))
            {
                employeeDto.Links.Manager.Employee.Href =
                (await
                    linker.GetUriAsync<EmployeesController, IHttpActionResult>(
                        a => a.GetAsync(int.Parse(employeeDto.Links.Manager.Employee.Id)))).ToString();
            }

            if (employeeDto.Links.Holds != null)
            {
                foreach (var holdsDto in employeeDto.Links.Holds.Where(holdsDto => holdsDto.Position != null))
                {
                    if (!string.IsNullOrEmpty(holdsDto.Position.Id))
                    {
                        holdsDto.Position.Href =
                            (await linker.GetUriAsync<PositionsController, IHttpActionResult>(p => p.GetAsync(int.Parse(holdsDto.Position.Id)))).ToString();
                    }

                    if (holdsDto.Position.Links.BelongsTo.OrganizationUnit != null)
                    {
                        holdsDto.Position.Links.BelongsTo.OrganizationUnit.Href =
                            (await linker.GetUriAsync<OrganizationUnitsController, IHttpActionResult>(
                                ou => ou.GetAsync(holdsDto.Position.Links.BelongsTo.OrganizationUnit.Id))).ToString();
                    }

                    if (holdsDto.Position.Links.BasedOn.JobProfile != null)
                    {
                        int jobCodeId;
                        if (int.TryParse(holdsDto.Position.Links.BasedOn.JobProfile.Id, out jobCodeId))
                        {
                            holdsDto.Position.Links.BasedOn.JobProfile.Href =
                                (await linker.GetUriAsync<JobCodesController, IHttpActionResult>(ou => ou.GetAsync(jobCodeId))).ToString();
                        }
                    }
                }
            }
        }

        private static async Task SetLinksForEmployeesAsync(IEnumerable<Dtos.Employee> employeesDto, IResourceLinker linker)
        {
            foreach (var employeeDto in employeesDto.Where(employeeDto => employeeDto.Id != null))
            {
                await SetLinksForEmployeeAsync(employeeDto, linker);
            }

        }
    }
}