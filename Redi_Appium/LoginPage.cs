using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using Xunit;

namespace Redi_Appium
{
    public class LoginPage : IDisposable
    {
        private readonly AndroidDriver _driver;
        private readonly SwipeHandler _swipeHandler;

        public LoginPage()
        {
            var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");
            var driverOptions = new AppiumOptions()
            {
                AutomationName = AutomationName.AndroidUIAutomator2,
                PlatformName = "Android",
                DeviceName = "Samsung Galaxy S10",
            };

            driverOptions.AddAdditionalAppiumOption("appPackage", "com.dichoisolution.redi.qa");
            driverOptions.AddAdditionalAppiumOption("appActivity", "com.dichoisolution.redi.MainActivity");
            // NoReset assumes the app com.google.android is preinstalled on the emulator
            driverOptions.AddAdditionalAppiumOption("noReset", true);

            try
            {
                _driver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                throw new Exception("App cannot open", ex);
            }

            try
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            catch (Exception ex)
            {
                throw new Exception("Element not found", ex);
            }

            _swipeHandler = new SwipeHandler(_driver);
        }

        [Fact]
        public void OpenAppTest()
        {
            _driver.StartActivity("com.dichoisolution.redi.qa", "com.dichoisolution.redi.MainActivity");
            _swipeHandler.Swipe("LEFT", 3);
            _driver.FindElement(MobileBy.AccessibilityId("Next")).Click();
        }
        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}
