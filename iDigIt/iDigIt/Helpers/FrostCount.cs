using System;
using System.Collections.Generic;
using System.Text;

namespace iDigIt.Helpers
{
    public class FrostCount
    {
        public string Month { get; set; }
        public int Count { get; set; }
        public int MaxFrostCount { get; set; }
        public int ColWidth
        {
            get
            {
                return (MaxFrostCount + 1) - Count;
            }
        }

        public int StartSpan
        {
            get
            {
                return Count + 1;
            }
        }
        
    }
}