using System;
using OpenQA.Selenium;
using NUnit.Framework;

namespace SeleniumTestProject.Pages
{
    [TestFixture]
    public class SeleniumTests : BaseClass
    {
        public IWebDriver driver;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            driver = StartBrowser();
            ParkingCalc parkingcalc = new ParkingCalc(driver);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void VerifyInitialElementsPresentOnLoad()
        {
            ParkingCalc parkingcalc = new ParkingCalc(driver);
            Assert.True(parkingcalc.VerifyElements(driver,
                "lot",
                "entrytime",
                "entrydate",
                "exittime",
                "exitdate",
                "entryAM",
                "entryPM",
                "exitAM",
                "exitPM",
                "calculate")
                );
        }
        [Test]
        public void OneHourShortTerm()
        {
            ParkingCalc parkingcalc = new ParkingCalc(driver);
            Assert.AreEqual(parkingcalc.FillOutParkingForm(
                driver,
                "STP",
                "6:00",
                "PM",
                "04/01/2015",
                "7:00",
                "PM",
                "04/01/2015"
                ),2.00);
        }
    }
}
