using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace PraktischeArbeit_EmA.Models
{
    public class ForecastItem
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public Uri Picture { get; set; }
    }
}
