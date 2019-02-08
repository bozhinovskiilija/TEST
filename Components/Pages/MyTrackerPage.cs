using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;
using Components.Utilities;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions; //using this expected condition instead of OpenQA.Selenium.Support.UI

namespace Components.Pages
{
    public class MyTrackerPage : WebDriverFactory
    {
        static IWebElement myTrackerButton = Driver.FindElement(By.Id("nav-item-live-tracker"));

        static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

        public static bool ListAllBetsIHaveBiddedFor()
        {
            // wait.Until(ExpectedConditions.ElementToBeClickable(myTrackerButton));
            Thread.Sleep(5000);
            myTrackerButton.Click();

            var BidForButton = Driver.FindElement(By.XPath("//*[@id='BiddedOn']"));
            var WatchingButton = Driver.FindElement(By.XPath("//*[@id='Watched']"));

            WatchingButton.Click();
            BidForButton.Click();
            
            //scroll down the page for displaying all tiles(this is important because you can't check the tile details if it is not diaplyed on the page )
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            var tiles = Driver.FindElements(By.ClassName("card-slot"));

            for (int i = 0; i < tiles.Count; i++)
            {
                var tileID = tiles[i].GetAttribute("id").ParseIntFromString();

                var myBid = tiles[i].GetAttribute("data-i-have-bids");

                if (myBid == "true")
                {                   
                    continue;
                }
                return false;              
            }

            return true;
        }

        public static bool ListAllBetsIHaveWatched()
        {
            //wait.Until(ExpectedConditions.ElementToBeClickable(myTrackerButton));
            Thread.Sleep(5000);
            myTrackerButton.Click();
          
            //scroll down the page for displaying all tiles(this is important because you can't check the tile details if it is not diaplyed on the page )
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Thread.Sleep(2000);

            var tiles = Driver.FindElements(By.ClassName("card-slot"));

            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles.Count!=0)
                {
                    var tileID = tiles[i].GetAttribute("id").ParseIntFromString();
                    var tileWatchedIcon = tiles[i].FindElement(By.ClassName("stop-watching-bid"));

                    if (!tileWatchedIcon.Displayed)
                    {
                        //continue;
                        return false;
                    }                  
                }             
            }
            return true;
        }

        public static bool UnwathLotFromTracker()
        {
            Thread.Sleep(5000);
            myTrackerButton.Click();

            var tiles = Driver.FindElements(By.ClassName("card-slot"));

            if (tiles.Count != 0)
            {
                for (int i = 0; i < tiles.Count;i++)
                {
                    var tileID = tiles[i].GetAttribute("id").ParseIntFromString();
                    var tileWatchedIcon = tiles[i].FindElement(By.CssSelector(".card-up.reserve-state.reserve-state > p > a"));
                    tileWatchedIcon.Click();

                    Thread.Sleep(5000);

                    for (int j = 1; j < tiles.Count; j++)
                    {
                        if (tiles[j].Displayed)
                        {
                            var tileID2 = tiles[j].GetAttribute("id").ParseIntFromString();

                            if (!tileID2.Equals(tileID))
                            {
                                return true;
                            }
                            return false;
                       }
                    }

                    break;
                }               
            }
            return true;
        }

            public static bool SortBy_WinningBids()
            {
            Thread.Sleep(3000);
            myTrackerButton.Click();

            var BidForButton = Driver.FindElement(By.XPath("//*[@id='BiddedOn']"));         
            var WatchingButton = Driver.FindElement(By.XPath("//*[@id='Watched']"));

            WatchingButton.Click();
            BidForButton.Click();

            IWebElement SortBy = Driver.FindElement(By.CssSelector(".trackerSort .colorful-select.dropdown-dark.trackerSortDropdown .select-dropdown"));
            SortBy.Click();
            Thread.Sleep(2000);
            var SortByUl = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark.trackerSortDropdown .dropdown-content.select-dropdown>li"))[3];
            SortByUl.Click();
           
            Thread.Sleep(2000);
                       
            var CardState = Driver.FindElements(By.ClassName("bid-card-state"));
           
            for (int i = 0; i < CardState.Count-1; i++)
            {
                var state = CardState[i].Text;

                if (CardState[i].Text.CompareTo(CardState[i+1].Text)<0)
                {
                    // sorted = false;
                    return false;                  
                }

                continue;
            }

            return true;
        }


        public static bool SortBy_EndingSoon()
        {
            Thread.Sleep(3000);
            myTrackerButton.Click();

            var BidForButton = Driver.FindElement(By.XPath("//*[@id='BiddedOn']"));
            var WatchingButton = Driver.FindElement(By.XPath("//*[@id='Watched']"));

            WatchingButton.Click();
            BidForButton.Click();

            IWebElement SortBy = Driver.FindElement(By.CssSelector(".trackerSort .colorful-select.dropdown-dark.trackerSortDropdown .select-dropdown"));
            SortBy.Click();
            var SortByUl = Driver.FindElements(By.CssSelector(".colorful-select.dropdown-dark.trackerSortDropdown .dropdown-content.select-dropdown>li"))[0];
            SortByUl.Click();

            var elements = Driver.FindElements(By.ClassName("tracker-time-left"));

            for (int i=0; i<elements.Count-1; i++)
            {
                Thread.Sleep(2000);

                var dateTimeAttribute = elements[i].GetAttribute("data-auction-end-date");
                DateTime endDate = DateTime.Parse(dateTimeAttribute);

                var dateTimeAttribute2 = elements[i+1].GetAttribute("data-auction-end-date");
                DateTime endDate2 = DateTime.Parse(dateTimeAttribute2);

                if (endDate.CompareTo(endDate2) < 0 || endDate.CompareTo(endDate2) == 0)
                {
                    continue;
                }
                return false;
            }

            return true;
        }

    }
}

