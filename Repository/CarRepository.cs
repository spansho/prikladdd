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
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Car> GetCars(Guid EngineId, bool trackChanges) => FindByCondition(e => e.EngineId.Equals(EngineId), trackChanges).OrderBy(e => e.CarName);

        public  Car GetCar(Guid engineId, Guid id, bool trackChanges) =>
FindByCondition(e => e.EngineId.Equals(engineId) && e.Id.Equals(id),
trackChanges).SingleOrDefault();


        public void CreateCarForEngine(Guid engineId, Car car)
        {
            car.EngineId = engineId;
            Create(car);
        }
       
    }
}
