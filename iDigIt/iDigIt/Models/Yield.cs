using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace iDigIt.Models
{
    public class Yield : RealmObject
    {
        [PrimaryKey]
        public string YieldId { get; set; }
        public string Plant { get; set; }
        public string Crop { get; set; }
        public int Year { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
    }
}
