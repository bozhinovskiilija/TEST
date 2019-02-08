using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using AutomationCore;
using OpenQA.Selenium;

using Tesseract;

namespace Components.Pages
{
    public class AuctionHelpPage : WebDriverFactory
    {





        public static bool AccordionComponent()
        {
            Thread.Sleep(3000);
            var helpButton = Driver.FindElement(By.CssSelector("#auctionNavigationBar > ul > li:nth-child(8) > a"));
            helpButton.Click();
            Thread.Sleep(3000);

            var modal = Driver.FindElement(By.CssSelector(".modal-body.help-modal-body"));

            if (modal.Displayed)
            {
                var accordionHeaders = Driver.FindElements(By.CssSelector(".card-header > a"));

                for (int i = 0; i < accordionHeaders.Count; i++)
                {
                    var dataToggle = accordionHeaders[i].GetAttribute("aria-expanded");

                    if (dataToggle == "false")
                    {
                        accordionHeaders[i].Click();
                    }
                    var listItems = Driver.FindElements(By.CssSelector(".list-group.modal-help-list .list-group-item"));

                    for (int j = 0; j < listItems.Count; j++)
                    {
                        //if list-item not visible click on header !!!
                        Thread.Sleep(1000);
                        if (listItems[j].Displayed)
                        {
                            listItems[j].Click();
                        }
                        else
                        {
                            accordionHeaders[i + 1].Click();
                            i++;
                        }
                        var textOfTabPane = Driver.FindElement(By.CssSelector(".tab-pane.active")).Text;
                        var clearTabtext = Regex.Replace(textOfTabPane, @"\s+", "").ToLower();

                        var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                        string text;
                        using (var streamReader = new StreamReader(outPutDirectory + "\\file2.txt", Encoding.UTF8))
                        {
                            text = streamReader.ReadToEnd();
                           
                            
                            var clearText = Regex.Replace(text, @"\s+", "").ToLower();

                            if (clearText.Contains(clearTabtext))
                            {
                                continue;
                            }

                            return false;
                        }

                    }
                }
            }

            return true;
        }



        public static bool AuctionHelpSearch(string term)
        {
            Thread.Sleep(3000);
            var helpButton = Driver.FindElement(By.CssSelector("#auctionNavigationBar > ul > li:nth-child(8) > a"));
            helpButton.Click();
            Thread.Sleep(3000);


            var searchBox = Driver.FindElement(By.Id("helpSearchBox"));
            searchBox.SendKeys("auction");


            var accordionHeaders = Driver.FindElements(By.CssSelector(".card-header > a"));

            for (int i = 0; i < accordionHeaders.Count; i++)
            {
                var DataToggleAttribute = accordionHeaders[i].GetAttribute("data-toggle");

                var titleOfHeader = accordionHeaders[i].FindElement(By.TagName("h6"));

                var styleAttribute = titleOfHeader.GetAttribute("style");

               

                string headerText = accordionHeaders[i].Text;

                if (DataToggleAttribute == "collapse" && styleAttribute==null)
                {
                    accordionHeaders[i].Click();
                }
                var listItems = Driver.FindElements(By.CssSelector(".list-group.modal-help-list .list-group-item"));

                for (int j = 0; j < listItems.Count; j++)
                {
                    //if list-item not visible click on header !!!

                    Thread.Sleep(2000);
                    if (listItems[j].Displayed)
                    {
                        listItems[j].Click();
                    }

                    else 
                    {
                        accordionHeaders[i+1].Click();
                        i++;
                    }
                    var textOfTabPane = Driver.FindElement(By.CssSelector(".tab-pane.active")).Text;
                    var clearTabtext = Regex.Replace(textOfTabPane, @"\s+", "").ToLower();


                    if (clearTabtext.Contains(term))
                    {
                        continue;
                    }

                    return false;
                } 


            }
            return true;
        }

    }
}

