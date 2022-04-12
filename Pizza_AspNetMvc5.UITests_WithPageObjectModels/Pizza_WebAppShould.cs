using ApprovalTests;
using ApprovalTests.Reporters;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using Xunit;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels
{
	[Trait("Category", "Smoke")]
	public class Pizza_WebAppShould : IClassFixture<ChromeDriverFixture>
	{
		private const string AboutUrl = "https://localhost:44303/Home/About";
		private const string ContactUrl = "https://localhost:44303/Home/Contact";
		private const string PizzeriasUrl = "https://localhost:44303/Pizzerias";
		private const string CreateUrl = "https://localhost:44303/Pizzerias/Create";
		private const string PizzeriasTitle = "Index - My Pizzerias App";
		// private const string HomeTitle = "Home - My Pizzerias App";

		private readonly ChromeDriverFixture ChromeDriverFixture;

		public Pizza_WebAppShould(ChromeDriverFixture chromeDriverFixture)
		{
			ChromeDriverFixture = chromeDriverFixture;
			ChromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
			ChromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
		}

		// If loads without error
		[Fact]
		public void LoadHomePage()
		{
			HomePage homePage = new HomePage(ChromeDriverFixture.Driver);
			homePage.NavigateTo(PizzeriasUrl, PizzeriasTitle);
		}

		// If reloads home page when going to different url and clicking back
		[Fact]
		public void ReloadHomePageOnBack()
		{
			HomePage homePage = new HomePage(ChromeDriverFixture.Driver);
			homePage.NavigateTo(PizzeriasUrl, PizzeriasTitle);
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();
			ChromeDriverFixture.Driver.Navigate().GoToUrl(AboutUrl);
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();
			ChromeDriverFixture.Driver.Navigate().Back();
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();
		}

		// If reloads home page when going to different url, then home and after that clicking back and then forward
		[Fact]
		public void ReloadHomePageOnForward()
		{
			ChromeDriverFixture.Driver.Navigate().GoToUrl(AboutUrl);
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();

			HomePage homePage = new HomePage(ChromeDriverFixture.Driver);
			homePage.NavigateTo(PizzeriasUrl, PizzeriasTitle);
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();

			ChromeDriverFixture.Driver.Navigate().Back();
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();

			ChromeDriverFixture.Driver.Navigate().Forward();
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();
		}

		// If opens live chat pop-up
		[Fact]
		public void AlertIfLiveChatClosed()
		{
			ChromeDriverFixture.Driver.Navigate().GoToUrl(ContactUrl);
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			TestHelper.Pause();

			ChromeDriverFixture.Driver.FindElement(By.Id("LiveChat")).Click();
			WebDriverWait wait = new WebDriverWait(ChromeDriverFixture.Driver, TimeSpan.FromSeconds(2));
			// Open Live Chat pop-up
			//IAlert alert = driver.SwitchTo().Alert();
			IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

			Assert.Equal("Live chat is currently not available.", alert.Text);

			TestHelper.Pause();
			// Simultes user clicking OK
			alert.Accept();
			TestHelper.Pause();
		}

		// If not navigates to about us url when cancel is clicked
		[Fact]
		public void NotNavigateToAboutUsWhenCancelClicked()
		{
			ChromeDriverFixture.Driver.Navigate().GoToUrl(ContactUrl);
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			Assert.Equal(ContactUrl, ChromeDriverFixture.Driver.Url);
			ChromeDriverFixture.Driver.FindElement(By.Id("LearnAboutUs")).Click();
			TestHelper.Pause();
			WebDriverWait wait = new WebDriverWait(ChromeDriverFixture.Driver, TimeSpan.FromSeconds(2));
			// Open Learn About Us pop-up
			IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
			// Simultes user clicking Cancel
			alert.Dismiss();

			Assert.StartsWith(ContactUrl, ChromeDriverFixture.Driver.Url);
		}

		// If sets and deletes cookie
		[Fact]
		public void NotDisplayCookieMessage()
		{
			HomePage homePage = new HomePage(ChromeDriverFixture.Driver);
			homePage.NavigateTo(PizzeriasUrl, PizzeriasTitle);
			IJavaScriptExecutor js = (IJavaScriptExecutor)ChromeDriverFixture.Driver;
			js.ExecuteScript("document.cookie = 'isCookieAccepted=yes'");
			ChromeDriverFixture.Driver.Navigate().Refresh();
			// Minimize browser window to prevent from accidential clicks
			ChromeDriverFixture.Driver.Manage().Window.Minimize();
			Cookie cookiesValue = ChromeDriverFixture.Driver.Manage().Cookies.GetCookieNamed("isCookieAccepted");

			Assert.Equal("yes", cookiesValue.Value);

			ChromeDriverFixture.Driver.Manage().Cookies.DeleteCookieNamed("isCookieAccepted");
			ChromeDriverFixture.Driver.Navigate().Refresh();

			Assert.NotNull(ChromeDriverFixture.Driver.FindElements(By.XPath("//div[@id='cookie-banner']")));
		}

		// If renders page (takes screenshot of page) and checks if screenshot matches approved file
		// You need to add ApprovalTests in nuget package reference
		[UseReporter(typeof(BeyondCompareReporter))]
		[Fact]
		public void RenderContactPage()
		{
			ChromeDriverFixture.Driver.Navigate().GoToUrl(ContactUrl);
			ITakesScreenshot screenshotDriver = (ITakesScreenshot)ChromeDriverFixture.Driver;
			Screenshot screenshot = screenshotDriver.GetScreenshot();
			screenshot.SaveAsFile("contactPage.bmp", ScreenshotImageFormat.Bmp);

			FileInfo file = new FileInfo("contactPage.bmp");

			Approvals.Verify(file);
			// When fails with error message:
			// ApprovalTests.Core.Exceptions.ApprovalMissingException : Failed Approval: Approval File not found
			// copy "contactPage.bmp" file from Pizza...UITests -> bin -> Debug to Pizza...UITests and
			// change it's name to Pizza_WebAppShould.RenderContactPage.approved.bmp
		}

		// Actions, more:
		// https://www.selenium.dev/selenium/docs/api/dotnet/html/T_OpenQA_Selenium_Interactions_Actions.htm
		[Fact]
		public void RenderAboutPageWithActions()
		{
			ChromeDriverFixture.Driver.Navigate().GoToUrl(PizzeriasUrl);
			IWebElement aboutUsLink = ChromeDriverFixture.Driver.FindElement(By.XPath("//a[contains(@href,'Create')]"));

			Actions actions = new Actions(ChromeDriverFixture.Driver);
			actions.MoveToElement(aboutUsLink);
			actions.Click();
			actions.Perform();

			Assert.Equal(CreateUrl, ChromeDriverFixture.Driver.Url);
		}
	}
}
