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
    /// Defines properties and methods for job code change requests API controller.
    /// </summary>
    [RoutePrefix("api/jobcode-change-requests")]
    public class JobcodeChangeRequestsController : ApiController
    {
        private readonly IDataHubEfRepository _repository;
        private readonly ApprovalFactory _approvalFactory = new ApprovalFactory();
        private readonly JobcodeChangeRequestFactory _jobcodeChangeRequestFactory = new JobcodeChangeRequestFactory();

        /// <summary>
        /// Initialises a new instance of the <see cref="JobcodeChangeRequestsController"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public JobcodeChangeRequestsController(IDataHubEfRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name = "GetJobcodeChangeRequestById")]
        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            try
            {
                var request = await _repository.GetJobcodeChangeRequestAsync(id);

                if (request == null)
                {
                    return NotFound();
                }

                var requestDto = _jobcodeChangeRequestFactory.CreateJobcodeChangeRequest(request);

                var linker = new RouteLinker(Request);
                await SetLinksForJobcodeChangeRequestAsync(requestDto, linker);

                return Ok(requestDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requesterId"></param>
        /// <param name="creationDateFrom"></param>
        /// <param name="creationDateTo"></param>
        /// <param name="status"></param>
        /// <param name="approverId"></param>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> GetAsync(string requesterId = null, DateTime? creationDateFrom = null, DateTime? creationDateTo = null, string status = null, string approverId = null)
        {
            try
            {
                var requests = await _repository.GetJobcodeChangeRequestsAsync(requesterId, creationDateFrom, creationDateTo, status, approverId);

                if (requests == null)
                {
                    return NotFound();
                }

                var requestsDto =
                    requests.Select(jccr => _jobcodeChangeRequestFactory.CreateJobcodeChangeRequest(jccr)).ToList();

                var linker = new RouteLinker(Request);
                await SetLinksForJobcodeChangeRequestsAsync(requestsDto, linker);

                return Ok(requestsDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [Route("{requestid}/approvals/{approvalId}", Name="GetApprovalByRequest")]
        public async Task<IHttpActionResult> GetApprovalByRequestAsync(Guid requestId, Guid approvalId)
        {
            try
            {
                var approval = await _repository.GetApprovalAsync(approvalId);

                if (approval == null)
                {
                    return NotFound();
                }

                var approvalDto = _approvalFactory.CreateApproval(approval);

                var linker = new RouteLinker(Request);
                approvalDto.Href =
                    (await linker.GetUriAsync<ApprovalsController, IHttpActionResult>(a => a.GetAsync(approval.Id)))
                        .ToString();

                return Ok(approvalDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        private async Task SetLinksForJobcodeChangeRequestAsync(Dtos.JobcodeChangeRequest jobcodeChangeRequestDto, RouteLinker linker)
        {
            jobcodeChangeRequestDto.Href =
            (await
                linker.GetUriAsync<JobcodeChangeRequestsController, IHttpActionResult>(
                    a => a.GetAsync(jobcodeChangeRequestDto.Id))).ToString();

            foreach (var approvalDto in jobcodeChangeRequestDto.Links.Approvals)
            {
                approvalDto.Href =
                    linker.GetUriAsync<JobcodeChangeRequestsController, IHttpActionResult>(
                        r => r.GetApprovalByRequestAsync(jobcodeChangeRequestDto.Id, approvalDto.Id)).Result.ToString();
            }
        }

        private async Task SetLinksForJobcodeChangeRequestsAsync(IEnumerable<Dtos.JobcodeChangeRequest> jobcodeChangeRequestsDto, RouteLinker linker)
        {
            foreach (var jobcodeChangeRequestDto in jobcodeChangeRequestsDto)
            {
                await SetLinksForJobcodeChangeRequestAsync(jobcodeChangeRequestDto, linker);
            }

        }
    }
}