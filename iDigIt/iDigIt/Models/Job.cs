using Realms;
using System;
using Xamarin.Forms;

namespace iDigIt.Models
{
    public class Job : RealmObject
    {
        [PrimaryKey]
        public string JobId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Plant { get; set; }
        public int Time { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
        public DateTimeOffset Date { get; set; }

        [Ignored]
        public Color TextColor { get; set; }
    }
}