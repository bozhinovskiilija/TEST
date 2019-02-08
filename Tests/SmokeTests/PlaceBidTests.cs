using System;
using AutomationCore;
using Components.Components;
using NUnit.Framework;
using OpenQA.Selenium;
using Tests.Base;

namespace Tests.SmokeTests
{
    [TestFixture]
    public class PlaceBidTests:BaseTest
    {
        public string url = "https://d2qmb2rf14b8v4.cloudfront.net/";

        public static string Capture(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "ErrorScreenshots\\" + screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Jpeg);
            return localpath;
        }

        [Test]
        public void Check_Bid_Functionality()
        {         
            Assert.IsTrue(PlaceBid.MakeABid(),"bidding was not successful (or there are no lots available for bidding)"); 

            ITakesScreenshot screenshotDriver = Driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            string timestamp = DateTime.Now.ToString("yyyyMMdd-hhmmss");//yyyy-MM-dd-hhmm
            screenshot.SaveAsFile(@"C:\Work\AutomationTesting.Betsold\TestResults\PlaceBid\Exception-" + timestamp + ".jpg", ScreenshotImageFormat.Jpeg);
        }
    }
}
