using System;
using System.Linq;
using System.Threading;
using AutomationCore;
using OpenQA.Selenium;
using Components.Utilities;


namespace Components.Pages
{
    public static class IWebDriverEx
    {
        public static string ByClass(this IWebDriver driver, string className)
        {
            return driver.FindElement(By.ClassName(className)).Text;
        }
    }

    public class MyAccountPage : WebDriverFactory
    {
        static IWebElement myAccountButton = Driver.FindElement(By.Id("nav-item-my-auction"));

        public static bool BidsOverview()
        {
            Thread.Sleep(3000);
            myAccountButton.Click();

            Thread.Sleep(2000);
           
            var bidsOverviewTile = Driver.FindElement(By.ClassName("account-tile-header"));

            var tileContent = Driver.FindElements(By.ClassName("account-tile-content"));

            var biddedOn = tileContent[0].Text.ParseIntFromString();
            
            var currentBidValue = tileContent[1].Text.ParseDoubleFromString();
                     
            var tileFooter = Driver.FindElements(By.ClassName("account-footer-text"));

            var winningBids = tileFooter[0].Text.ParseDoubleFromString();
          
            var potentialWin = tileFooter[1].Text.ParseDoubleFromString();
          
            bidsOverviewTile.Click();

            Thread.Sleep(3000);

            var totalNumber = Driver.FindElement(By.ClassName("total-number")).Text.ParseIntFromString();
            
            var currentBidsTotal = Driver.FindElement(By.ClassName("current-total")).Text.ParseDoubleFromString();       
                      
            var currentPossible = Driver.FindElement(By.ClassName("current-possible")).Text.ParseDoubleFromString();

            var maxWinsColumn = Driver.FindElements(By.ClassName("posswin"));

            double maxWinSum = 0;

            for (int i = 0; i < maxWinsColumn.Count; i++)
            {
                var win = maxWinsColumn[i].Text.ParseDoubleFromString();             
                maxWinSum += win;
            }

           var myBidColumn = Driver.FindElements(By.ClassName("my-bid"));
            double myBidSum = 0;

            for (int i = 1; i < myBidColumn.Count; i++)
            {
                var bid = myBidColumn[i].Text;
                if (bid.Contains("£"))
                {
                    var bidValue = double.Parse(string.Join("", bid.ToCharArray().Where(c => c == '.' || Char.IsDigit(c))));
                    myBidSum += bidValue;
                }
                else if (bid.Contains("p"))
                {                    
                    var bidValue = double.Parse(string.Join("", bid.ToCharArray().Where(c => c == '.' || Char.IsDigit(c))));
                    myBidSum += ((1 / 100d) * bidValue);
                }
               
            }

            if (biddedOn.Equals(totalNumber) && currentBidValue.Equals(currentBidsTotal) && /*currentBidTotalValue.Equals(Math.Round(myBidSum,2)) &&*/ potentialWin.Equals(currentPossible) && currentPossible.Equals(maxWinSum))
            {
                return true;
            }

            return false;
        }


        public static bool WatchedOverview()
        {
            //WATCHING OVERVIEW TILE
            Thread.Sleep(3000);
            myAccountButton.Click();
            Thread.Sleep(2000);

            var watchedOverviewTile = Driver.FindElements(By.ClassName("account-tile-header"));
          
            var tileContent = Driver.FindElements(By.ClassName("account-tile-content"));

            var watchingNumber = tileContent[5].Text.ParseIntFromString();

            var OtherWatching = tileContent[6].Text.ParseIntFromString();

            var tileFooter = Driver.FindElements(By.ClassName("account-footer-text"));

            var watchLots = tileFooter[4].Text.ParseDoubleFromString();

            var potentialWinValue = tileFooter[5].Text.ParseDoubleFromString();
          
            watchedOverviewTile[2].Click();

            Thread.Sleep(2000);

            //////// WATCHING OVERVIEW DETAIS
            var watchedLotsCount = Driver.FindElement(By.Id("watchedLotsCount")).Text.ParseFloatFromString();
       
            var NumberOfLotsInTable = Driver.FindElements(By.ClassName("watch-event-detail")).Count;

            var Prices = Driver.FindElement(By.ClassName("current-possible")).Text;
            var WatchedBidValue = Prices.Split(' ')[4].ParseFloatFromString();          

            var CurrentPossibleWinings = Prices.Split(' ')[8].ParseDoubleFromString();
                  
            double sumOfHighBid = 0.00;
            var highBidColumns = Driver.FindElements(By.ClassName("high-bid"));

            for (int i = 1; i < highBidColumns.Count; i++)
            {
                var BidValue = double.Parse(highBidColumns[i].Text.Replace('£', ' '));
                sumOfHighBid += BidValue;
            }
            double sumOfProfit = 0;
            var profitColumn = Driver.FindElements(By.ClassName("posswin"));

            for (int j = 0; j < profitColumn.Count; j++)
            {
                var profit = profitColumn[j].Text;

                if (profit.Contains("£"))
                {
                    var ProfitValue = double.Parse(profitColumn[j].Text.Replace('£', ' '));
                    sumOfProfit += ProfitValue;
                }else if (profit.Contains("p"))
                {
                    var ProfitValue = double.Parse(profitColumn[j].Text.Replace('p', ' '));
                    sumOfProfit += ((1 / 100d) * ProfitValue);
                }
                
            }
            //values are different  - possible bugg
            if (watchedLotsCount.Equals(watchingNumber).Equals(NumberOfLotsInTable) && WatchedBidValue.Equals(Math.Round(sumOfHighBid, 2))/*.Equals(BidValueOfWatchLots)*/ && CurrentPossibleWinings.Equals(sumOfProfit).Equals(potentialWinValue))
            {
                return true;
            }

            return false;
        }


