using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Repository.Extensions.Utility;

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

        public static IQueryable<Car> Sort(this IQueryable<Car> cars,string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return cars.OrderBy(e => e.CarName);
             var orderQuery =OrderQueryBuilder.CreateOrderQuery<Car>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return cars.OrderBy(e => e.CarName);
            return cars.OrderBy(orderQuery);
        }

    }
}
