using System;
using System.Web.Http;
using DataHub.Data;
using DataHub.Domain.Entities;

namespace DataHub.Api.Controllers
{
    [Route("api/orgunit-change-requests")]
    public class OrgUnitChangeRequestsController : ApiController
    {
        private readonly GenericRepository<OrgUnitChangeRequest> _repository;

        public OrgUnitChangeRequestsController(GenericRepository<OrgUnitChangeRequest> repository)
        {
            _repository = repository;
        }

        // GET: api/OrgUnitChangeRequests
        public IHttpActionResult Get()
        {
            try
            {
                var requests = _repository.AllInclude(oucr => oucr.OrgUnitChangeRequestItems);

                if (requests == null)
                {
                    return NotFound();
                }

                return Ok(requests);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}