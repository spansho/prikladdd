﻿using AutoMapper;
using CompanyEmployess.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [Route("api/engines")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EngineController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEngines()
        {
           
                var engines = _repository.Engine.GetAllEnginesAsync(trackChanges: false);
                var enginesDto = _mapper.Map<IEnumerable<EngineDto>>(engines);
                return Ok(enginesDto);       
        }


        [HttpGet("{id}", Name = "EngineById")]
        public IActionResult GetEngine(Guid id)
        {
            var engine = _repository.Engine.GetEngineAsync(id, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Engine with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var engineDto = _mapper.Map<EngineDto>(engine);
                return Ok(engineDto);
            }
        }

        [HttpPost]
        public IActionResult CreateEngine([FromBody] EngineForCreationDto engine)
        {
            if (engine == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client isnull.");
                return BadRequest("CompanyForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            var engineEntity = _mapper.Map<Engine>(engine);
            _repository.Engine.CreateEngine(engineEntity);
            _repository.Save();
            var EngineToReturn = _mapper.Map<EngineDto>(engineEntity);
            return CreatedAtRoute("EngineById", new { id = EngineToReturn.Id }, EngineToReturn);
        }

        [HttpGet("collection/({ids})", Name = "EngineCollection")]
        public async Task<IActionResult> GetEngineCollectionAsync([ModelBinder(BinderType =typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            
            var engineEntities =await _repository.Engine.GetByIdsAsync(ids, trackChanges: false);
            if (ids.Count() != engineEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var EngineToReturn =
           _mapper.Map<IEnumerable<EngineDto>>(engineEntities);
            return Ok(EngineToReturn);
        }


        [HttpPost("collection")]
        public IActionResult CreateEngineCollection([FromBody]IEnumerable<EngineForCreationDto> engineCollection)
        {
            if (engineCollection == null)
            {
                _logger.LogError("Company collection sent from client is null.");
                return BadRequest("Company collection is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDtoobject");
                return UnprocessableEntity(ModelState);
            }
            var enginEntities = _mapper.Map<IEnumerable<Engine>>(engineCollection);
            foreach (var engine in enginEntities)
            {
                _repository.Engine.CreateEngine(engine);
            }
            _repository.Save();
            var engineCollectionToReturn =
            _mapper.Map<IEnumerable<EngineDto>>(enginEntities);
            var ids = string.Join(",", engineCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("EngineCollection", new { ids },
            engineCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngine(Guid id)
        {
            var engine = await _repository.Engine.GetEngineAsync(id, trackChanges: false);
            if (engine == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Engine.DeleteEngine(engine);
            _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEngine(Guid id, [FromBody] EngineForUpdateDto engine)
        {
            if (engine == null)
            {
                
            _logger.LogError("CompanyForUpdateDto object sent from client is null.");
                return BadRequest("CompanyForUpdateDto object is null");
            }
            var engineEntity = _repository.Engine.GetEngineAsync(id, trackChanges: true);
            if (engineEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(engine, engineEntity);
            _repository.SaveAsync();
            return NoContent();
        }
    }
}
