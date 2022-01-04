using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Windows.Input;

using PraktischeArbeit_EmA.ViewModels;
using PraktischeArbeit_EmA.Services;
using PraktischeArbeit_EmA.Models;

namespace PraktischeArbeit_EmA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            var settingsViewModel = new SettingsViewModel(new OpenUserService());
            settingsViewModel.Navigation = Navigation;
            BindingContext = settingsViewModel;
        }
    }
}