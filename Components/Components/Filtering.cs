using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions; //using this expected condition instead of OpenQA.Selenium.Support.UI

namespace Components.Components
{
    public class Filtering:WebDriverFactory
    {                                                                      
         static IWebElement TypeOfBetInput = Driver.FindElement(By.XPath("//*[@id='collapseFilterArea']/div/div[1]/div/input"));
         static IWebElement PriceRangeInput = Driver.FindElement(By.XPath("//*[@id='collapseFilterArea']/div/div[9]/div/input"));
         static IWebElement AuctionEndsInput = Driver.FindElement(By.XPath("//*[@id='collapseFilterArea']/div/div[11]/div/input"));
         static IWebElement ListingAmount = Driver.FindElement(By.Id("listingsAmount"));

         static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        ///////////////////////////////////////////   TYPE OF BET FILTER  //////////////////////////////////////////////////      

        public static bool CheckIfBetsAreFiltered_BySingles()
        {
            var elements = Driver.FindElements(By.ClassName("lotSummaryName"));

            int counter = 0;
            foreach (var element in elements.Where(e => e.Displayed))
            {
                counter += 1;
                element.Click();

                Thread.Sleep(2000);

                IWebElement BetType = wait.Until((d) =>
                {
                    return d.FindElement(By.CssSelector(".lot-detail-panel .auction-card-expanded .auction-row-expanded"));
                });

                var BetTypeAttribute = BetType.GetAttribute("data-bet-type");                                   
                var BetTitle = Driver.FindElements(By.ClassName("gc-bet-title"));
                var words = BetTitle.ElementAt(counter - 1).Text.Split(' ');
       
                string BetDetailHeader= words[0] + ' ' + words[1];
             
                if (!BetTypeAttribute.Equals("Single") && !BetDetailHeader.Equals("Match Betting:"))
                {
                    return false;
                }                               
            }              
            return true;
        }

        public static bool FilterByTypeOfBet_Single()
        {
            var amount = ListingAmount.Text;

            TypeOfBetInput.Click();
            var TypeOfBetUl = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark .select-dropdown.multiple-select-dropdown>li"))[1];
            var nameOfFilter = TypeOfBetUl.Text;
            TypeOfBetUl.Click();

             Thread.Sleep(2500);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;


            IWebElement Chip = wait.Until((d) =>
            {
                return d.FindElement(By.CssSelector(".chips-initial .chip"));
            });

            var textOfChip = Chip.Text;

            var HighBidSort = Driver.FindElement(By.Id("HighBid"));
            Actions action = new Actions(Driver);
            action.MoveToElement(HighBidSort).Perform();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBetsAreFiltered_BySingles())
            {
                return true;
            }
            return false;
        }

        ///////////////////////// DOUBLE ///////////////////////////////  
        public static bool CheckIfBetsAreFiltered_ByDouble()
        {
            var elements = Driver.FindElements(By.ClassName("lotSummaryName"));

            int counter = 0;
            foreach (var element in elements.Where(e =>e.Displayed))
            {
                counter += 1;
                Thread.Sleep(2000);

                element.Click();
                Thread.Sleep(2000);

                var tabPaneHeaders = wait.Until((d) =>
                {
                    return d.FindElements(By.ClassName("acca-tabs-header"));
                });

                var HeaderText = tabPaneHeaders.ElementAt(counter - 1).Text;
              
                var BetType = Driver.FindElement(By.CssSelector(".lot-detail-panel .auction-card-expanded .accumulator-area"));
                var BetTypeAttribute = BetType.GetAttribute("data-bet-type");

                if (!HeaderText.Equals("Double") && !BetTypeAttribute.Equals("Double"))
                {
                    return false;
                }               
            }
            return true;
        }

