using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Components.Pages;
using NUnit.Framework;
using Tests.Base;

namespace Tests.PagesTests
{
    [TestFixture]
   public class AuctionHelpPageTests:BaseTest
    {
        [Test]
       
        public void Check_Accordion_Components()
        {
            Assert.IsTrue(AuctionHelpPage.AccordionComponent(), "Accordion component is not displayed correctly");
        }

        [Test]
        public void CheckSearchingFunctionality()
        {
            AuctionHelpPage.AuctionHelpSearch("auction");
        }
    }
}
