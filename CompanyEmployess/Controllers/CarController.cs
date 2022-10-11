using AutoMapper;
using CompanyEmployess.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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

        public CarController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCarsForEngine(Guid engineId)
        {
            var engine = _repository.Engine.GetEngineAsync(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in the database.");
                return NotFound();
               
            }

            

            var engines = _repository.Car.GetAllCarAsync(engineId, trackChanges: false);
            var enginesDto = _mapper.Map<IEnumerable<CarDto>>(engines);
            return Ok(enginesDto);
        }


        [HttpGet("{id}", Name = "GetCarForEngine")]

        public IActionResult GetCarForEngine(Guid engineId, Guid id)
        {
            var engine = _repository.Engine.GetEngineAsync(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in thedatabase.");
                return NotFound();
            }
            
            var carFromDb = _repository.Car.GetCarAsync(engineId, id, trackChanges: false);
            if (carFromDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var car = _mapper.Map<CarDto>(carFromDb);
            return Ok(car);
        }

        [HttpPost]
        public IActionResult CreateCarForEngine(Guid engineId, [FromBody]CarForCreationDto car)
        {
            if (car == null)
            {
                _logger.LogError("EmployeeForCreationDto object sent from client isnull.");
            return BadRequest("EmployeeForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            var engine = _repository.Engine.GetEngineAsync(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in thedatabase.");
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
        public async Task<IActionResult> DeleteCarForEngine(Guid engineId, Guid id)
        {
            var car = await _repository.Car.GetAllCarAsync(engineId, trackChanges: false);
            if (car == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in thedatabase.");
            return NotFound();
            }
           
            var carForEngine = await _repository.Car.GetCarAsync(engineId, id, trackChanges: false);
            if (carForEngine == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in thedatabase.");
            return NotFound();
            }

            _repository.Car.DeleteCar(carForEngine);
            _repository.Save();
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCarForEngine(Guid engineId, Guid id, [FromBody]CarForUpdateDto car)
        {
            if (car == null)
            {
                _logger.LogError("EmployeeForUpdateDto object sent from client is null.");
            return BadRequest("EmployeeForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            var engine = _repository.Engine.GetEngineAsync(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in thedatabase.");
            return NotFound();
            }
            var carEntity = _repository.Car.GetCarAsync(engineId, id,trackChanges: true);
            if (carEntity == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in thedatabase.");
            return NotFound();
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(car, carEntity);
            _repository.Save();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateCarForEngine(Guid engineId, Guid id,
 [FromBody] JsonPatchDocument<CarForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            
           
            var engine = _repository.Engine.GetEngineAsync(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in thedatabase.");
            return NotFound();
            }
            var carEntity = _repository.Car.GetCarAsync(engineId, id,trackChanges:true);
            if (carEntity == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            var carToPatch = _mapper.Map<CarForUpdateDto>(carEntity);
            patchDoc.ApplyTo(carToPatch);
            TryValidateModel(carToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(carToPatch, carEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
