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
    public class MyAccountPageTests: BaseTest        
    {
        
        [Test]
        public static void BidsOverviewTests()
        {
          Assert.IsTrue(MyAccountPage.BidsOverview());  
        }

        [Test]
        public static void WatchedOverviewTests()
        {
            Assert.IsTrue(MyAccountPage.WatchedOverview(),"Different values - possible bug");
        }

        [Test]
        public static void ListingsOverviewTests()
        {
            Assert.IsTrue(MyAccountPage.ListingsOverview(), "Different values in listing overview and listing details- possible bug !!");
        }

        [Test] //TO-DO
        public static void HistoryOverviewTests()
        {
            Assert.IsTrue(MyAccountPage.HistoryOverview(), "Different values in history overview and history details - possible bug !!!");
        }

        [Test]
        public static void HistoryOverviewTests_BidsSearching()
        {
            Assert.IsTrue(MyAccountPage.SearchHistoricBids("chelsea"), "Incorect results!");
        }

        [Test]
        public static void HistoryOverviewTests_ListingsSearching()
        {
            Assert.IsTrue(MyAccountPage.SearchHistoricListings("chelsea"), "Incorect results");
        }




    }
}
