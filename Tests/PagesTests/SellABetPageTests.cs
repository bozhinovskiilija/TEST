using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using Components.Components;
using Components.Pages;
using NUnit.Framework;
using Tests.Base;

namespace Tests.PagesTests
{
    [TestFixture]
    public class SellABetPageTests:WebDriverFactory
    {
        [SetUp]
        public void SetUp()
        {
           /* Driver =*/ Create(BrowserType.Chrome);
            Driver.Navigate().GoToUrl("http://betsold-frontend-v3-corbetts-dev-eu-west-1.s3-website-eu-west-1.amazonaws.com/corbett/");
            Thread.Sleep(5000);
            Driver.Manage().Window.Maximize();
           // Login.LoginComponent("nicola1", "asd");
            Driver.SwitchTo().Frame("betsold-iframe");
        }

        [TearDown]
        public void CleanUp()
        {
            CleanUpAfterEveryTestMethod();
        }

      /*  [Test]
        public static void ListBetInTheAuctionTest()
        {
           Assert.IsTrue(SellABetPage.ListBetInAuction());
        }*/
    }
}
