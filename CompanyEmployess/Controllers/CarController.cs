using AutoMapper;
using CompanyEmployess.ActionFilters;
using CompanyEmployess.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [Route("api/engines/{engineId}/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<CarDto> _dataShaper;

        public CarController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<CarDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarsForEngine(Guid engineId,
        [FromQuery] CarParameters carParameters)
        {
            if (!carParameters.ValidAgeRange)
                return BadRequest("Max DollarCost can't be less than min age.");
            var engine = await _repository.Engine.GetEngineAsync(engineId,
           trackChanges:
            false);
            if (engine == null)
            {
                _logger.LogInfo($"Engine with id: {engineId} doesn't exist in the database.");
            return NotFound();
            }
            var carsFromDb = await _repository.Car.GetAllCarAsync(engineId,carParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
            JsonConvert.SerializeObject(carsFromDb.MetaData));
            var carDto = _mapper.Map<IEnumerable<CarDto>>(carsFromDb);
            return Ok(_dataShaper.ShapeData(carDto, carParameters.Fields));
        }


        [HttpGet("{id}", Name = "GetCarForEngine")]

        public IActionResult GetCarForEngine(Guid engineId, Guid id)
        {
            var engine = _repository.Engine.GetEngineAsync(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Engine with id: {engineId} doesn't exist in thedatabase.");
                return NotFound();
            }

            var carFromDb = _repository.Car.GetCarAsync(engineId, id, trackChanges: false);
            if (carFromDb == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var car = _mapper.Map<CarDto>(carFromDb);
            return Ok(car);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateCarForEngine(Guid engineId, [FromBody] CarForCreationDto car)
        {
            if (car == null)
            {
                _logger.LogError("CarForCreationDto object sent from client isnull.");
                return BadRequest("CarForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            var engine = _repository.Engine.GetEngineAsync(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Car with id: {engineId} doesn't exist in thedatabase.");
                return NotFound();
            }

            var carEntity = _mapper.Map<Car>(car);
            _repository.Car.CreateCarForEngine(engineId, carEntity);
            _repository.Save();
            var carToReturn = _mapper.Map<CarDto>(carEntity);
            return CreatedAtRoute("GetCarForEngine", new
            {
                engineId,
                id = carToReturn.Id
            }, carToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCarForEngineExistsAttribute))]

        public async Task<IActionResult> DeleteCarForEngine(Guid engineId, Guid id)
        {
            var carForEngine = HttpContext.Items["car"] as Car;
            _repository.Car.DeleteCar(carForEngine);
            await _repository.SaveAsync();
            return NoContent();
        }


        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCarForEngineExistsAttribute))]
        public async Task<IActionResult> UpdateCarForEngineAsync(Guid engineId, Guid id, [FromBody] CarForUpdateDto car)
        {
            var carEntity = HttpContext.Items["car"] as Car;
            _mapper.Map(car, carEntity);
            await _repository.SaveAsync();
            return NoContent();

        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateCarForEngineAsync(Guid engineId, Guid id,
 [FromBody] JsonPatchDocument<CarForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var carEntity = HttpContext.Items["car"] as Car;
            var carToPatch = _mapper.Map<CarForUpdateDto>(carEntity);
            patchDoc.ApplyTo(carToPatch, ModelState);
            TryValidateModel(carToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);

            }
            _mapper.Map(carToPatch, carEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
