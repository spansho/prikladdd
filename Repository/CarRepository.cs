using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Extensions;


namespace Repository
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<PagedList<Car>> GetAllCarAsync(Guid engineId, CarParameters carParameters, bool trackChanges)
        {
            var cars = await FindByCondition(e => e.EngineId.Equals(engineId),trackChanges)
            .FilterCars(carParameters.MinDollarCost,
            carParameters.MaxDollarCost).Search(carParameters.SearchTerm)
            .OrderBy(e => e.CarName).ToListAsync();
            return PagedList<Car>
            .ToPagedList(cars, carParameters.PageNumber,
            carParameters.PageSize);
        }
          
       
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
