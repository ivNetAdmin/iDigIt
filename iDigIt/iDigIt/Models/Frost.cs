using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace iDigIt.Models
{
    public class Frost : RealmObject
    {
        [PrimaryKey]
        public string FrostId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
    }
}
