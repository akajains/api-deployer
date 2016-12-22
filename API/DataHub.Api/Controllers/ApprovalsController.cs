using System;
using System.Threading.Tasks;
using System.Web.Http;
using DataHub.Data;
using DataHub.Api.Factories;
using Ploeh.Hyprlinkr;

namespace DataHub.Api.Controllers
{
    /// <summary>
    /// Defines properties and methods for approvals API controller.
    /// </summary>
    [RoutePrefix("api/approvals")]
    public class ApprovalsController : ApiController
    {
        private readonly IDataHubEfRepository _repository;
        private readonly ApprovalFactory _approvalFactory = new ApprovalFactory();

        /// <summary>
        /// Initialises a new instance of the <see cref="ApprovalsController"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public ApprovalsController(IDataHubEfRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            try
            {
                var approval = await _repository.GetApprovalAsync(id);

                if (approval == null)
                {
                    return NotFound();
                }

                var approvalDto = _approvalFactory.CreateApproval(approval);

                var linker = new RouteLinker(Request);
                approvalDto.Href = (await linker.GetUriAsync<ApprovalsController, IHttpActionResult>(a => a.GetAsync(approvalDto.Id))).ToString();

                return Ok(approvalDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}