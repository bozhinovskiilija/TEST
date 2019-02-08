using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;

namespace Components.Components
{
   public class ActiveLinks:WebDriverFactory
    {
        static IWebElement myTrackerButton = Driver.FindElement(By.Id("nav-item-live-tracker"));
        static IWebElement myAccountButton = Driver.FindElement(By.Id("nav-item-my-auction"));

        public static bool MyTrackerButton_IsDisplayed
        {
            get
            {
                try
                {
                    return myTrackerButton.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static bool MyAccountButton_IsDisplayed
        {
            get
            {
                try
                {
                    return myAccountButton.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }
    }
}
