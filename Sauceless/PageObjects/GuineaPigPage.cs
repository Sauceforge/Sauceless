using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Sauceless.PageObjects.Base;
using Sauceless.Util;
using SeleniumExtras.PageObjects;

namespace Sauceless.PageObjects;

public class GuineaPigPage : PageObjectBase {
    public GuineaPigPage(WebDriver driver, string urlRoot)
        : base(urlRoot + "test/guinea-pig", "GuineaPig", "I am a page title - Sauce Labs") {
        GetPage(driver);
        PageFactory.InitElements(driver, this);
    }

    internal GuineaPigPage ClickLink(WebDriver driver) {
        var link = driver.FindElement(By.Id(SaucelessConstants.Links.IAmALink));
        link.Click();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(d => d.Url.Contains("guinea-pig2"));
        return this;
    }

    internal static IWebElement GetField(WebDriver driver, string fieldId) => driver.FindElement(By.Id(fieldId));

    internal static string GetUserAgent(WebDriver driver) 
        => driver.FindElement(By.Id(SaucelessConstants.Fields.UserAgent)).Text;

    internal GuineaPigPage TypeField(WebDriver driver, string fieldId, string data) {
        var element = GetField(driver, fieldId);
        element.Clear();
        element.SendKeys(data);
        return this;
    }

    internal static void SubmitForm(WebDriver driver) {
        var submitButton = driver.FindElement(By.Id("submit"));
        submitButton.Click();
    }
}