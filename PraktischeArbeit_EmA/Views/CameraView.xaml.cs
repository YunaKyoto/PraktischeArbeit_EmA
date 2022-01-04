using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PraktischeArbeit_EmA.ViewModels;
using PraktischeArbeit_EmA.Services;

namespace PraktischeArbeit_EmA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {
        public CameraView()
        {
            InitializeComponent();
            cameraViewModel = new CameraViewModel(new OpenUserService());
            cameraViewModel.Navigation = Navigation;
            BindingContext = cameraViewModel;

            //BindingContext = new MainViewModel(new OpenUserService());
        }

        CameraViewModel cameraViewModel;
        public void resultScan(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async  () =>
            {
                var user = await cameraViewModel.userService.GetItem();
                var userGet = user.Find(x => x.Id == Int32.Parse(result.Text));

                if (userGet != null)
                {
                    var itemView = Resolver.Resolve<DetailView>();
                    var itemViemModel = itemView.BindingContext as DetailViewModel;
                    itemViemModel.Visible = true;
                    if (itemViemModel != null)
                    {
                        itemViemModel.Item = userGet;
                        await Navigation.PushAsync(itemView);
                    }
                }

            });
        }
    }
}