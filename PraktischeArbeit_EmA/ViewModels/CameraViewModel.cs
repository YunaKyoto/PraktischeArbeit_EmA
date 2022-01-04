using System;
using System.Collections.Generic;
using System.Text;

using PraktischeArbeit_EmA.Services;
using PraktischeArbeit_EmA.Models;

namespace PraktischeArbeit_EmA.ViewModels
{
    public class CameraViewModel : ViewModel
    {
        public CameraViewModel(OpenUserService userService)
        {
            this.userService = userService;
        }

        public OpenUserService userService;

        /*private string scanResultText;
        public string ScanResultText
        {
            get => scanResultText;
            set => Set(ref scanResultText, value);
        }*/
    }
}
