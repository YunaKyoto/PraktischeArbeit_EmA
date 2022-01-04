using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace PraktischeArbeit_EmA.Models
{
    public class ForecastGroup : List<ForecastItem>
    {
        public event EventHandler ItemStatusChanged;
        public ForecastGroup() { }
        public ForecastGroup(IEnumerable<ForecastItem> items)
        {
            AddRange(items);

        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public string Info { get; set; }
        public List<ForecastItem> Items => this;
    }
}
