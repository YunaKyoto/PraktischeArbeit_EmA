using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

using SQLite;
using QuickType;
using Newtonsoft.Json;


using PraktischeArbeit_EmA.Models;
using Xamarin.Forms;

namespace PraktischeArbeit_EmA.Services
{

    public class OpenUserService : IUserService
    {
        public event EventHandler<ForecastItem> OnItemAdded;
        public event EventHandler<ForecastItem> OnItemUpdated;

        public async Task<Forecast> GetForecast()
        {
            var url = $"https://randomuser.me/api/1.3/?" +
                      $"inc=gender,name,dob,phone,picture&nat=de,ch&results=1";


            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<UserData>(result);

            
            var forecast = new Forecast()
            {
                //Info = data.Info.Seed,
                Items = data.Results.Select(x => new ForecastItem()
                {
                    //QRCode = GenerateQR("H"),
                    Name = x.Name.First + " " + x.Name.Last,
                    Gender = x.Gender,
                    Birthday = x.Dob.Date.ToString("d") + " (" + x.Dob.Age + ")",
                    Phone = x.Phone,
                    Picture = x.Picture.Thumbnail
                }).ToList()

            };

            return forecast;
        }
        
        public async Task<List<ForecastItem>> GetItem()
        {
            await CreateConnection();
            return await connector.Table<ForecastItem>().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task DeleteItem(ForecastItem inItem)
        {
            await CreateConnection();
            var x = await connector.FindAsync<ForecastItem>(inItem.Id);
            if (x != null)
            {
                await connector.DeleteAsync(inItem);
            }
        }
        public async Task DeleteAllItems()
        {
            await CreateConnection();
            
            await connector.DeleteAllAsync<ForecastItem>();
        }
        public async Task AddItem(ForecastItem item)
        {
            await CreateConnection();
            await connector.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }
        public async Task UpdateItem(ForecastItem item)
        {
            await CreateConnection();
            await connector.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }
        public async Task AddOrUpdateItem(ForecastItem item)
        {
            var it = await this.GetItem();
            var userGet = it.Find(x => x.Id == item.Id);

            if (userGet == null)
            {
                if (it.Count < 50) {
                    await AddItem(item);
                }
            }
            else
            {
                await UpdateItem(item);
            }
        }

        private async Task CreateConnection()
        {
            if (this.connector != null)
            {
                return;
            }
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dbPath = Path.Combine(docPath, "UserData.db");
            connector = new SQLiteAsyncConnection(dbPath);
            await connector.CreateTableAsync<ForecastItem>();      

            if (await connector.Table<ForecastItem>().CountAsync() == 0)
            {
                
                /*for (int i = 0; i < 5; i++)
                {
                    var forecast = await GetForecast();
                    var it = await GetItem();
                    foreach (var item in forecast.Items)
                    {
                        if (!it.Contains(item))
                        {
                            await connector.InsertAsync(item);
                        }
                    }
                    
                }*/
            }
        }

        private SQLiteAsyncConnection connector;

    }
}
