using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Components.Components
{
    public class Searching : WebDriverFactory
    {
        static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        public static void SearchComponent(string search_term)
        {
           
            IWebElement SearchInputField = wait.Until((d) =>
            {
                return d.FindElement(By.Id("inpFreeText"));
            });
            SearchInputField.SendKeys(search_term);
            SearchInputField.SendKeys(Keys.Enter);   
        }

        public static bool IterateTable(string term)
        {
            IWebElement Chip = wait.Until((d) =>
            {
                return d.FindElement(By.CssSelector(".chips-initial .chip"));
            });
                     
            var textOfChip = Chip.Text.ToLower();
            var elements = Driver.FindElements(By.ClassName("lotSummaryName"));

            Thread.Sleep(2000);

            foreach (var element in elements)
            {             
                var elementText = element.Text;

                if (textOfChip.Equals(term.ToLower()) && elementText.Contains(term))
                {
                    continue;
                }
                element.Click();
                var tabPane = Driver.FindElement(By.ClassName("tab-pane"));
                var tabpaneText = tabPane.Text;

                if (!tabpaneText.Contains(term) && !textOfChip.Equals(term.ToLower()))
                {
                    return false;
                }
               
            }

            return true;
        }

       
    }
}
