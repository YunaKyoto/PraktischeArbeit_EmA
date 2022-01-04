using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PraktischeArbeit_EmA.ViewModels;
using Xamarin.Essentials;
using PraktischeArbeit_EmA.Services;
using PraktischeArbeit_EmA.Models;

namespace PraktischeArbeit_EmA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            var mainViewModel = new MainViewModel(new OpenUserService());
            mainViewModel.Navigation = Navigation;
            BindingContext = mainViewModel;

            //BindingContext = new MainViewModel(new OpenUserService());
        }

        /*protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MainViewModel viewModel)
            {
                MainThread.BeginInvokeOnMainThread(async () => {
                    await viewModel.LoadData();
                });
            }
        }*/
    }
}