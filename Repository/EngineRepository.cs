using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Engine>> GetAllCompaniesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.EngineName).ToListAsync();

        public async Task<Engine> GetCompanyAsync(Guid engineId, bool trackChanges) =>await FindByCondition(c => c.Id.Equals(engineId), trackChanges).SingleOrDefaultAsync();

        public void CreateEngine(Engine engine) => Create(engine);

        public async Task<IEnumerable<Engine>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteEngine(Engine engine)
        {
            Delete(engine);
        }

    }
}
