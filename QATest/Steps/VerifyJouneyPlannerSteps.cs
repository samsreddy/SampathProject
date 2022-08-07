using System;
using TechTalk.SpecFlow;
using QATest.Classes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QATest.GlobalHelperClasses;
using OpenQA.Selenium.Support.UI;

using (IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory)) ;


namespace QATest.Steps
{
    [Binding]
    public class VerifyJouneyPlannerSteps
    {
        private readonly BrowserDriver _webDriverContext;
        public string defaultDomin = TestContext.Parameters["siteURL"];
        public string SystemArtifcatsDirectory = Environment.GetEnvironmentVariable("SYSTEM_ARTIFACTDIRECTORY");


        public VerifyJouneyPlannerSteps(BrowserDriver webDriverContext)
        {
            _webDriverContext = webDriverContext;
        }

        [Given(@"I open the URL using a chosen browser")]
        public void GivenIOpenTheURLUsingAChosenBrowser()
        {
            Assert.IsNotNull(_webDriverContext);
            _webDriverContext.driver.Navigate().GoToUrl("https://www.tfl.gov.uk");
        }

        [Given(@"I have accepted cookies")]
        public void GivenIHaveAcceptedCookies()
        {
            _webDriverContext.driver.ClickCookies();
            Assert.IsTrue(true);
        }

        [Then(@"I can check Plan a journey section is present")]
        public void ThenICanCheckPlanAJourneySectionIsPresent()
        {
            IWebElement planAJourney = _webDriverContext.driver.FindElements(By.XPath("//li[@class='plan-journey']"))[0];
            Assert.IsNotNull(planAJourney);
        }

        [When(@"I enter ""(.*)"" into the From Journey Field")]
        public void WhenIEnterIntoTheFromJourneyField(string fromJouney)
        {
            IWebElement fromJourneyTextBox = _webDriverContext.driver.FindElements(By.XPath("//input[@id='InputFrom']"))[0];
            fromJourneyTextBox.SendKeys(fromJouney + Keys.Tab);
            Assert.IsTrue(true);
        }

        [When(@"I enter ""(.*)"" into the To Journey Field")]
        public void WhenIEnterIntoTheToJourneyField(string toJouney)
        {
            IWebElement toJourneyTextBox = _webDriverContext.driver.FindElements(By.XPath("//input[@id='InputTo']"))[0];
            toJourneyTextBox.SendKeys(toJouney + Keys.Tab);
            Assert.IsTrue(true);
        }

        [When(@"I click on the Plan my journey Button")]
        [When(@"I click on the Update jouney button")]
        public void WhenIClickOnThePlanMyJourneyButton()
        {
            WebDriverWait wait = new(_webDriverContext.driver, TimeSpan.FromSeconds(3));
            IWebElement planAJourneyButton = _webDriverContext.driver.FindElement(By.XPath("//input[@id='plan-journey-button']"));
            Assert.IsNotNull(planAJourneyButton);            
            planAJourneyButton.Click();
        }

        [Then(@"I should see the Journey results")]
        [Then(@"I should see the amended Journey results")]
        public void ThenIShouldSeeTheJourneyResults()
        {
            WebDriverWait wait = new(_webDriverContext.driver, TimeSpan.FromSeconds(5));
            IWebElement jouneyResults = _webDriverContext.driver.FindElements(By.XPath("//span[@class='jp-results-headline']"))[0];
            Assert.IsNotNull(jouneyResults);
        }

        [Then(@"I should see the error message displayed against the fields")]
        public void ThenIShouldSeeTheErrorMessageDisplayedAgainstTheFields()
        {
            IWebElement inputFromFieldError = _webDriverContext.driver.FindElements(By.XPath("//span[@id='InputFrom-error']"))[0];
            Assert.IsNotNull(inputFromFieldError);
            IWebElement inputToFieldError = _webDriverContext.driver.FindElements(By.XPath("//span[@id='InputTo-error']"))[0];
            Assert.IsNotNull(inputToFieldError);
        }

        [Then(@"I should see the field validation error message displayed on the screen")]
        public void ThenIShouldSeeTheFieldValidationErrorMessageDisplayedOnTheScreen()
        {
            WebDriverWait wait = new(_webDriverContext.driver, TimeSpan.FromSeconds(2));
            IWebElement fieldValidationError = _webDriverContext.driver.FindElement(By.XPath("//li[@class='field-validation-error']"));
            var validationText = fieldValidationError.Text;
            Assert.AreEqual("Sorry, we can't find a journey matching your criteria", validationText);
        }

        [When(@"I click the Edit Jouney Button")]
        public void WhenIClickTheEditJouneyButton()
        {
            IWebElement editJouney = _webDriverContext.driver.FindElements(By.XPath("//a[@class='edit-journey']//span"))[0];
            Assert.IsNotNull(editJouney);
            editJouney.Click();
        }

        [Then(@"I should see the edit journey page")]
        public void ThenIShouldSeeTheEditJourneyPage()
        {
            IWebElement editingPage= _webDriverContext.driver.FindElements(By.XPath("//div[@class='editing']"))[0];
            Assert.IsNotNull(editingPage);
        }

        [When(@"I amend the jouney details")]
        public void WhenIAmendTheJouneyDetails()
        {
            IWebElement switchButton = _webDriverContext.driver.FindElements(By.XPath("//a[@class='switch-button hide-text']"))[0];
            Assert.IsNotNull(switchButton);
            switchButton.Click();
        }

        [When(@"I click on the link leaving change time link")]
        public void WhenIClickOnTheLinkLeavingChangeTimeLink()
        {
            IWebElement changeTimeLink = _webDriverContext.driver.FindElements(By.XPath("//a[@class='change-departure-time']"))[0];
            Assert.IsNotNull(changeTimeLink);
            changeTimeLink.Click();
        }

        [Then(@"I should see Arriving option for the plan a journey")]
        public void ThenIShouldSeeArrivingOptionForThePlanAJourney()
        {
            IWebElement arrivingOption = _webDriverContext.driver.FindElements(By.XPath("//label[@for='arriving']"))[0];
            Assert.IsNotNull(arrivingOption);
            arrivingOption.Click();
        }



        [Then(@"I should close the browser")]
        public void ThenIShouldCloseTheBrowser()
        {
            _webDriverContext.driver.Quit();
        }

    }
}
