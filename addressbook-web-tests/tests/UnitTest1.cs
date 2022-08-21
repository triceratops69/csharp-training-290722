using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace addressbook_web_tests.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] s = new string[] { "I", "want", "to", "sleep", "rave" };
            string[] mass = new string[] { "1", "2", "3", "4" };
            int i = 0;
            foreach (string element in mass)
            {
                System.Console.Out.WriteLine(element);
                System.Console.Out.WriteLine(s[i]);
                i++;
            }

            /*
            for (int i=0; i < s.Length; i++)
            {
                System.Console.Out.WriteLine(s[i]);
            }

            foreach (string element in s)
            {
                System.Console.Out.WriteLine(element);
            }

            IWebDriver driver = null;
            int attempt = 0;

            while (/*driver.FindElements(By.Id("test")).Count == 0 &&*//* attempt < 20)
            {
                if (attempt % 2 == 0)
                {
                    if (attempt % 4 == 0)
                    {
                        System.Threading.Thread.Sleep(10);
                        System.Console.Beep();
                    }
                    System.Threading.Thread.Sleep(100);
                }
                else
                {
                    System.Threading.Thread.Sleep(300);
                }
                
                System.Console.Beep();
                attempt++;
            }
            */
            /*
            do
            {
                System.Threading.Thread.Sleep(1000);
                attempt++;
            } while (driver.FindElements(By.Id("test")).Count == 0 && attempt < 60);
            */

        }
    }
}