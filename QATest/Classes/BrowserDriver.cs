using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace QATest.Classes
{
    public class BrowserDriver
    {
        private readonly IWebDriver _webDriver;
        private readonly string _browserVariable;

        public BrowserDriver()
        {
            //_webDriver = CreateWebDriver();
            _browserVariable = Environment.GetEnvironmentVariable("browser", EnvironmentVariableTarget.Process);
        }

        //public IWebDriver Current => _webDriver;
        public IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        private IWebDriver CreateWebDriver()
        {
            try
            {
                return _browserVariable switch
                {
                    "chrome" => CreateChromeWebDriver(),
                    "edge" => CreateEdgeWebDriver(),
                    "firefox" => CreateFirefoxWebDriver(),
                    _ => CreateChromeWebDriver()
                };
            }
            catch (Exception ex)
            {
                if (_browserVariable != "chrome" || _browserVariable != "edge" || _browserVariable != "firefox")
                {
                    Console.WriteLine("Pipeline submitted an invalid browser parameter");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                else
                {
                    Console.WriteLine("An error has occurred when trying to create a WebDriver");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                return null;
            }
        }

        private static IWebDriver CreateChromeWebDriver()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();

            ChromeOptions chromeOptions = new() { };

            chromeOptions.AddArguments("start-maximized"); // https://stackoverflow.com/a/26283818/1689770
            chromeOptions.AddArguments("enable-automation"); // https://stackoverflow.com/a/43840128/1689770
            chromeOptions.AddArguments("--window-size=1280,800"); // https://stackoverflow.com/questions/47061662/selenium-tests-fail-against-headless-chrome
            chromeOptions.AddArguments("--no-sandbox"); //https://stackoverflow.com/a/50725918/1689770
            chromeOptions.AddArguments("--disable-infobars"); //https://stackoverflow.com/a/43840128/1689770
            chromeOptions.AddArguments("--disable-dev-shm-usage"); //https://stackoverflow.com/a/50725918/1689770
            chromeOptions.AddArguments("--disable-browser-side-navigation"); //https://stackoverflow.com/a/49123152/1689770
            chromeOptions.AddArguments("--disable-gpu"); //https://stackoverflow.com/questions/51959986/how-to-solve-selenium-chromedriver-timed-out-receiving-message-from-renderer-exc

            ChromeDriver chromeDriver = new(chromeDriverService, chromeOptions);

            return chromeDriver;
        }

        private static IWebDriver CreateEdgeWebDriver()
        {
            EdgeDriverService edgeDriverService = EdgeDriverService.CreateDefaultService();

            EdgeOptions edgeOptions = new()
            {
            };

            EdgeDriver edgeDriver = new(edgeDriverService, edgeOptions);

            return edgeDriver;
        }

        private static IWebDriver CreateFirefoxWebDriver()
        {
            FirefoxDriverService firefoxDriverService = FirefoxDriverService.CreateDefaultService();

            FirefoxDriver firefoxDriver = new(firefoxDriverService);

            return firefoxDriver;
        }
    }
}