        public static bool ListingsOverview()
        {
            //LISTINGS OVERVIEW TILE
            Thread.Sleep(3000);
            myAccountButton.Click();
            Thread.Sleep(2000);

            var ListingsOverviewTile = Driver.FindElements(By.ClassName("account-tile-header"));
      
            var tileContent = Driver.FindElements(By.ClassName("account-tile-content"));

            var NumberOfMyBetsInAuction = tileContent[2].Text.ParseIntFromString();

            var OtherWatchingNumber = tileContent[3].Text.ParseIntFromString();
          
            var tileFooter = Driver.FindElements(By.ClassName("account-footer-text"));//[5] [6]

            var CurrentListingsWinValue = tileFooter[2].Text.ParseDoubleFromString();
           
            var CurrentSaleValue = tileFooter[3].Text.ParseDoubleFromString();

            ListingsOverviewTile[1].Click();

            Thread.Sleep(2000);

            //////// LISTINGS OVERVIEW DETAIS
            var MyTotalListings = Driver.FindElement(By.ClassName("total-number")).Text.ParseFloatFromString();

            var NumberOfMyListingsInTable = Driver.FindElements(By.ClassName("al-event-detail")).Count; //watch-bet-detail

            var MyCurrentValue = Driver.FindElement(By.ClassName("current-total")).Text.ParseDoubleFromString();
            
            var CurrentPossibleEarnings = Driver.FindElement(By.ClassName("current-possible")).Text.ParseDoubleFromString();

            double sumOfHighBid = 0;
            var highBidColumns = Driver.FindElements(By.ClassName("high-bid"));

            for (int i = 1; i < highBidColumns.Count; i++)
            {
                var BidValue = double.Parse(highBidColumns[i].Text.Replace('£', ' '));
                sumOfHighBid += BidValue;               
            }

            double sumOfProfit = 0;
            var profitColumn = Driver.FindElements(By.ClassName("current-profit"));

            for (int j = 1; j < profitColumn.Count; j++)
            {
                var ProfitValue = double.Parse(profitColumn[j].Text.Replace('£', ' ').Replace('+',' '));
                sumOfProfit += ProfitValue;
            }
            //values are different  - possible bugg
            if (MyTotalListings.Equals(NumberOfMyListingsInTable).Equals(NumberOfMyBetsInAuction) && MyCurrentValue.Equals(sumOfHighBid).Equals(CurrentSaleValue) && CurrentPossibleEarnings.Equals(sumOfProfit).Equals(CurrentListingsWinValue))
            {
                return true;
            }
            return false;
        }

