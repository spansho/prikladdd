using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEngineRepository
    {
        Task<IEnumerable<Engine>> GetAllEngineAsync(bool trackChanges);
        Task<Engine> GetEngineAsync(Guid companyId, bool trackChanges);
        void CreateEngine(Engine engine);

        Task<IEnumerable<Engine>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteEngine(Engine engine);
        object GetAllEngines(bool trackChanges);
    }
}
