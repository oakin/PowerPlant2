using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PowerPlant.Domain
{
    public class AppEnums
    {
               
       
        public enum LogTypes
        {
            [Description("Success")]
            Success = 1,
            [Description("UnSuccess")]
            UnSuccess = 2,
            [Description("Error")]
            Error = 3,
            [Description("History")]
            History = 4,
            [Description("All")]
            All = 5
        }
    }
}
