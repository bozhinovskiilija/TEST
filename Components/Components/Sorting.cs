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
    public class Sorting:WebDriverFactory
    {
       static IWebElement MaxWinSortButton = Driver.FindElement(By.Id("Profit"));
       static IWebElement DescriptionSortButton = Driver.FindElement(By.Id("BetDescription"));
       static IWebElement HighBidSort = Driver.FindElement(By.Id("HighBid"));

       static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        public static void SortByMaxWin_Descending()
        {
           MaxWinSortButton.Click();
        }
     
        public static void SortByDescription_Descending()
        {
            DescriptionSortButton.Click();
        }

        public static void SortByHighBid_Descending()
        {
            HighBidSort.Click();
        }

        public static bool CheckSortTableByMaxWin_Descending()
        {

            var elements = wait.Until((d) =>
            {
                return d.FindElements(By.Id("ar-profit"));
            });           
           
            var array = new List<float>(20);

            for (int i = 0; i < elements.Count; i++)
            {
                array.Add(float.Parse((elements[i]).Text.Replace("£","")));
            }

            bool sorted = true;

            for (int i = 0; i < array.Count-1; i++)
            {
                if (array[i] < array[i + 1])
                {
                    sorted = false;
                    break;
                }
            }

            return sorted;
        }

        public static bool CheckSortTableByDescription_Descending()
        {
            var elements = wait.Until((d) =>
            {
                return d.FindElements(By.Id("event-detail"));
            });
           
            var array = new List<String>(20);

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Displayed)
                {
                    var words = (elements[i].Text.Split());
                    array.Add(words[0].ToLower());
                }           
            }

            bool sorted = true;

            for (int i = 0; i < array.Count - 1; i++)
            {
                if (array[i].CompareTo(array[i + 1]) < 0)
                {
                    sorted = false;
                    break;
                }
            }

            return sorted;
        }

        public static bool CheckSortTableByHighBid_Descending()
        {
            var elements = wait.Until((d) =>
            {
                return d.FindElements(By.Id("ar-hi-bid"));
            });
            
            var array = new List<float>(20);

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Displayed)
                {
                    array.Add(float.Parse((elements[i]).Text.Replace("£", "")));
                }               
            }

            bool sorted = true;

            for (int i = 0; i < array.Count - 1; i++)
            {
                if (array[i] < array[i + 1])
                {
                    sorted = false;
                    break;
                }
            }
            return sorted;
        }

    }
}
