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
        IEnumerable<Engine> GetAllEngines(bool trackChanges);
        Engine GetEngine(Guid engineyId, bool trackChanges);
        void CreateEngine(Engine engine);

        IEnumerable<Engine> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
