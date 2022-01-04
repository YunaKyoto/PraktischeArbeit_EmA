using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Input;
using Xamarin.Forms;

using PraktischeArbeit_EmA.Services;
using PraktischeArbeit_EmA.Models;
using PraktischeArbeit_EmA.Views;

namespace PraktischeArbeit_EmA.ViewModels
{
    public class DetailViewModel : ViewModel
    {
        public DetailViewModel(OpenUserService userService)
        {
            this.userService = userService;
            Item = new ForecastItem();  
        }

        public ICommand SaveItem => new Command(async () =>
        {
            //var items = await userService.GetItem();

            if (Item.Name != null) {
                await userService.AddOrUpdateItem(Item);
            }
            var MainView = Resolver.Resolve<MainView>();
            await Navigation.PushAsync(MainView);
            //await Navigation.PopAsync();
        });

        public ICommand Delete => new Command(async () =>
        {
            if (this.Item != null)
            {
                await this.userService.DeleteItem(this.Item);
            }
            var MainView = Resolver.Resolve<MainView>();
            await Navigation.PushAsync(MainView);
        });

        private Boolean visible = false;
        public Boolean Visible 
        { 
            get => visible;
            set => Set(ref visible, value);
        }
        public ForecastItem Item { get; set; }
        private OpenUserService userService;
    }
}
