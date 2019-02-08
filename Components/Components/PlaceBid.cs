using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions; //using this expected condition instead of OpenQA.Selenium.Support.UI

namespace Components.Components
{
    public class PlaceBid : WebDriverFactory
    {
        private static IWebElement BidConfirmBid = Driver.FindElement(By.Id("btn-confirm-bid"));
        private static IWebElement myTrackerButton = Driver.FindElement(By.Id("nnav-item-live-tracker"));
        static  WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        public static bool MakeABid()
        {
           var BetsPerPageDropdown = Driver.FindElement(By.XPath("//*[@id='auction-page']/div[1]/div[5]/div/input"));
           wait.Until(ExpectedConditions.ElementToBeClickable(BetsPerPageDropdown));
           BetsPerPageDropdown.Click();         

           var NumberOfBetsItems = Driver.FindElements(By.CssSelector(".auction-row-select .dropdown-content.select-dropdown>li"))[4];
         
           wait.Until(ExpectedConditions.ElementToBeClickable(NumberOfBetsItems));
           NumberOfBetsItems.Click();                                                      //child tag         
           
           Thread.Sleep(3000);

           var rows = Driver.FindElements(By.ClassName("auction-row"));

           for (int i = 0; i<rows.Count;i++)
           {
                if (rows[i].Displayed)
                {
                    bool betId = Int32.TryParse(rows[i].GetAttribute("id").Split('-')[1], out int id);

                    if (rows[i].GetAttribute("data-high-bid-is-mine") == "false" && rows[i].GetAttribute("data-lot-is-mine")=="false") //if i have already bidded for that lot
                    {
                        var price = rows[i].FindElement(By.CssSelector(".ar-hi-bid"));

                        var highest = price.Text.Replace("£", "");
                        float highestBid = float.Parse(highest);

                        if (highestBid < 10000)
                        {
                            float newBid = highestBid + 1;

                            var bidButton = rows[i].FindElement(By.ClassName("auction-bid-row"));
                            bidButton.Click();
                            IWebElement BidModalWindow = wait.Until((d) => { return d.FindElement(By.Id("bidModal")); });

                            IWebElement ModalInputField = Driver.FindElement(By.Id("userSubmittedBid"));                           
                            wait.Until(ExpectedConditions.ElementToBeClickable(ModalInputField));
                            ModalInputField.Clear();
                            ModalInputField.SendKeys(newBid.ToString());
                        
                            BidConfirmBid.Click();

                            Thread.Sleep(5000);
                                
                            myTrackerButton.Click();

                            var WatchedButton = Driver.FindElement(By.Id("Watched"));
                            WatchedButton.Click();
                            var IHaveBiddedForButton = Driver.FindElement(By.Id("BiddedOn"));
                            IHaveBiddedForButton.Click();

                            var tiles = Driver.FindElements(By.CssSelector(".card-rotating.effect__click"));

                            for (int j = 0; j < tiles.Count; j++)
                            {
                                var tileID = tiles[j].GetAttribute("id");

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
                            Console.WriteLine("Bid was successfully created, high bid price was not updated");
                            return false;
                        }
                    }
                }
                else
                {
                    continue;
                }
                         
            }

            return false;
        }
    }
}





