using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Essentials;
using SQLite;

using PraktischeArbeit_EmA.Models;
using PraktischeArbeit_EmA.Views;
using PraktischeArbeit_EmA.Services;

namespace PraktischeArbeit_EmA.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel(OpenUserService userService)
        {
            this.userService = userService;

            userService.OnItemAdded += (sender, item) => AddMembers.Add(CreateToDoItemViewModel(item));
            userService.OnItemUpdated += (sender, item) => Task.Run(async () => await this.LoadData());

            Task.Run(async () => await this.LoadData());
        }

        public async Task LoadData()
        {
            var it = await this.userService.GetItem();
            var itemGroup = it.Select(i => CreateToDoItemViewModel(i));

            //var forecast = await this.userService.GetForecast();
            //var itemGroup = new List<ForecastGroup>();
            /*foreach (var item in forecast.Items)
                {
                    if (!itemGroup.Any())
                    {
                    /*itemGroup.Add(
                        new ForecastGroup(new List<ForecastItem>() { item })
                        { Info = item.Info });*/
            /*await this.userService.AddItem(item);
            continue;
            }
            var group = itemGroup.SingleOrDefault(x => x.Info == item.Info);
            if (group == null)
            {
            /*itemGroup.Add(
                new ForecastGroup(new List<ForecastItem>() { item })
                { Info = item.Info });*/
            /*await this.userService.AddItem(item);
            continue;
            }
            //await this.userService.AddItem(item);
            group.Items.Add(item);
            }*/

            AddMembers = new ObservableCollection<ForecastGroup>(itemGroup);
            Members = new ObservableCollection<ForecastItem>(it);
        }

        private ForecastGroup CreateToDoItemViewModel(ForecastItem item)
        {
            var itemViewModel = new ForecastGroup(new List<ForecastItem>() { item });
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
        }

        public ICommand Settings => new Command(async () =>
        {
            var SettingsView = Resolver.Resolve<SettingsView>();
            await Navigation.PushAsync(SettingsView);
        });
        public ICommand Camera => new Command(async () =>
        {
            var CameraView = Resolver.Resolve<CameraView>();
            await Navigation.PushAsync(CameraView);
        });
        public ICommand AddUser => new Command(async () =>
        {
            var DetailView = Resolver.Resolve<DetailView>();
            await Navigation.PushAsync(DetailView);
        });

        public ICommand OpenMember => new Command(async () =>
        {
            if (this.selectedMember != null)
            {
                await NavigationToItem(SelectedMember);
            }
            await Task.Run(async () => await this.LoadData());
        });


        private async Task NavigationToItem(ForecastItem itemVM)
        {
            if (itemVM != null)
            {
                var itemView = Resolver.Resolve<DetailView>();
                var itemViemModel = itemView.BindingContext as DetailViewModel;
                itemViemModel.Visible = true;
                if (itemViemModel != null)
                {
                    itemViemModel.Item = itemVM;
                    await Navigation.PushAsync(itemView);
                }
            }
        }
        
        OpenUserService userService;

        private ForecastItem selectedMember;
        public ForecastItem SelectedMember
        {
            get => selectedMember;
            set => Set(ref selectedMember, value);
        }

        private ObservableCollection<ForecastItem> members;
        public ObservableCollection<ForecastItem> Members
        {
            get => members;
            set => Set(ref members, value);
        }

        public ObservableCollection<ForecastGroup> addMembers;
        public ObservableCollection<ForecastGroup> AddMembers
        {
            get => addMembers;
            set => Set(ref addMembers, value);
        }
    }
}