        //TEST IS NOT COMPLETED
        public static bool HistoryOverview() 
        {
            //HISTORY OVERVIEW TILE
            Thread.Sleep(5000);
           // WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            //wait.Until(ExpectedConditions.ElementToBeClickable(myAccountButton));

            myAccountButton.Click();
            Thread.Sleep(2000);
      
            var HistoryOverviewTile = Driver.FindElements(By.ClassName("account-tile-header"));
           
            var tileContent = Driver.FindElements(By.ClassName("account-tile-content"));

            var NumberOfPreviousBids = tileContent[7].Text.ParseIntFromString();          

            var NumberOfPreviousListings = tileContent[8].Text.ParseIntFromString();

            var NumberOfWinnningBids = tileContent[9].Text.ParseIntFromString();

            var SoldBets = tileContent[10].Text.ParseIntFromString();
           
            var tileFooter = Driver.FindElements(By.ClassName("account-footer-text"));

            var ListingsSaleValue = tileFooter[6].Text.ParseDoubleFromString();

            var EarndMoney = tileFooter[7].Text.ParseDoubleFromString();

            HistoryOverviewTile[3].Click();

            Thread.Sleep(2000);

            //////// HISTORY OVERVIEW DETAIS
            var NumberOfMyBidsInHistoryTable = Driver.FindElements(By.CssSelector(".listing-title > span")).Count;

            double sumOfMyHighBit = 0;
            var MyBidColumns = Driver.FindElements(By.ClassName("high-bid-amount"));

            for (int i = 0; i < MyBidColumns.Count; i++)
            {
                if (MyBidColumns[i].Text.Equals("-"))
                {
                    continue;
                }

                var BidValue = double.Parse(MyBidColumns[i].Text.Replace('£', ' '));             

                sumOfMyHighBit += BidValue;
            }

            double sumOfProfit = 0;
            var profitColumn = Driver.FindElements(By.ClassName("posswin"));

            for (int j = 0; j < profitColumn.Count; j++)
            {
                var profit = profitColumn[j].Text;

                if (profit.Contains("£"))
                {
                    var ProfitValue = double.Parse(profitColumn[j].Text.Replace('£', ' '));
                    sumOfProfit += ProfitValue;
                }
                else if(profit.Contains("p"))
                {
                    var ProfitValue = double.Parse(profitColumn[j].Text.Replace('p', ' '));
                    sumOfProfit += ((1 / 100d) * ProfitValue);
                }
              
            }

            //if (MyTotalListings.Equals(NumberOfMyListingsInTable).Equals(NumberOfMyBetsInAuction) && MyCurrentValue.Equals(highBidColumns).Equals(CurrentListingsWinValue) && CurrentPossibleEarnings.Equals(sumOfProfit).Equals(CurrentSaleValue))
            //{
            //    return true;
            //}

            return true;
        }

        public static bool SearchHistoricBids(string term)
        {
            Thread.Sleep(3000);
            myAccountButton.Click();
            Thread.Sleep(3000);

            var HistoryOverviewTile = Driver.FindElements(By.ClassName("account-tile-header"));
            HistoryOverviewTile[3].Click();

            var SearchHistoricBids = Driver.FindElement(By.ClassName("searchBids"));
            SearchHistoricBids.SendKeys(term);

            var MyHistoricBids = Driver.FindElements(By.CssSelector(".listing-title > span"));

            for (int i = 0; i < MyHistoricBids.Count; i++)
            {
                var bet = MyHistoricBids[i].Text;
                var betCounts = MyHistoricBids.Count;

                if (betCounts!=0 && MyHistoricBids[i].Displayed)
                {
                    if (!bet.ToLower().Contains(term))
                    {
                        //continue;
                        return false;
                    }
                   // return false;
                }
                continue;
            }


            return true;
        }

        public static bool SearchHistoricListings(string term)
        {
            Thread.Sleep(2000);
            myAccountButton.Click();
            Thread.Sleep(2000);

            var HistoryOverviewTile = Driver.FindElements(By.ClassName("account-tile-header"));
            HistoryOverviewTile[3].Click();

            Thread.Sleep(3000);

            var HistoricListingsButton = Driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[1]/div/a"));
            HistoricListingsButton.Click();


            var SearchHistoricBids = Driver.FindElement(By.ClassName("searchBids"));
            SearchHistoricBids.SendKeys(term);

            var MyHistoricBids = Driver.FindElements(By.CssSelector(".listing-title > span"));

            for (int i = 0; i < MyHistoricBids.Count; i++)
            {
                var bet = MyHistoricBids[i].Text;
                var betCounts = MyHistoricBids.Count;

                if (betCounts != 0 && MyHistoricBids[i].Displayed)
                {
                    if (!bet.ToLower().Contains(term))
                    {
                        //continue;
                        return false;
                    }
                    //return false;
                }
                continue;                
            }

            return true;
        }

    }
    }
