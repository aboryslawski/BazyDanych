using BazyDanych.Data.BazyDanych;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BazyDanych.Data
{
    public class WeatherForecastService
    {
        private readonly AspnetBazydanychFE03AB7A0D1F48FA9C62B214EBB023F7Context _context;

        public WeatherForecastService(AspnetBazydanychFE03AB7A0D1F48FA9C62B214EBB023F7Context context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> GetForecastAsync(string strCurrentUser)
        {
            // Get Weather Forecasts

            return await _context.WeatherForecast
                // Only get entries for the current logged in user
                .Where(x => x.UserName == strCurrentUser)
                // Use AsNoTracking to disable EF change tracking
                // Use ToListAsync to avoid blocking a thread
                .AsNoTracking().ToListAsync();
        }
    }
}