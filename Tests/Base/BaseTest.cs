using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using NUnit.Framework;

namespace Tests.Base
{
    [TestFixture]
    public class BaseTest : WebDriverFactory
    {

  
        [SetUp]
        public void Setup()
        {
           
            Driver = Create(BrowserType.Chrome);
            ClearBrowserCache();
            Driver.Navigate().GoToUrl("https://corbett-sit.betsoldsport.com/?tsk=justgetmein2");
            Driver.Manage().Window.Maximize();
            // Driver.SwitchTo().Frame("betsold-iframe");   
        }

        //[TearDown]
        //public void CleanUp()
        //{
        //    CleanUpAfterEveryTestMethod();
        //}

        [TearDown]
        public void CleanUp()
        {
            if (Driver != null)
                Driver.Dispose();

        }

    }
}



//public static void Setup(String browserName)
//{
//    if (browserName.Equals("chrome"))
//        /* Driver = */
//        Create(BrowserType.Chrome);

//    else /*(browserName.Equals("firefox"))*/
//         /* Driver = */
//        Create(BrowserType.Firefox);
//    //else 
//    //    /* Driver = */ Create(BrowserType.IE);



//    Driver.Navigate().GoToUrl("https://corbett-sit.betsoldsport.com/?tsk=justgetmein2");
//    //Driver.Navigate().GoToUrl("http://betsold-frontend-v3-corbetts-dev-eu-west-1.s3-website-eu-west-1.amazonaws.com/corbett/");
//    Driver.Manage().Window.Maximize();
//    // Driver.SwitchTo().Frame("betsold-iframe");   
//}







//[SetUp]
//public void Setup()
//{
//    WebDriverFactory.Initialize();
//    WebDriverFactory.Driver.Navigate().GoToUrl("http://betsold-frontend-v3-corbetts-test-eu-west-1.s3-website-eu-west-1.amazonaws.com/corbett/");
//    WebDriverFactory.Driver.SwitchTo().Frame("betsold-iframe");
//    //LoginPage.GoTo();
//    //LoginPage.Login("username", "password");
//}
//[TearDown]
//public void CleanUp()
//{
//WebDriverFactory.Close();
//}