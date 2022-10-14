using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryCarExtensions
    {
        public static IQueryable<Car> FilterCars(this IQueryable<Car>cars, uint minDollarCost, uint maxDollarCost) =>
 cars.Where(e => (e.DollarCost >= minDollarCost && e.DollarCost <= maxDollarCost));
        public static IQueryable<Car> Search(this IQueryable<Car>
       cars,
        string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return cars;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return cars.Where(e => e.CarName.ToLower().Contains(lowerCaseTerm));
        }

    }
}
