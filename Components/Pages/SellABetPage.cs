using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;

namespace Components.Pages
{
    public class SellABetPage:WebDriverFactory
    {
         static IWebElement ListaABetButton = Driver.FindElement(By.Id("nav-item-list-a-bet"));
         static IWebElement MyTrackerButton = Driver.FindElement(By.Id("nav-item-live-tracker"));

        public static bool ListBetInAuction()
        {
            ListaABetButton.Click();
            Thread.Sleep(2000);

            var ListForAuction = Driver.FindElements(By.ClassName("listing-row"));//all bets in the table

            //var ListForAuctionButton = Driver.FindElements(By.ClassName("listing-row"))

            if (ListForAuction.Count > 0)
            {
                var betDetails = ListForAuction[0].Text;
                var onlyTeams = betDetails.Split(',')[0];

                var button = ListForAuction[0].FindElement(By.ClassName("list-for-auction"));
                button.Click();

                    Thread.Sleep(2000);

                    var AuctionDurationButton = Driver.FindElement(By.Id("selectedAuctionDuration"));

                    AuctionDurationButton.Click();
                    Thread.Sleep(2000);
                    var chooseTime = Driver.FindElements(By.CssSelector(".dropdown .dropdown-menu.dropdown-dark>a"))[3];
                    chooseTime.Click();

                    var confirmButton = Driver.FindElement(By.Id("btn-confirm-list"));
                    confirmButton.Click();


                Thread.Sleep(3000);

                var myTrackerButton = Driver.FindElement(By.Id("nav-item-live-tracker"));
                Thread.Sleep(4000);

                myTrackerButton.Click();

                var WatchingButton = Driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[4]/div"));
                WatchingButton.Click();
                var SellingButton = Driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[3]/div"));
                SellingButton.Click();

                Thread.Sleep(3000);

                    var SellingSlotCards = Driver.FindElements(By.CssSelector(".card-rotating.effect__click"));

                    for (int j = 0; j < SellingSlotCards.Count; j++)
                    {
                        var detail = SellingSlotCards[j].FindElement(By.ClassName("bid-card-bet"));

                        var resultString = detail.Text.Split(',')[0];



                    if (onlyTeams != resultString)
                    {
                        
                        continue;

                    }

                    return true;
                }


                }


            

            return false;

        }

    }
}
