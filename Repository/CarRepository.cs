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
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Car>> GetAllCarAsync(Guid engineId, bool trackChanges) => 
            await FindByCondition(e=>e.Id.Equals(engineId),trackChanges).OrderBy(c => c.CarName).ToListAsync();

        public async Task<Car> GetCarAsync(Guid engineId, Guid id, bool trackChanges) => 
            await FindByCondition(e => e.EngineId.Equals(engineId) && 
            e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();


        public void CreateCarForEngine(Guid engineId, Car car)
        {
            car.EngineId = engineId;
            Create(car);
        }


        public void DeleteCar(Car car)
        {
            Delete(car);
        }
    }
}
