using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomationCore;
using OpenQA.Selenium;
using Components.TestDataAccess;


namespace Components.Components
{
   public class Login:WebDriverFactory
    {
        static IWebElement loginInput = Driver.FindElement(By.Id("inp-username"));
        static IWebElement passwordInput = Driver.FindElement(By.Id("inp-password"));
        static IWebElement loginButton = Driver.FindElement(By.Id("btn-login"));
        static IWebElement logout = Driver.FindElement(By.Id("btn-logout"));



        //public static void LoginComponent(string username, string password)
        //{
        //    loginInput.SendKeys(username);
        //    passwordInput.SendKeys(password);
        //    loginButton.Click();
        //}

        public static void LoginComponent(string testName)
        {
            var userData = ExcelDataAccess.GetTestData(testName);
            loginInput.SendKeys(userData.Username);
            passwordInput.SendKeys(userData.Password);
            loginButton.Click();
        }




        public static void SetImplicitWait()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static bool LogoutButton_IsDisplayed
        {
            get
            {
               try
                {
                    return logout.Displayed;                   
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }
    }
}
