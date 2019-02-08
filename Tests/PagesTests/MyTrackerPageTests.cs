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
    public class MyTrackerPageTests: BaseTest                  
    {

        [Test]
        public static void List_All_Bets_I_Have_Bidded_For()
        {           
            Assert.IsTrue(MyTrackerPage.ListAllBetsIHaveBiddedFor());           
        }

        [Test]
        public static void List_All_Bets_I_Have_Watched()
        {
            Assert.IsTrue(MyTrackerPage.ListAllBetsIHaveWatched());
        }

        [Test]
        public static void Unwatching_Lot_From_MyTracker()
        {
            Assert.IsTrue(MyTrackerPage.UnwathLotFromTracker(),"Lot is not removed from my tracker watching");
        }

        [Test]
        public static void Sort_Bets_I_Have_Bidded_For_By_WinningBids()
        {
            Assert.IsTrue(MyTrackerPage.SortBy_WinningBids());
        }

        [Test]
        public static void Sort_Bets_I_Have_Bidded_For_By_EndingSoonest()
        {
            Assert.IsTrue(MyTrackerPage.SortBy_EndingSoon());
        }
    }
}
