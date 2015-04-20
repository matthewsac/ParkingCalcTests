using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestProject
{
    public static class Common
    {
        private static TimeSpan _defaultTimeSpan = new TimeSpan(0, 0, 30);
        public static string WebBrowser { get; set; }
        public static TimeSpan DriverTimeout
        {
            get {return _defaultTimeSpan;}
            set {_defaultTimeSpan = value;}
        }
    }
}
