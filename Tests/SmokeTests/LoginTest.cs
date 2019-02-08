using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using Tests.Base;
using Components.Components;
using static System.Drawing.Imaging.ImageFormat;
using System.Web;
using Utilities;
using Components.TestDataAccess;
using System.Reflection;
using System.Drawing;
using Tesseract;
using Leptonica;

namespace Tests.SmokeTests
{
    [TestFixture]
    public class LoginTest : WebDriverFactory
    {
        public static ExtentTest test;
        ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(@"C:\Work\AutomationTesting.Betsold\Tests\BetsoldExtentReport.html");
        public static ExtentReports extent = new ExtentReports();

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

        [SetUp]
        public void Setup()
        {
            Driver = Create(BrowserType.Chrome);
            Driver.Navigate().GoToUrl("http://betsold-frontend-v3-corbetts-test-eu-west-1.s3-website-eu-west-1.amazonaws.com/corbett/");
            Driver.Manage().Window.Maximize();
            Driver.SwitchTo().Frame("betsold-iframe");
        }


        public void /*Image*/ GetTextFromImage(IWebElement element, string uniqueName)
        {
            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            string pth = Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Screenshots/" + uniqueName + ".jpeg";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Jpeg);

            Image img = Image.FromFile(localpath/*uniqueName*/);
            Rectangle rect = new Rectangle();

            if (element != null)
            {
                // Get the Width and Height of the WebElement using
                int width = element.Size.Width;
                int height = element.Size.Height;

                // Get the Location of WebElement in a Point.
                // This will provide X & Y co-ordinates of the WebElement
                Point p = element.Location;

                // Create a rectangle using Width, Height and element location
                rect = new Rectangle(p.X, p.Y, width, height);
            }

            //croping the image based on rect.
            Bitmap bmpImage = new Bitmap(img);
            var cropedImag = bmpImage.Clone(rect, bmpImage.PixelFormat);
          

            string dataPath = @"C:\Betsold\AutomationTesting\Tests\testdata\";
            string language = "eng";
            string imgPath = @"C:\Betsold\AutomationTesting\Tests\Screenshots\logo-test.jpeg";

            OcrEngineMode oem = OcrEngineMode.LSTM_ONLY;
            PageSegmentationMode psm = PageSegmentationMode.AUTO;

            TessBaseAPI tessBaseAPI = new TessBaseAPI(dataPath, language, oem, psm);

            // Set the input image
            tessBaseAPI.SetImage(imgPath);

            var processedImage = tessBaseAPI.GetThresholdedImage();
            processedImage.Write(@"C:\Users\ibozhinovski\Desktop\", ImageFileFormatTypes.IFF_JFIF_JPEG);


            // Recognize image
            tessBaseAPI.Recognize();

            ResultIterator resultIterator = tessBaseAPI.GetIterator();

            // Extract text from result iterator
            StringBuilder text = new StringBuilder();
            PageIteratorLevel pageIteratorLevel = PageIteratorLevel.RIL_PARA;
            do
            {
                text.Append(resultIterator.GetUTF8Text(pageIteratorLevel));
            } while (resultIterator.Next(pageIteratorLevel));

            tessBaseAPI.Dispose();

            Console.Read();

            /*
            // croping the image based on rect.
            Bitmap bmpImage = new Bitmap(img);
            var cropedImag = bmpImage.Clone(rect, bmpImage.PixelFormat);           

            var ocr = new TesseractEngine("./testdata", "eng");

            var page = ocr.Process(cropedImag);

            var result = page.GetText();

            Console.WriteLine(result);
            */
        }


        [Test]
        public void getText()

        {

            var image = Driver.FindElement(By.ClassName("sideBar-logo"));

            
            GetTextFromImage(image,"logo-test");
        }

        [Test]
        public void UserLoginTest()
        {
            Login.LoginComponent("User2");
        }

    }
}
