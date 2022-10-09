using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICarRepository
    {
        public IEnumerable<Car> GetCars(Guid companyId, bool trackChanges);

        public Car GetCar(Guid companyId, Guid id, bool trackChanges);

        void CreateCarForEngine(Guid engineId, Car car);

        void DeleteCar(Car car);
    }
}
