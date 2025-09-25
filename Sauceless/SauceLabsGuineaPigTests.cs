using OpenQA.Selenium.Chrome;
using Sauceless.PageObjects;
using Sauceless.Util;
using Shouldly;

namespace Sauceless;

public class SauceLabsGuineaPigTests : IDisposable 
{
    private readonly ChromeDriver _driver;

    public SauceLabsGuineaPigTests() {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new"); // run without opening UI, remove if you want UI
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        _driver = new ChromeDriver(options);
        _driver
            .Manage()
            .Timeouts()
            .ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void CanLoadGuineaPigPage() {
        //Arrange & Act
        _ = new GuineaPigPage(_driver, SaucelessConstants.BaseUrl);

        //Assert
        _driver.Title.ShouldContain("I am a page title - Sauce Labs");
    }

    [Theory]
    [InlineData("This is a test comment from Selenium!")]
    [InlineData("This is another comment.")]
    public void CanFillAndSubmitForm(string comment) {
        //Arrange
        var guineaPigPage = new GuineaPigPage(_driver, SaucelessConstants.BaseUrl);

        //Act
        guineaPigPage.TypeField(_driver, SaucelessConstants.Fields.Comments, comment);
        GuineaPigPage.SubmitForm(_driver);

        //Assert
        var comments = GuineaPigPage.GetField(_driver, SaucelessConstants.Fields.YourComments);
        comments.Text.ShouldBe($"Your comments: {comment}");
    }

    [Fact]
    public void CanClickLink() {
        //Arrange
        var guineaPigPage = new GuineaPigPage(_driver, SaucelessConstants.BaseUrl);

        //Act
        guineaPigPage.ClickLink(_driver);

        //Assert
        _driver.Url.ShouldContain("saucelabs.com/test-guinea-pig2.html");
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
        GC.SuppressFinalize(this);
    }
}
