using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestProject
{
    public class BaseClass
    {
        private FirefoxProfile _ffp;
        private IWebDriver _mydriver;

        public IWebDriver StartBrowser()
        {
            Common.WebBrowser = "firefox";
            //setting to "firefox directly now, will reconfig this to read from file when I learn how
            switch (Common.WebBrowser)
            {
                case "firefox":
                    _ffp = new FirefoxProfile();
                    _ffp.AcceptUntrustedCertificates = true;
                    _mydriver = new FirefoxDriver(_ffp);
                    break;
                case "iexplore":
                    _mydriver = new InternetExplorerDriver();
                    break;
                case "chrome":
                    _mydriver = new ChromeDriver();
                    break;
            }

            return _mydriver;
        }
        
     static void Main(string[] args)
        {

        }
    }
}
