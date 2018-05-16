using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iDigIt.Helpers
{
    public class JobTypeCount
    {
        const string CULTIVATE_COLOR = "#c8e9ca";
        const string PREPARATION_COLOR = "#e2d4cf";
        const string GENERAL_COLOR = "#ffe6cc";

        public string Name { get; set; }
        public decimal Count { get; set; }        
        public decimal Total { get; set; }
        public decimal Time { get; set; }
        public decimal TotalTime { get; set; }
        public string ShortName
        {
            get
            {
                return Name.Substring(0, 1);
                //switch (Name)
                //{
                //    case "Cultivate":
                //        return "C";
                //    case "Preparation":
                //        return "P";
                //}

                //return "G";
            }
        }
        public Color JobColour
        {
            get
            {
                switch(Name)
                {
                    case "Cultivate":
                        return Color.FromHex(CULTIVATE_COLOR);
                    case "Preparation":
                        return Color.FromHex(PREPARATION_COLOR);
                }

                return Color.FromHex(GENERAL_COLOR);
            }
        }
        public string Percent {
            get
            {
                return Count == 0 ? "N 0.0%" : String.Format("N {0:0.0}%",Decimal.Round((Count * 100) / Total, 2));
            }
        }
        public string TimePercent
        {
            get
            {
                return Time == 0 ? "T 0.0%" : String.Format("T {0:0.0}%", Decimal.Round((Time * 100) / TotalTime, 2));
            }
        }
   
        public int ColWidth
        {
            get
            {
                return Count == 0 ? 1 : Convert.ToInt32(Count);
            }
        }

        public int ReverseColWidth
        {
            get
            {
                return Count == 0 ? 1 : Convert.ToInt32((Total + 1) - Count);
            }
        }

        public int StartSpan
        {
            get
            {
                return Convert.ToInt32(Count + 1);
            }
        }
    }
}
