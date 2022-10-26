using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [Route("api/{v:apiversion}/engines")]
    [ApiController]
    public class EngineV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public EngineV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetEngines()
        {
            var engines = await _repository.Engine.GetAllEnginesAsync(trackChanges:false);
            return Ok(engines);
        }
    }
}
