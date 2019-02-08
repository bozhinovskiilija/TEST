using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace AutomationCore
{
    public class WebDriverFactory
    {
        public static IWebDriver Driver { get; set; }

        public static IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:

                    return GetChromeDriver();
                    //break;

                //case BrowserType.IE:
                //    InternetExplorerOptions ieOptions = new InternetExplorerOptions
                //    {
                //        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                //        IgnoreZoomLevel = true,
                //        EnableNativeEvents = false
                //    };
                //    Driver = new InternetExplorerDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ieOptions);
                //    break;

                case BrowserType.Firefox:

                    return GetFirefoxDriver();

                default:
                    throw new ArgumentOutOfRangeException("no such browser exists");
            }
        }

        public static void ClearBrowserCache()
        {
            Driver.Manage().Cookies.DeleteAllCookies(); //delete all cookies
            Thread.Sleep(5000); //wait 5 seconds to clear cookies.
        }

        public static IWebDriver GetChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");
            //options.AddArguments("--headless");
            //options.AddArguments("--disable-gpu");
            //options.AddArguments("--window-size=1920,1080");
            //options.AddArguments("--no-sandbox");
            //options.AddArguments("--allow-insecure-localhost");


            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(outPutDirectory,options);
        }

        public static IWebDriver GetFirefoxDriver()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new FirefoxDriver(outPutDirectory);
        }

        public static void CleanUpAfterEveryTestMethod()
        {
            //Driver.Close(); //close the current browser
            Driver.Quit(); //quit the driver
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Thread.Sleep((int)(timeSpan.TotalSeconds * 1000));
        }

    }
}


//public static string BaseAddress = "http://betsold-frontend-v3-corbetts-dev-eu-west-1.s3-website-eu-west-1.amazonaws.com/corbett/";

//public static void Initialize()
//{

//    //ChromeOptions option = new ChromeOptions();
//    //option.AddArgument("--headless");
//    //Driver = new ChromeDriver(option);
//    Driver = new ChromeDriver(@"C:\Work\AutomationTesting.Betsold");
//    Driver.Manage().Window.Maximize();
//}

//public static void Close()
//{
//Driver.Close(); //close the current browser
//Driver.Quit(); //quit the driver
//}