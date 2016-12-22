using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataHub.Api.Factories;
using DataHub.Data;
using Ploeh.Hyprlinkr;

namespace DataHub.Api.Controllers
{
    /// <summary>
    /// Defines properties and methods for job codes API controller.
    /// </summary>
    [RoutePrefix("api/job-codes")]
    public class JobCodesController : ApiController
    {
        private readonly IDataHubEfRepository _repository;
        private readonly JobCodeFactory _jobCodeFactory = new JobCodeFactory();

        /// <summary>
        /// Initialises a new instance of the <see cref="JobCodesController"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public JobCodesController(IDataHubEfRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name="GetById")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            try
            {
                var jobCode = await _repository.GetJobCodeAsync(id);

                if (jobCode == null)
                {
                    return NotFound();
                }

                var jobCodeDto = _jobCodeFactory.CreateJobCode(jobCode);

                var linker = new RouteLinker(Request);

                jobCodeDto.Href = (await linker.GetUriAsync<JobCodesController, IHttpActionResult>(jc => jc.GetAsync(jobCodeDto.Id))).ToString();

                return Ok(jobCodeDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="band"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> GetAsync(string band = null, string title = null)
        {
            try
            {
                var jobCodes = await _repository.GetJobCodesAsync(band, title);

                if (jobCodes == null)
                {
                    return NotFound();
                }

                var jobCodesDto = jobCodes.Select(jc => _jobCodeFactory.CreateJobCode(jc)).ToList();

                var linker = new RouteLinker(Request);

                foreach (var jobCodeDto in jobCodesDto)
                {
                    jobCodeDto.Href = (await linker.GetUriAsync<JobCodesController, IHttpActionResult>(jc => jc.GetAsync(jobCodeDto.Id))).ToString();
                }

                return Ok(jobCodesDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}