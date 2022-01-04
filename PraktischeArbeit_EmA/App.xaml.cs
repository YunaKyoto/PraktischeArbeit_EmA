using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PraktischeArbeit_EmA.Views;
using PraktischeArbeit_EmA.ViewModels;

namespace PraktischeArbeit_EmA
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(Resolver.Resolve<Views.MainView>());
            //new MainView());
            //Resolver.Resolve<Views.MainView>());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
