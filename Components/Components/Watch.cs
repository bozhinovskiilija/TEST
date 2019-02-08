using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions; //using this expected condition instead of OpenQA.Selenium.Support.UI


namespace Components.Components
{
    public class Watch:WebDriverFactory
    {
        private static IWebElement myTrackerButton = Driver.FindElement(By.Id("nav-item-live-tracker"));

        static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        public static bool StartWatchingAuction()
        {
            var rows = Driver.FindElements(By.CssSelector("a.start-watching-auction.unwatched"));
            if (rows.Count > 1)
            {
               for(int j = 0; j < rows.Count; j++)
                {
                    if (rows[j].Displayed)
                    {
                        var eye = rows[0];
                        var innerElementI = eye.FindElements(By.CssSelector("i.fa"));
                        bool isParsingSuccesfull = Int32.TryParse(innerElementI.First().GetAttribute("id").Split('-')[1], out int id);

                        eye.Click();

                        // wait.Until(ExpectedConditions.ElementToBeClickable(myTrackerButton));
                        Thread.Sleep(2000);

                        myTrackerButton.Click();

                        Thread.Sleep(5000);

                        var tiles = Driver.FindElements(By.CssSelector(".card-rotating.effect__click"));

                        for (int i = 0; i < tiles.Count; i++)
                        {
                            var tileID = tiles[i].GetAttribute("id");

                            var tileIDNumber = string.Join("", tileID.ToCharArray().Where(char.IsDigit));

                            int onlyNumber = Int32.Parse(tileIDNumber);

                            if (onlyNumber.Equals(id))
                            {
                                return true;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                } 
              
            }
            return false;
        }       
    }
}