        public static bool FilterByTypeOfBet_Double()
        {
            var amount = ListingAmount.Text;

            TypeOfBetInput.Click();
            var TypeOfBetUl = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark .select-dropdown.multiple-select-dropdown>li"))[2];
            var nameOfFilter = TypeOfBetUl.Text;
            TypeOfBetUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text;

            var HighBidSort = Driver.FindElement(By.Id("HighBid"));
            Actions action = new Actions(Driver);//move the mouse out of the dropdown list
            action.MoveToElement(HighBidSort).Perform();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBetsAreFiltered_ByDouble())
            {
                return true;
            }
            return false;
        }

        /////////////// TREBLE///////////////////
        public static bool CheckIfBetsAreFiltered_ByTreble()
        {
            var elements = Driver.FindElements(By.ClassName("lotSummaryName"));

            int counter = 0;
            foreach (var element in elements.Where(e =>e.Displayed))
            {
                counter += 1;
                Thread.Sleep(2000);

                element.Click();
                Thread.Sleep(2000);
                var tabPaneHeaders = Driver.FindElements(By.ClassName("acca-tabs-header"));
                var HeaderText = tabPaneHeaders.ElementAt(counter -1).Text;

                var BetType = Driver.FindElement(By.CssSelector(".lot-detail-panel .auction-card-expanded .accumulator-area"));
                var BetTypeAttribute = BetType.GetAttribute("data-bet-type");


                if (!HeaderText.Equals("Treble") && !BetTypeAttribute.Equals("Treble"))
                {
                    return false;
                }
               
            }
           return true;
        }

        public static bool FilterByTypeOfBet_Treble()
        {
            var amount =  ListingAmount.Text;

            TypeOfBetInput.Click();
            var TypeOfBetUl = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark .select-dropdown.multiple-select-dropdown>li"))[3];
            var nameOfFilter = TypeOfBetUl.Text;
            TypeOfBetUl.Click();

            Thread.Sleep(2000);
            
            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip =  Chip.Text;

            var HighBidSort = Driver.FindElement(By.Id("HighBid"));
            Actions action = new Actions(Driver);
            action.MoveToElement(HighBidSort).Perform();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBetsAreFiltered_ByTreble())
            {             
                return true;
            }
            return false;
        }

        //ACCUMULATOR
        public static bool CheckIfBetsAreFiltered_ByAccumulator()
        {
            var elements = Driver.FindElements(By.ClassName("lotSummaryName"));

            int counter = 0;
            foreach (var element in elements)
            {
                counter += 1;
                Thread.Sleep(2000);

                element.Click();
                Thread.Sleep(2000);
                var tabPaneHeaders = Driver.FindElements(By.ClassName("acca-tabs-header"));
                var HeaderText = tabPaneHeaders.ElementAt(counter - 1).Text;

                var BetType = Driver.FindElement(By.CssSelector(".lot-detail-panel .auction-card-expanded .accumulator-area"));
                var BetTypeAttribute = BetType.GetAttribute("data-bet-type");

                if (!BetTypeAttribute.Equals("Accumulator"))
                {
                    return false;
                }               
            }
            return true;
        }

        public static bool FilterByTypeOfBet_Accumulator()
        {
            var amount = ListingAmount.Text;

            TypeOfBetInput.Click();
            var TypeOfBetUl = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark .select-dropdown.multiple-select-dropdown>li"))[4];
            var nameOfFilter = TypeOfBetUl.Text;
            TypeOfBetUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text;

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBetsAreFiltered_ByAccumulator())
            {
                return true;
            }
            return false;
        }

        //////////////////////////////// BID PRICE RANGE FILTER /////////////////////////////

        private static bool CheckPriceFilter_GenericFunction(int min, int max)
        {
            var elements = Driver.FindElements(By.ClassName("lotSummaryName"));

            int counter = 0;
            foreach (var element in elements.Where(e => e.Displayed))
            {
                counter += 1;
                Thread.Sleep(2000);

                element.Click();
                Thread.Sleep(2000);

                var CurrentHighBid = Driver.FindElements(By.CssSelector(".listing-detail-area.acca-listing .acca-area-row .postion-accumaltor-details .ld-center .ld-high-bid .txt-bold"));
                var highBid = float.Parse(CurrentHighBid.ElementAt(counter - 1).Text.Replace('£', ' '));

                if (highBid > min || highBid < max)
                {
                    continue;
                }
                return false;
            }

            return true;
        }

        /// UP TO £10   0-10
        public static bool CheckIfBidPricIsUpToTenPounds()
        {
            return CheckPriceFilter_GenericFunction(min:0,max:10);
        }

        public static bool FilterByPrice_UpToTenPounds()
        {
            
            var amount = ListingAmount.Text;
            PriceRangeInput.Click();

            var priceRangeUl = Driver.FindElements(By.CssSelector(".choosePrice .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[1];
           
            var nameOfFilter = priceRangeUl.Text.ToLower();

            priceRangeUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBidPricIsUpToTenPounds())
            {
                return true;
            }
            return false;
        }

        ////////////////////////////////// UP TO £50 ///////////////////////////////////////////
       
        public static bool CheckIfBidPricIsUpToFiftyPounds()
        {
            return CheckPriceFilter_GenericFunction(10, 50);        
        }

        public static bool FilterByPrice_UpToFiftyPounds()
        {
            var amount = ListingAmount.Text;
            PriceRangeInput.Click();

            var priceRangeUl = Driver.FindElements(By.CssSelector(".choosePrice .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[2];
          
            var nameOfFilter = priceRangeUl.Text.ToLower();

            priceRangeUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBidPricIsUpToFiftyPounds())
            {
                return true;
            }
            return false;
        }


        ///// UP TO £100 

        public static bool CheckIfBidPricIsUpToOneHoundredPounds()
        {
            return CheckPriceFilter_GenericFunction(min: 50, max: 100 );
        }

        public static bool FilterByPrice_UpToOneHoundredPounds()
        {
            var amount = ListingAmount.Text;
            PriceRangeInput.Click();

            var priceRangeUl = Driver.FindElements(By.CssSelector(".choosePrice .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[3];

            // var nameOfFilter = priceRangeUl.Text.Split(' ')[2].Replace('£',' ');
            var nameOfFilter = priceRangeUl.Text.ToLower();

            priceRangeUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBidPricIsUpToOneHoundredPounds())
            {
                return true;
            }
            return false;
        }

       
        ///// UP TO £500 (include all values less than 500pounds)
        public static bool CheckIfBidPricIsUpToFiveHoundredPounds()
        {
            return CheckPriceFilter_GenericFunction(min: 100, max: 500);
        }

        public static bool FilterByPrice_UpToFiveHoundredPounds()
        {
            var amount = ListingAmount.Text;
            PriceRangeInput.Click();

            var priceRangeUl = Driver.FindElements(By.CssSelector(".choosePrice .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[4];

            var nameOfFilter = priceRangeUl.Text.ToLower();

            priceRangeUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBidPricIsUpToFiveHoundredPounds())
            {
                return true;
            }
            return false;
        }

        ///// UP TO 1000 (include all values less than 1000pounds)
        public static bool CheckIfBidPricIsUpToOneThousandPounds()
        {
            return CheckPriceFilter_GenericFunction(min: 500, max: 1000);
        }

        public static bool FilterByPrice_UpToOneThousanddPounds()
        {
            var amount = ListingAmount.Text;
            PriceRangeInput.Click();

            var priceRangeUl = Driver.FindElements(By.CssSelector(".choosePrice .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[5];
            Thread.Sleep(1000);
           
            var nameOfFilter = priceRangeUl.Text.ToLower();
            priceRangeUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if (amount != newAmount && textOfChip == nameOfFilter && CheckIfBidPricIsUpToOneThousandPounds())
            {
                return true;
            }
            return false;
        }

        //OVER £1000
        public static bool CheckIfBidPricIsOverOneThousandPounds()
        {
            return CheckPriceFilter_GenericFunction(min: 1000, max: 100000000);
        }

        public static bool FilterByPrice_OverOneThousanddPounds()
        {
            var amount = ListingAmount.Text;
            PriceRangeInput.Click();

            var priceRangeUl = Driver.FindElements(By.CssSelector(".choosePrice .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[6];
            Thread.Sleep(1000);
            
            var nameOfFilter = priceRangeUl.Text.ToLower();

            priceRangeUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();   

            if (amount != newAmount && textOfChip == "over £1000 only" && CheckIfBidPricIsUpToOneThousandPounds())
            {
                return true;
            }
            return false;
        }


        ///////////////////////// AUCTION ENDS WITHIN FILTER //////////////////////
        
        public static bool CheckTimeLeft_Any()
        {
            var elements = Driver.FindElements(By.ClassName("lot-ends"));

            foreach (var element in elements.Where(e=>e.Displayed))
            {
                var dateTimeAttribute = element.GetAttribute("data-auction-end-date");
                DateTime dateDrrn = DateTime.Parse(dateTimeAttribute);
                
                var currentDate = DateTime.Now;
                var AuctionEnds = dateDrrn.Subtract(currentDate);
                var totalDays = AuctionEnds.Days;                
            }

            return true;
        }

        public static bool FilterByAuctionEnds_Any()
        {
            var amount = ListingAmount.Text;
            AuctionEndsInput.Click();
            Thread.Sleep(2000);
            var AuctionEndsUl = Driver.FindElements(By.CssSelector(".selectTime .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[5];
            AuctionEndsUl.Click();

            Thread.Sleep(2000);
            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
           
            if (amount == newAmount  && CheckTimeLeft_Any())
            {
                return true;
            }
            return false;
        }

        //NEXT HOUR
        public static bool CheckTimeLeft_OneHour()
        {
            Thread.Sleep(5000);
            var elements = Driver.FindElements(By.ClassName("lot-ends"));

            foreach (var element in elements.Where(e=>e.Displayed))
            {
                Thread.Sleep(5000);

                var dateTimeAttribute = element.GetAttribute("data-auction-end-date");
                DateTime dateDrrn = DateTime.Parse(dateTimeAttribute);
                var currentDate = DateTime.Now;

                var AuctionEnds = dateDrrn.Subtract(currentDate);

                var h = AuctionEnds.Hours;
                var m = AuctionEnds.Minutes;

                if ((h <= 1) || (m >= 0 || m <= 59))
                {
                    continue;
                }
                return false;
            }

            return true;
        }

        public static bool FilterByAuctionEnds_NextHour()
        {

            var amount = ListingAmount.Text;
            AuctionEndsInput.Click();

            var AuctionEndsUl = Driver.FindElements(By.CssSelector(".selectTime .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[1];
            AuctionEndsUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if ((amount != newAmount && textOfChip == "up to 1h" && CheckTimeLeft_OneHour())||amount.Equals("0"))
            {
                return true;
            }
            return false;
        }

        //NEXT TWO HOUR
        public static bool CheckTimeLeft_TwoHour()
        {
            Thread.Sleep(5000);
            var elements = Driver.FindElements(By.ClassName("lot-ends"));

            if (elements.Count > 0)
            {
                foreach (var element in elements.Where(e=>e.Displayed))
                {
                    Thread.Sleep(5000);

                    var dateTimeAttribute = element.GetAttribute("data-auction-end-date");
                    DateTime dateDrrn = DateTime.Parse(dateTimeAttribute);

                    var currentDate = DateTime.Now;

                    var AuctionEnds = dateDrrn.Subtract(currentDate);

                    var h = AuctionEnds.Hours;
                    var m = AuctionEnds.Minutes;

                    if ((h <= 2) || (m >= 0 || m <= 59))
                    {
                        continue;
                    }
                    return false;
                }
            }
            

            return true;
        }

        public static bool FilterByAuctionEnds_NextTwoHours()
        {

            var amount = ListingAmount.Text;
            AuctionEndsInput.Click();

            var AuctionEndsUl = Driver.FindElements(By.CssSelector(".selectTime .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[2];
            AuctionEndsUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if ((amount != newAmount && textOfChip == "up to 2h" && CheckTimeLeft_TwoHour()) || amount.Equals("0"))
            {
                return true;
            }
            return false;
        }



        //NEXT 24 HOURS
        public static bool CheckTimeLeft()
        {
            var elements = Driver.FindElements(By.ClassName("lot-ends"));

            foreach (var element in elements.Where(e =>e.Displayed))
            {
                Thread.Sleep(1000);
                var dateTimeAttribute = element.GetAttribute("data-auction-end-date");
                DateTime dateDrrn = DateTime.Parse(dateTimeAttribute);

                var currentDate = DateTime.Now;

                var AuctionEnds = dateDrrn.Subtract(currentDate);

                var h = AuctionEnds.Hours;
                var m = AuctionEnds.Minutes;

                if ((h>0 || h<=23) || (m>=0 || m<=59))
                {
                    continue;
                }
                return false;
            }

            return true;
        }

        public static bool FilterByAuctionEnds_NextDay()//next day(24 hours)
        {
            Thread.Sleep(2000);
            var amount = ListingAmount.Text;
            AuctionEndsInput.Click();
            Thread.Sleep(2000);
            var AuctionEndsUl = Driver.FindElements(By.CssSelector(".selectTime .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[3];
            AuctionEndsUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if ((amount != newAmount && textOfChip == "up to 24h" && CheckTimeLeft()) || amount.Equals("0"))
            {
                return true;
            }
            return false;
        }

        ///NEXT TWO DAYS (48 HOURS)
        public static bool CheckTimeLeft_NextTwoDays()
        {
            var elements = Driver.FindElements(By.ClassName("lot-ends"));

            foreach (var element in elements.Where(e =>e.Displayed))
            {
                Thread.Sleep(2000);
                var dateTimeAttribute = element.GetAttribute("data-auction-end-date");
                DateTime dateDrrn = DateTime.Parse(dateTimeAttribute);

                var currentDate = DateTime.Now;

                var AuctionEnds = dateDrrn.Subtract(currentDate);

                var d = AuctionEnds.Days;
                var h = AuctionEnds.Hours;
                var m = AuctionEnds.Minutes;

                if (d<=2 ||(h >= 0 || h <= 23) || (m >= 0 || m <= 59))
                {
                    continue;
                }
                return false;
            }

            return true;
        }

        public static bool FilterByAuctionEnds_NextTwoDay()//next two day(48 hours)
        {
            var amount = ListingAmount.Text;
            AuctionEndsInput.Click();
            Thread.Sleep(2000);
            var AuctionEndsUl = Driver.FindElements(By.CssSelector(".selectTime .colorful-select.dropdown-dark .dropdown-content.select-dropdown>li"))[4];
            AuctionEndsUl.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if ((amount != newAmount && textOfChip == "up to 48h" && CheckTimeLeft_NextTwoDays()) || amount.Equals("0"))
            {
                return true;
            }
            return false;
        }

        
        // NO BIDS
        public static bool CheckIfHighBidIsZero()
        {
            var elements = Driver.FindElements(By.ClassName("ar-hi-bid"));
            
            foreach (var element in elements.Where(e =>e.Displayed))
            {
                Thread.Sleep(1000);

                var highBid = float.Parse(element.Text.Replace('£', ' '));

                if (highBid != 0)
                {
                    return false;
                }                            
            }
            return true;
        }


        public static bool FilterByBidsMade_NO_BIDS()
        {         
            var amount = ListingAmount.Text;
            Thread.Sleep(1000);
            var BidsButton = Driver.FindElement(By.XPath("//*[@id='collapseFilterArea']/div/div[10]/div/span[1]/label"));
            Thread.Sleep(2000);
            BidsButton.Click();
            Thread.Sleep(2000);
            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if (amount != newAmount && textOfChip == "no bids" && CheckIfHighBidIsZero())
            {
                return true;
            }
            return false;
        }


        // BIDS MADE
        public static bool CheckIfHighBidIsGraterThenZero()
        {
            var elements = Driver.FindElements(By.ClassName("ar-hi-bid"));

            foreach (var element in elements.Where(e =>e.Displayed))
            {
               Thread.Sleep(1000);

               var highBid = float.Parse(element.Text.Replace('£', ' '));

               if (highBid == 0)
               {
                    return false;
               }
                           
            }
            return true;
        }

        public static bool FilterByBidsMade_BIDS_MADE()
        {
            var amount = ListingAmount.Text;
            Thread.Sleep(2000);
            var BidsButton = Driver.FindElement(By.XPath("//*[@id='collapseFilterArea']/div/div[10]/div/span[2]/label"));
            Thread.Sleep(2000);
            BidsButton.Click();
            Thread.Sleep(2000);
            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var Chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = Chip.Text.ToLower();

            if (amount != newAmount && textOfChip == "bids made" && CheckIfHighBidIsGraterThenZero())
            {
                return true;
            }
            return false;
        }



        //////////////// SALE TYPE FILTER //////////////

        public static bool CheckIfSaleTypeIsAuction()
        {
            Thread.Sleep(3000);
            var BetDetails = Driver.FindElements(By.ClassName("auction-row"));
            

            for (int i = 0; i < BetDetails.Count; i++)
            {
                var BuyNowAttribute = BetDetails[i].GetAttribute("data-buy-now");
                
                if (!BuyNowAttribute.Equals("0"))
                {
                    return false;
                }               
            }
            return true;
        }

        //AUCTION
        public static bool FilterBySaleType_Auction()
        {
            var amount = ListingAmount.Text;
            Thread.Sleep(2000);

            var BuyItNowButton = Driver.FindElement(By.CssSelector("#collapseFilterArea > div > div.col-lg-2.chooseAuction.z-depth-1 > div > span:nth-child(2) > label"));
            Thread.Sleep(2000);
            BuyItNowButton.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = chip.Text.ToLower();

            if (amount != newAmount && textOfChip == "auction" && CheckIfSaleTypeIsAuction())
            {
                return true;
            }

            return false;
        }

        //BUY IT NOW

        public static bool CheckIfSaleTypeIsBuyItNow()
        {
            Thread.Sleep(3000);
            var BetDetails = Driver.FindElements(By.ClassName("auction-row"));

            for (int i = 0; i < BetDetails.Count; i++)
            {
                var BuyNowAttribute = BetDetails[i].GetAttribute("data-buy-now");
                var number = float.Parse(BuyNowAttribute);

                if (!BuyNowAttribute.Equals("0"))
                {
                    continue;
                }
               return false;
            }

            return true;
        }

        //BUY IT NOW
        public static bool FilterBySaleType_BuyItNow()
        {
            var amount = ListingAmount.Text;
            Thread.Sleep(2000);

            var BuyItNowButton = Driver.FindElement(By.CssSelector("#collapseFilterArea > div > div.col-lg-2.chooseAuction.z-depth-1 > div > span:nth-child(1) > label"));
            Thread.Sleep(2000);
            BuyItNowButton.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = chip.Text.ToLower();

            if (amount != newAmount && textOfChip == "buy now" && CheckIfSaleTypeIsBuyItNow())
            {
                return true;
            }

            return false;
        }

        ///////// FILTER BY TYPE OF SPORT ///////////
        public static bool CheckIfFilterByHorseRacing() 
        {
            Thread.Sleep(3000);
            var BetDetailsSort = Driver.FindElement(By.Id("BetDescription"));
            BetDetailsSort.Click();
            Thread.Sleep(2000);
            var betDetails = Driver.FindElements(By.CssSelector(".accordianheader.bet-detail"));

            for (int i = 0; i < betDetails.Count; i++)
            {
                var betDetailText = betDetails[i].Text;

                if (!betDetailText.Contains("Race Winner"))
                {
                    return false;
                }             
            }
            return true;
        }

        public static bool FilterByHorseRacingType()
        {
            var amount = ListingAmount.Text;
            Thread.Sleep(2000);

            var horseRacing = Driver.FindElement(By.CssSelector("#sportsSideMenu > li:nth-child(2) > a"));
            Thread.Sleep(2000);
            horseRacing.Click();

            Thread.Sleep(2000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = chip.Text.ToLower();

            if (amount != newAmount && textOfChip == "horse racing" && CheckIfFilterByHorseRacing())
            {
                return true;
            }

            return false;
        }

        public static bool CheckIfFilterFootballType()
        {
            Thread.Sleep(3000);
            var BetDetailsSort = Driver.FindElement(By.Id("BetDescription"));
            BetDetailsSort.Click();//descending
            BetDetailsSort.Click();//ascending
            Thread.Sleep(2000);
            var betDetails = Driver.FindElements(By.CssSelector(".accordianheader.bet-detail"));

            for (int i = 0; i < betDetails.Count; i++)
            {
                var betDetailText = betDetails[i].Text;

                if (!betDetailText.Contains("Correct Score"))
                {
                    return false;
                }              
            }

            return true;
        }

        public static bool FilterByFootballType()
        {
            var amount = ListingAmount.Text;
            Thread.Sleep(2000);
          
            var football = Driver.FindElement(By.CssSelector("#sportsSideMenu > li:nth-child(1) > a"));
            Thread.Sleep(2000);
            football.Click();

            Thread.Sleep(3000);

            var newAmount = Driver.FindElement(By.Id("listingsAmount")).Text;
            var chip = Driver.FindElement(By.CssSelector(".chips-initial .chip"));
            var textOfChip = chip.Text.ToLower();

            if (amount != newAmount && textOfChip == "football" && CheckIfFilterFootballType())
            {
                return true;
            }

            return false;
        }

        public static bool SaveFilters()
        {
            TypeOfBetInput.Click();
            var TypeOfBetUl = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark .select-dropdown.multiple-select-dropdown>li"))[1];//singles
            var nameOfFilter = TypeOfBetUl.Text;
            TypeOfBetUl.Click(); //SINGLES

            Thread.Sleep(2000);
            var BidsButton = Driver.FindElement(By.XPath("//*[@id='collapseFilterArea']/div/div[10]/div/span[1]/label"));
            Thread.Sleep(2000);
            BidsButton.Click(); //WITHOUT BIDS
            Thread.Sleep(2000);

            var Chip1 = Driver.FindElement(By.CssSelector("#auction-page > div.safari-fix > div.filterByRow.row > div > div:nth-child(2)"));
            var textOfChip1 = Chip1.Text.ToLower();
            var Chip2 = Driver.FindElement(By.CssSelector("#auction-page > div.safari-fix > div.filterByRow.row > div > div:nth-child(1)"));
            var textOfChip2 = Chip2.Text.ToLower();

            var SaveFilters = Driver.FindElement(By.Id("saveFilterLink"));
            SaveFilters.Click();

            var ModalInput = Driver.FindElement(By.CssSelector("#filterPreferencesModal > div > div > div.modal-body > div > input"));
            Thread.Sleep(2000);
            ModalInput.Click();
            ModalInput.SendKeys("ilija-filters");
            var ModalSaveButton = Driver.FindElement(By.CssSelector("#filterPreferencesModal > div > div > div.modal-body > div > a"));
            ModalSaveButton.Click();

            Thread.Sleep(3000);

           //resetfilter   classname
            var ResetFilter = Driver.FindElement(By.ClassName("resetfilter"));
            ResetFilter.Click();
            Thread.Sleep(2000);

            var SavedSearches = Driver.FindElement(By.XPath("//*[@id='collapseFilterArea']/div/div[7]/div"));
            SavedSearches.Click();
            Thread.Sleep(1000);
            var elements = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark .dropdown-content.select-dropdown>li")).ToArray();
            var visible = elements.Where(r => r.Displayed).ToArray();
            visible.Last().Click();

            Thread.Sleep(1000);
            var Chip11 = Driver.FindElement(By.CssSelector("#auction-page > div.safari-fix > div.filterByRow.row > div > div:nth-child(2)"));
            var textOfChip11 = Chip11.Text.ToLower();

            var Chip22 = Driver.FindElement(By.CssSelector("#auction-page > div.safari-fix > div.filterByRow.row > div > div:nth-child(1)"));
             var textOfChip22 = Chip22.Text.ToLower();

            if (textOfChip2.Equals(textOfChip22) && textOfChip2.Equals(textOfChip22))
            {
                return true;
            }
            return false;
        }


        public static bool IterateBidOdds_Fractional()
        {

            var elements = Driver.FindElements(By.ClassName("ar-a-odds"));

            for (int i = 0; i < elements.Count-1; i++)
            {
                string[] stringSeparators = new string[] { "\r\n" };

                var highBid = elements[i].Text.Split(stringSeparators, StringSplitOptions.None)[0];

                if(String.IsNullOrEmpty(highBid))
                {
                    continue;                    
                }
                else if (!String.IsNullOrEmpty(highBid) && Regex.IsMatch(highBid, @"[1-9]/[0-9]") )
                {
                    continue;
                }

                    return false;
            }

            return true;
        }

        public static bool OddsFormat_Fractional()
        {
            Thread.Sleep(2000);

            //#auctionNavigationBar > ul > li:nth-child(9) > a
            var ConfigurePreferencesButton = Driver.FindElement(By.CssSelector("#auctionNavigationBar > ul > li:nth-child(9) > a"));
            ConfigurePreferencesButton.Click();
            Thread.Sleep(2000);

            var OkButtonModal = Driver.FindElement(By.CssSelector("#auctionHelpModal > div > div > div.modal-footer.justify-content-center.z-depth-1 > a"));

            var FractionalButton = Driver.FindElement(By.Id("btn-odds-fractional"));
            FractionalButton.Click();
            OkButtonModal.Click();

            if (IterateBidOdds_Fractional())
            {
                return true;
            }

            return false;
        }

        public static bool IterateBidOdds_Decimal()
        {

            var elements = Driver.FindElements(By.ClassName("ar-a-odds"));

            for (int i = 0; i < elements.Count - 1; i++)
            {
                // 103/1

                string[] stringSeparators = new string[] { "\r\n" };

                var highBid = elements[i].Text.Split(stringSeparators, StringSplitOptions.None)[0];

                if (String.IsNullOrEmpty(highBid))
                {
                    continue;
                }
                else if (!String.IsNullOrEmpty(highBid) && (Regex.IsMatch(highBid, @"[1-9].[0-9]") || highBid.Contains("NaN")))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public static bool OddsFormat_Decimal()
        {
            Thread.Sleep(2000);
            var ConfigurePreferencesButton = Driver.FindElement(By.CssSelector("#auctionNavigationBar > ul > li:nth-child(9) > a"));
            ConfigurePreferencesButton.Click();
            Thread.Sleep(2000);

            var OkButtonModal = Driver.FindElement(By.CssSelector("#auctionHelpModal > div > div > div.modal-footer.justify-content-center.z-depth-1 > a"));

            var DecimalButton = Driver.FindElement(By.Id("btn-odds-decimal"));
            DecimalButton.Click();
            OkButtonModal.Click();

            if (IterateBidOdds_Decimal())
            {
                return true;
            }

            return false;
        }


        public static bool IterateBidOdds_American()
        {

            var elements = Driver.FindElements(By.ClassName("ar-a-odds"));

            for (int i = 0; i < elements.Count - 1; i++)
            {
                // 103/1

                string[] stringSeparators = new string[] { "\r\n" };

                var highBid = elements[i].Text.Split(stringSeparators, StringSplitOptions.None)[0];

                if (String.IsNullOrEmpty(highBid))
                {
                    continue;
                }
                else if (!String.IsNullOrEmpty(highBid) && (Regex.IsMatch(highBid, @"^[\-\+\s]*[0-9\s]+$") || highBid.Contains("NaN")))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public static bool OddsFormat_American()
        {
            Thread.Sleep(2000);
            var ConfigurePreferencesButton = Driver.FindElement(By.CssSelector("#auctionNavigationBar > ul > li:nth-child(9) > a"));
            ConfigurePreferencesButton.Click();
            Thread.Sleep(2000);

            var OkButtonModal = Driver.FindElement(By.CssSelector("#auctionHelpModal > div > div > div.modal-footer.justify-content-center.z-depth-1 > a"));

            var DecimalButton = Driver.FindElement(By.Id("btn-odds-american"));
            DecimalButton.Click();
            OkButtonModal.Click();

            if (IterateBidOdds_American())
            {
                return true;
            }

            return false;
        }

        public static bool HideFilterOptions()
        {
            var HideButton = Driver.FindElement(By.Id("filterDirectionText"));
            HideButton.Click();
            Thread.Sleep(2000);

            var NameOfFilters = Driver.FindElement(By.ClassName("chooseTxt"));

            if (!NameOfFilters.Displayed)
            {
                return true;
            }
            return false;
        }
        public static bool ShowFilterOptions()
        {
            var HideButton = Driver.FindElement(By.Id("filterDirectionText"));
            HideButton.Click();
            Thread.Sleep(2000);
            HideButton.Click();

            var NameOfFilters = Driver.FindElement(By.ClassName("chooseTxt"));

            if (NameOfFilters.Displayed)
            {
                return true;
            }
            return false;
        }


    }
}
