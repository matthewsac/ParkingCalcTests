using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestProject.Pages
{
    class ParkingCalc
    {
        //assign names to all object xpaths
        //private string _lot = ".//*[@id='Lot']";
        //private string _entrytime = ".//*[@id='EntryTime']";
        //private string _entrydate = ".//*[@id='EntryDate']";
        //private string _exittime = ".//*[@id='ExitTime']";
        //private string _exitdate = ".//*[@id='ExitDate']";
        //private string _entryAM = "//input[@value='AM' and @name='EntryTimeAMPM']";
        //private string _entryPM = "//input[@value='PM' and @name='EntryTimeAMPM']";
        //private string _exitAM = "//input[@value='AM' and @name='ExitTimeAMPM']";
        //private string _exitPM = "//input[@value='PM' and @name='ExitTimeAMPM']";
        //private string _cost = "//span[@class='SubHead']/font/b";
        //private string _bodycopy = "//span[@class='BodyCopy']/font/b"; //text value could be elapsed time, or error message

        private readonly IWebDriver parkingcalcpage;
        public Dictionary<string, string> elementList = new Dictionary<string, string>();

        public ParkingCalc(IWebDriver driver)
        {
            //"E:\\Users\\Curtis\\Documents\\Visual Studio 2013\\Projects\\SeleniumTestProject\\SeleniumTestProject\\Pages\\TestData\\Elements_ParkingCalc.xml"
            
            parkingcalcpage=driver;
            GoTo(parkingcalcpage,"http://adam.goucher.ca/parkcalc/");
            ReadElementsIntoElementList("E:\\Users\\Curtis\\Documents\\Visual Studio 2013\\Projects\\SeleniumTestProject\\SeleniumTestProject\\Pages\\TestData\\Elements_ParkingCalc.xml");
        }
        public void GoTo(IWebDriver driver,String URL)
        {
            driver.Navigate().GoToUrl(URL);
        }
        public void ReadElementsIntoElementList(String xmlfile)
        {
            XmlReader elementListXML = XmlReader.Create(xmlfile);
            while (elementListXML.Read())
            {
                if (elementListXML.Name == "pageElement")
                {
                    elementList[elementListXML.GetAttribute("elementName")] = elementListXML.GetAttribute("xPath");
                }
            }
        }
        public bool VerifyElements(IWebDriver driver, params String[] elements)
        {
            foreach (String element in elements)
            {
                try
                {
                    String elementXPath=elementList[element];
                    var verify=driver.FindElement(By.XPath(elementXPath));
                    Console.Write("Found " + element + " using XPath "+verify.Text);
                }
                catch (Exception)
                {
                    Console.Write("Error: could not find "+element);
                    return false;
                    throw;
                } 
            }
            return true;
        }
        public double FillOutParkingForm(
            IWebDriver driver,
            String selectedlot,
            String entrytime,
            String entryAMPM,
            String entrydate,
            String exittime, 
            String exitAMPM,
            String exitdate
            )
        {
            
            return 2.00;

        }
    }
}
