using Realms;
using System;

namespace iDigIt.Models
{
    public class Plant : RealmObject
    {
        [PrimaryKey]
        public string PlantId { get; set; }
        public string Name { get; set; }
        public string Variety { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
    }
}