using System;
using System.Threading.Tasks;
using Components.Components;
using NUnit.Framework;
using Tests.Base;


namespace Tests.SmokeTests
{
    [TestFixture]
   
    class SortingTest:BaseTest
    {
        
        [Test]
     
        public static void Check_If_Elements_Are_Sorted_Profit()
        {
            Sorting.SortByMaxWin_Descending();
            Assert.IsTrue(Sorting.CheckSortTableByMaxWin_Descending());
        }

        [Test]
   
        public static void Check_If_Elements_Are_Sorted_By_Description()
        {

            Sorting.SortByDescription_Descending();
            Assert.IsTrue(Sorting.CheckSortTableByDescription_Descending());
        }


        [Test]
      
        public static void Check_If_Elements_Are_Sorted_By_HighBid()
        {

            Sorting.SortByHighBid_Descending();
            Assert.IsTrue(Sorting.CheckSortTableByHighBid_Descending());
        }


    }
}
