using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Components.Components;
using NUnit.Framework;
using Tests.Base;



namespace Tests.SmokeTests
{
    [TestFixture]
    [Category("Filters")]
    public class FilteringTests : BaseTest
    {
        public static ExtentTest test;
        public static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(@"C:\Betsold\AutomationTesting\Tests\BetsoldExtentReport.html");
        public static ExtentReports extent = new ExtentReports();



        [Test]
        [Category("Type of bet")]
        public static void Check_Filter_TypeOfBet_Single()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by type SINGLE");

            Assert.IsTrue(Filtering.FilterByTypeOfBet_Single());

            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Type of bet")]
        public static void Check_Filter_TypeOfBet_Double()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by type DOUBLE");

            Assert.IsTrue(Filtering.FilterByTypeOfBet_Double());

            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Type of bet")]
        public static void Check_Filter_TypeOfBet_Treble()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by type TREBLE");

            Assert.IsTrue(Filtering.FilterByTypeOfBet_Treble());

            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Type of bet")]    //TEST NOT DONE YET
        public static void Check_Filter_TypeOfBet_Accumulator()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by type TREBLE");

            Assert.IsTrue(Filtering.FilterByTypeOfBet_Accumulator());

            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }



        [Test]
        public static void Filter_By_Price_Range_From_0_To_10_Pounds()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by price range");

            Assert.IsTrue(Filtering.FilterByPrice_UpToTenPounds());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }
        [Test]
        public static void Filter_By_Price_Range_From_10_To_50_Pounds()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by price range");

            Assert.IsTrue(Filtering.FilterByPrice_UpToFiftyPounds());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Filter_By_Price_Range_From_50_To_100_Pounds()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by price range up to 100p");

            Assert.IsTrue(Filtering.FilterByPrice_UpToOneHoundredPounds());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Filter_By_Price_Range_From_100_To_500_Pounds()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by price range up to 500p");

            Assert.IsTrue(Filtering.FilterByPrice_UpToFiveHoundredPounds());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Filter_By_Price_Range_From_500_To_1000_Pounds()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by price range up to 10000p");

            Assert.IsTrue(Filtering.FilterByPrice_UpToOneThousanddPounds());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }


        [Test]
        public static void Filter_By_Price_Range_Over_1000_Pounds()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by price range over 10000p");

            Assert.IsTrue(Filtering.FilterByPrice_OverOneThousanddPounds());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Filter By Auction Ends")]
        public static void Filter_By_Auction_Ends_NextDay()//next 24 hours
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by auction ends");
                    
            Assert.IsTrue(Filtering.FilterByAuctionEnds_NextDay());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Filter By Auction Ends")]
        public static void Filter_By_Auction_Ends_NextTwoDay()//next 48 hours
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by auction ends");

            Assert.IsTrue(Filtering.FilterByAuctionEnds_NextTwoDay());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Filter By Auction Ends")]
        public static void Filter_By_Auction_Ends_NextOneHour()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by auction ends");

            Assert.IsTrue(Filtering.FilterByAuctionEnds_NextHour());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Filter By Auction Ends")]
        public static void Filter_By_Auction_Ends_NextTwoHour()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by auction ends");

            Assert.IsTrue(Filtering.FilterByAuctionEnds_NextTwoHours());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        [Category("Filter By Auction Ends")]
        public static void Filter_By_Auction_Ends_Any()
        {
            Assert.IsTrue(Filtering.FilterByAuctionEnds_Any());         
        }



/*
        [Test]
        public static void Check_Filter_By_Status()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by status");

            Assert.IsTrue(Filtering.FilterByStatus());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

    */

        [Test]
        public static void Filter_Lots_With_No_Bids()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots that have no bids");
          
            Assert.IsTrue(Filtering.FilterByBidsMade_NO_BIDS(),"There are lots with bids");
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Filter_Lots_With_Bids_Made()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots that have no bids");

            Assert.IsTrue(Filtering.FilterByBidsMade_BIDS_MADE(), "There are lots with no bids");
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Filter_Lots_By_Sale_Type_Auction()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots that are on auction");

            Assert.IsTrue(Filtering.FilterBySaleType_Auction(), "There are lots with no bids");
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Filter_Lots_By_Sale_Type_BuyItNow()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots that are on auction");

            Assert.IsTrue(Filtering.FilterBySaleType_BuyItNow(), "There are lots with no bids");
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }
/*
        [Test]
        public static void Filter_By_Type_Of_Sport_Horse_Racing()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by horse racing");

            Assert.IsTrue(Filtering.FilterByHorseRacingType());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Filter_By_Type_Of_Sport_Football()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Filter lots by footbal");
           
            
            Assert.IsTrue(Filtering.FilterByFootballType());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }
        */
        [Test]
        public static void Save_Filter_Functionality()
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Save filters functionality");
          
            Assert.IsTrue(Filtering.SaveFilters());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();
        }

        [Test]
        public static void Choose_Odds_Format_Preference_Fractional()
        {
            extent.AttachReporter(htmlReporter);          
            test = extent.CreateTest("Fractional odds format");
          
           
            Assert.IsTrue(Filtering.OddsFormat_Fractional());
            test.Log(Status.Pass, "Test case PASS");
            extent.Flush();

        }
        [Test]
        public static void Choose_Odds_Format_Preference_Decimal()
        {
            extent.AttachReporter(htmlReporter);
            
            test = extent.CreateTest("Decimal odds format");
            Assert.IsTrue(Filtering.OddsFormat_Decimal());
            test.Log(Status.Pass, "Test case PASS");

            extent.Flush();
        }

        [Test]
        public static void Choose_Odds_Format_Preference_American()
        {

            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("American odds format");

           
            Assert.IsTrue(Filtering.OddsFormat_American());
            test.Log(Status.Pass, "Test case PASS");

            extent.Flush();
        }
        //HideFilterOptions

        [Test]
        public static void HideFilters()
        {
            Assert.IsTrue(Filtering.HideFilterOptions());         
        }

        [Test]
        public static void ShowFilters()
        {
            Assert.IsTrue(Filtering.ShowFilterOptions());
        }
    }
}
