using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CompanyEmployess.ActionFilters
{
    public class ValidateCarForEngineExistsAttribute: IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateCarForEngineExistsAttribute(IRepositoryManager repository,
        ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ?
           true : false;
            var engineId = (Guid)context.ActionArguments["engineId"];
            var engine = await _repository.Engine.GetEngineAsync(engineId, false);
            if (engine == null)
            {
                _logger.LogInfo($"Engine with id: {engineId} doesn't exist in the database.");
            return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var car = await _repository.Car.GetCarAsync(engineId, id,trackChanges);
            if (car == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in thedatabase.");
               
                context.Result = new NotFoundResult();
            }
           else
            {
                context.HttpContext.Items.Add("employee", car);
                await next();
            }
        }
    }
}
