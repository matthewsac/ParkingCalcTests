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
                    Console.Write("VerifyElements: Found " + element + " using XPath "+elementXPath);
                }
                catch (Exception)
                {
                    Console.Write("VerifyElements Error: could not find "+element);
                    return false;
                    throw;
                } 
            }
            return true;
        }
        public IWebElement CreateSelObject(IWebDriver driver, String varname)
        {
            String xpath = elementList[varname];
            IWebElement el = driver.FindElement(By.XPath(xpath));
            return el;
        }
        public void LotSelect(IWebDriver driver, String lotchoice)
        {
            SelectElement lotdropdown = new SelectElement(driver.FindElement(By.XPath(elementList["lot"])));
            try 
	        {	        
		    lotdropdown.SelectByValue(lotchoice);
	        }
	        catch (OpenQA.Selenium.NoSuchElementException)
	        {
		        Console.Write("LotSelect Error: LotSelect tried to find {0} using {1} but could not find the element",lotchoice,elementList["lot"]);
		        throw;
	        }
        }      
        public double FillOutParkingForm(
            IWebDriver driver,
            String selectedlot,
            String en_time,
            String en_AMPM,
            String en_date,
            String ex_time, 
            String ex_AMPM,
            String ex_date
            )
        {
            IWebElement entrytime = CreateSelObject(driver, "entrytime");
            IWebElement entrydate = CreateSelObject(driver, "entrydate");
            IWebElement entryAM = CreateSelObject(driver, "entryAM");
            IWebElement entryPM = CreateSelObject(driver, "entryPM");
            IWebElement exittime = CreateSelObject(driver, "exittime");
            IWebElement exitdate = CreateSelObject(driver, "exitdate");
            IWebElement exitAM = CreateSelObject(driver, "exitAM");
            IWebElement exitPM = CreateSelObject(driver, "exitPM");
            IWebElement submitbutton = CreateSelObject(driver, "calculate");
            double result;

            LotSelect(driver,selectedlot); //Select lot from dropdown by value passed in selectedlot
            entrytime.Clear();
            entrytime.SendKeys(en_time); //Enter the entry time
            entrydate.Clear();
            entrydate.SendKeys(en_date); //Enter the entry date
            if (en_AMPM=="AM")
            {
                entryAM.Click();
            }
            else if (en_AMPM=="PM")
            {
                entryPM.Click();
            }
            else{
                Console.WriteLine("FillOutParkingForm Warning: FillOutParkingForm was passed {0} in en_AMPM. Valid values are 'AM' or 'PM'. Default used.",en_AMPM);
            }
            exittime.Clear();
            exittime.SendKeys(ex_time); //Enter the exit time
            exitdate.Clear();
            exitdate.SendKeys(ex_date); //Enter the exit date
            if (ex_AMPM=="AM")
            {
                exitAM.Click();
            }
            else if (ex_AMPM=="PM")
            {
                exitPM.Click();
            }
            else{
                Console.WriteLine("FillOutParkingForm Warning: FillOutParkingForm was passed {0} in ex_AMPM. Valid values are 'AM' or 'PM'. Default used.",ex_AMPM);
            }
            submitbutton.Click();
            //ParkingCalc will do either of two things:
            //   1. Return a cost and an elapsed time parked (if all data is valid)
            //   2. Return an error (like when the exit date is before the entry date)
            //We will determine success by looking for the 1st character of "cost"
            //If it is a $, then the calculation was a success.
            //If not, 'cost' will be an error message, so we will return -1 which should always result in a test fail
            String cost = CreateSelObject(driver, "cost").Text;
            if (cost.StartsWith("$"))
            {
                Console.WriteLine("FillOutParkingForm: Calcuation was successful, returned: {0}", cost);
                result = Convert.ToDouble(cost.Remove(0, 1));
            }
            else
            {
                Console.WriteLine("FillOutParkingForm: Calcuation returned: {0}", cost);
                result = -1.00;
            }
            return result;
        }
    }
}
