using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using NUnit.Framework;
using Tests.Base;
using Components.Components;

namespace Tests.SmokeTests
{
    [TestFixture]
    public class SearchingTest:BaseTest
    {
        [Test]
        public static void Check_If_Term_Exists_In_Table()
        {          
            Searching.SearchComponent("Chelsea");
            Assert.IsTrue(Searching.IterateTable("Chelsea"));
        }
    }
}
