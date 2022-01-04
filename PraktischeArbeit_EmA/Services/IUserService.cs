using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using PraktischeArbeit_EmA.Models;

namespace PraktischeArbeit_EmA.Services
{
    public interface IUserService
    {
        Task<Forecast> GetForecast();
        Task AddItem(ForecastItem item);
        Task<List<ForecastItem>> GetItem();
        Task DeleteAllItems();
        Task DeleteItem(ForecastItem inItem);
        Task UpdateItem(ForecastItem item);
        Task AddOrUpdateItem(ForecastItem item);
    }
}
