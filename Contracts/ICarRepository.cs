using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICarRepository
    {
        Task<PagedList<Car>> GetAllCarAsync(Guid engineId, CarParameters carParameters, bool trackChanges);

        Task<Car> GetCarAsync(Guid engineId,Guid id, bool trackChanges);

        void CreateCarForEngine(Guid engineId, Car car);

        void DeleteCar(Car car);
    }
}
