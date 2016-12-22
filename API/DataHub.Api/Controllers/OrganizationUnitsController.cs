using System;
using System.Threading.Tasks;
using System.Web.Http;
using DataHub.Data;

namespace DataHub.Api.Controllers
{
    /// <summary>
    /// Defines properties and methods for organisation units API controller.
    /// </summary>
    [RoutePrefix("api/organization-units")]
    public class OrganizationUnitsController : ApiController
    {
        private readonly IDataHubEfRepository _repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="OrganizationUnitsController"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public OrganizationUnitsController(IDataHubEfRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// GET: api/OrganizationUnits
        /// </remarks>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            try
            {
                var orgUnits = await _repository.GetOrganizationUnitsAsync(
                    ou => ou.ParentOrgUnit);

                if (orgUnits == null)
                {
                    return NotFound();
                }

                return Ok(orgUnits);
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
        /// GET: api/OrganizationUnits/1
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name = "GetOrganizationUnitById")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            try
            {
                var orgUnit = await _repository.GetOrganizationUnitAsync(
                    id.ToString(),
                    ou => ou.ParentOrgUnit);

                if (orgUnit == null)
                {
                    return NotFound();
                }

                return Ok(orgUnit);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}