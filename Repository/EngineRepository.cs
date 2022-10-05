using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EngineRepository : RepositoryBase<Engine>, IEngineRepository
    {
        public EngineRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Engine> GetAllEngines(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(c => c.EngineName).ToList();

        public Engine GetEngine(Guid engineId, bool trackChanges) => FindByCondition(c => c.Id.Equals(engineId), trackChanges).SingleOrDefault();
    }
}
