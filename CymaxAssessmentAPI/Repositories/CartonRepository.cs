using CymaxAssessmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CymaxAssessmentAPI.Repositories
{
    public class CartonRepository : ICartonRepository
    {
        private CartonContext _context;

        public CartonRepository(CartonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get fake carton data
        /// </summary>
        /// <returns></returns>
        public async Task<List<Carton>> GetCartons()
        {
            var cartons = await _context.Cartons.ToListAsync();
            
            // Added fake data to simulate data returned from DB
            cartons.Add(new Carton { Id = 1, Dimension = 1, Price = GetRandomPrice() });
            cartons.Add(new Carton { Id = 2, Dimension = 2, Price = GetRandomPrice() });
            cartons.Add(new Carton { Id = 3, Dimension = 3, Price = GetRandomPrice() });

            return cartons;
        }

        /// <summary>
        /// Just simulates random prices
        /// </summary>
        /// <returns></returns>
        private double GetRandomPrice()
        {
            Random random = new Random();
            return Math.Round(random.NextDouble() * (100 - 1) + 1, 2);
        }
    }
}
