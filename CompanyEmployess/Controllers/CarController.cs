using AutoMapper;
using CompanyEmployess.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var engine = _repository.Engine.GetEngine(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in the database.");
                return NotFound();
                

            }

            var engines = _repository.Car.GetCars(engineId, trackChanges: false);
            var enginesDto = _mapper.Map<IEnumerable<CarDto>>(engines);
            return Ok(enginesDto);
        }


        [HttpGet("{id}", Name = "GetCarForEngine")]

        public IActionResult GetCarForEngine(Guid engineId, Guid id)
        {
            var engine = _repository.Engine.GetEngine(engineId, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {engineId} doesn't exist in thedatabase.");
                return NotFound();
            }
            var carFromDb = _repository.Car.GetCar(engineId, id, trackChanges: false);
            if (carFromDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var car = _mapper.Map<CarDto>(carFromDb);
            return Ok(car);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid engineId, [FromBody]CarForCreationDto car)
        {
            if (car == null)
            {
                _logger.LogError("EmployeeForCreationDto object sent from client isnull.");
            return BadRequest("EmployeeForCreationDto object is null");
            }
            var engine = _repository.Engine.GetEngine(engineId, trackChanges: false);
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
    }
}
