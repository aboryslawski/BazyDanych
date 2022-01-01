using BazyDanych.Data.BazyDanych;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BazyDanych.Data
{
    public class WeatherForecastService
    {
        private readonly BazyDanychDbContext _context;

        public WeatherForecastService(BazyDanychDbContext context)
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

        public Task<WeatherForecast> CreateForecastAsync(WeatherForecast objWeatherForecast)
        {
            _context.WeatherForecast.Add(objWeatherForecast);
            _context.SaveChanges();

            return Task.FromResult(objWeatherForecast);
        }
        public Task<bool> UpdateForecastAsync(WeatherForecast objWeatherForecast)
        {
            var existingWeatherForecast = _context.WeatherForecast
                .FirstOrDefault(x => x.Id == objWeatherForecast.Id);

            if (existingWeatherForecast != null)
            {
                existingWeatherForecast.Date = objWeatherForecast.Date;
                existingWeatherForecast.Summary = objWeatherForecast.Summary;
                existingWeatherForecast.TemperatureC = objWeatherForecast.TemperatureC;
                existingWeatherForecast.TemperatureF = objWeatherForecast.TemperatureF;
                _context.SaveChanges();
            }

            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> DeleteForecastAsync(WeatherForecast objWeatherForecast)
        {
            var existingWeatherForecast = _context.WeatherForecast
                .FirstOrDefault(x => x.Id == objWeatherForecast.Id);

            if (existingWeatherForecast != null)
            {
                _context.WeatherForecast.Remove(existingWeatherForecast);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}