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
    public class SettingsViewModel : ViewModel
    {
        public SettingsViewModel(OpenUserService userService)
        {
            this.userService = userService;

        }

        public ICommand Load => new Command(async () =>
        {
                for (int i = 0; i < 3; i++)
                {
                    var forecast = await this.userService.GetForecast();
                    foreach (var item in forecast.Items)
                    {
                        await this.userService.AddOrUpdateItem(item);

                    }
                }
            var MainView = Resolver.Resolve<MainView>();
            await Navigation.PushAsync(MainView);
            //await Navigation.PopAsync();
        });

        public ICommand DeleteAll => new Command(async () =>
        {
            await this.userService.DeleteAllItems();
            var MainView = Resolver.Resolve<MainView>();
            await Navigation.PushAsync(MainView);
        });

        private OpenUserService userService;
    }
}
