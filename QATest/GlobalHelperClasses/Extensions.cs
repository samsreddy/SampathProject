using OpenQA.Selenium;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace QATest.GlobalHelperClasses
{
    public static class Extensions
    {
        public static void ClickCookies(this IWebDriver d)
        {
            IWebElement cookieButton = d.FindElements(By.XPath("//button[@id='CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll']"))[0];

            if (cookieButton.Displayed)
            {
                cookieButton.Click();

                if (d.FindElement(By.XPath("//div[@id='cb-confirmedSettings']")).Displayed)
                {
                    d.FindElement(By.XPath("//div[@id='cb-confirmedSettings']//button")).Click();
                }

            }
        }

        public static void Shuffledown(this IWebElement d, int shuffleDown)
        {
            int i = 0;

            while (i <= shuffleDown)
            {
                d.SendKeys(Keys.Down);

                i++;
            }
        }

        public static string XPathFromName(this string componentName)
        {
            string retVal = componentName.ToLower() switch
            {
                "bill search" => "//div[@id='billSearch']",
                "committee search" => "//form[@id='ctteesearch']",
                "date range option" => "//select[@id='DateRangeOption']",
                "committee transcript search" => "//form[@data-formname='ctteetranscriptsearch']",
                "bill results count" => "//p[@id='billsResultCount']",
                "bill search filter" => "//form[@id='billFilter']",
                "bill results list" => "//div[@id='billResult1']",
                "header" => "//div[contains(concat(' ',normalize-space(@class),' '),'white-banner')]",
                "v & m header" or "wq header" => "//div[contains(concat(' ',normalize-space(@class),' '),'yap-page-header')]",
                "vote msp filter" => "//select[@id='voteMSP']",
                "vote keyword filter" => "//input[@id='qryVoteKeywordTextBox']",
                "vote reference filter" => "//input[@id='qryVoteRefTextBox']",
                "motion msp filter" => "//select[@id='motionMSP']",
                "motion keyword filter" => "//input[@id='qryKeywordTextBox']",
                "motion reference filter" => "//input[@id='qryMotionRefTextBox']",
                "msp filter" => "//select[@id='msp']",
                "keyword filter" => "//input[@id='qryKeywordTextBox']",
                "question reference filter" => "//input[@id='qryQRefTextBox']",
                "msp list" => "//div[@class='new-search-filter-group__results full-width']",
                _ => componentName,
            };
            return retVal;
        }

        public static IWebElement GetParent(this IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }

        public static Dictionary<string, bool> CheckAccordionsWorks(this IWebDriver d)
        {
            List<IWebElement> accordionButtons = d.FindElements(By.XPath("//button[contains(concat(' ',normalize-space(@class),' '),'new-button new-button--round new-button--large js--accordion')]")).ToList();

            Dictionary<string, bool> dcResults = new();

            d.Manage().Window.Maximize();

            foreach (IWebElement btn in accordionButtons)
            {
                //get target accordion
                string trgt = btn.GetAttribute("data-accordion");

                //get target accordion control
                IWebElement trgtDiv = d.FindElement(By.XPath("//div[@id='" + trgt + "']"));

                if (trgtDiv != null)
                {
                    if (!trgtDiv.Displayed)
                    {
                        btn.Click();
                    }

                    trgtDiv = d.FindElement(By.XPath("//div[@id='" + trgt + "']"));

                    //check videos generate the required iframe
                    List<IWebElement> videoContainers = trgtDiv.FindElements(By.XPath(".//div[@class='video-container']")).ToList();

                    if (videoContainers.Count > 0)
                    {
                        for (int i = 0; i < videoContainers.Count; i++)
                        {
                            IWebElement vc = videoContainers[i];

                            IWebElement videoLink = vc.FindElement(By.XPath("./img"));

                            if (videoLink.Displayed)
                            {
                                videoLink.Click();

                                //get wrapper div again
                                IWebElement wrapper = d.FindElement(By.XPath("//div[@id='" + trgt + "']"));

                                IWebElement videoCon = wrapper.FindElement(By.XPath("//div[@class='video-container is-active']"));

                                //video-container is-active
                                IWebElement iFrm = videoCon.FindElements(By.XPath("./iframe")).FirstOrDefault();

                                if (iFrm == null)
                                {
                                    dcResults.Add("video play check", false);
                                }
                            }
                        }
                    }

                    dcResults.Add(btn.Text, trgtDiv.Displayed);
                }
            }

            return dcResults;
        }

        /// <summary>
        /// Check it is a real success code, not a redirect to the 404 page
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool IsRealSuccessCode(this HttpResponseMessage d)
        {
            bool success = d.IsSuccessStatusCode;

            if (success)
            {
                if (d.RequestMessage.RequestUri.AbsoluteUri.Contains("404"))
                {
                    success = false;
                }
            }

            return success;
        }        
    }
}