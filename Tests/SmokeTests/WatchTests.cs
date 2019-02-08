using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationCore;
using Components.Components;
using NUnit.Framework;
using Tests.Base;

namespace Tests.SmokeTests
{
    [TestFixture]
    public class WatchTests :BaseTest
    {
       
        [Test]
        public void Adding_Bet_To_MyTracker_Page()
        {
           Assert.IsTrue(Watch.StartWatchingAuction()); 
        }
    }
}
