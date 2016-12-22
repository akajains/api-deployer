using System;
using System.Threading.Tasks;
using System.Web.Http;
using DataHub.Data;

namespace DataHub.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/positions")]
    public class PositionsController : ApiController
    {
        private readonly IDataHubEfRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public PositionsController(IDataHubEfRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name="GetPositionById")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            try
            {
                var position = await _repository.GetPositionAsync(id);

                if (position == null)
                {
                    return NotFound();
                }

                //var positionDto = _positionFactory.CreatePosition(position);

                //var linker = new RouteLinker(Request);

                //positionDto.Href = linker.GetUri<PositionsController>(jc => jc.Get(positionDto.Id)).ToString();

                return Ok(position);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}