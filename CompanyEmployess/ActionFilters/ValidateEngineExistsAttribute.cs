using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CompanyEmployess.ActionFilters
{
    public class ValidateEngineExistsAttribute: IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateEngineExistsAttribute(IRepositoryManager repository,
        ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var engine = await _repository.Engine.GetEngineAsync(id,trackChanges);
            if (engine == null)
            {
                _logger.LogInfo($"Engine with id: {id} doesn't exist in the database.");
               
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("engine", engine);
                await next();
            }
        }
    }
}
