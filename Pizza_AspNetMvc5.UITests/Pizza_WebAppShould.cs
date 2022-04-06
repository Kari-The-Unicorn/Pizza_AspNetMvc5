using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace Pizza_AspNetMvc5.UITests
{
	[Trait("Category", "Smoke")]
	public class Pizza_WebAppShould
	{
		private const string HomeUrl = "https://localhost:44303/";
		private const string AboutUrl = "https://localhost:44303/Home/About";
		private const string ContactUrl = "https://localhost:44303/Home/Contact";
		private const string HomeTitle = "Home - My Pizzerias App";

		// If loads without error
		[Fact] 
		public void LoadHomePage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();
				string pageTitle = driver.Title;

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, pageTitle);
			}
		}

		// If reloads without error
		[Fact] 
		public void ReloadHomePage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();
				driver.Navigate().Refresh();
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, driver.Title);
			}
		}

		// If reloads home page when going to different url and clicking back
		[Fact]
		public void ReloadHomePageOnBack()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();
				driver.Navigate().GoToUrl(AboutUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();
				driver.Navigate().Back();
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, driver.Title);
			}
		}

		// If reloads home page when going to different url, then home and after that clicking back and then forward
		[Fact]
		public void ReloadHomePageOnForward()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(AboutUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				driver.Navigate().GoToUrl(HomeUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				driver.Navigate().Back();
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				driver.Navigate().Forward();
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, driver.Title);
			}
		}

		// If opens live chat pop-up
		[Fact]
		public void AlertIfLiveChatClosed()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(AboutUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();
				driver.FindElement(By.Id("LiveChat")).Click();
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
				// Open Live Chat pop-up
				//IAlert alert = driver.SwitchTo().Alert();
				IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

				Assert.Equal("Live chat is currently not available.", alert.Text);

				TestHelper.Pause();
				// Simultes user clicking OK
				alert.Accept();
				TestHelper.Pause();
			}
		}

		// If not navigates to about us url when cancel is clicked
		[Fact]
		public void NotNavigateToAboutUsWhenCancelClicked()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(ContactUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				Assert.Equal(ContactUrl, driver.Url);
				driver.FindElement(By.Id("LearnAboutUs")).Click();
				TestHelper.Pause();
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
				// Open Learn About Us pop-up
				IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
				// Simultes user clicking Cancel
				alert.Dismiss();

				Assert.Equal(ContactUrl, driver.Url);
			}
		}

		// If sets and deletes cookie
		[Fact]
		public void NotDisplayCookieMessage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
				js.ExecuteScript("document.cookie = 'isCookieAccepted=yes'");
				driver.Navigate().Refresh();
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				Cookie cookiesValue = driver.Manage().Cookies.GetCookieNamed("isCookieAccepted");

				Assert.Equal("yes", cookiesValue.Value);

				driver.Manage().Cookies.DeleteCookieNamed("isCookieAccepted");
				driver.Navigate().Refresh();

				Assert.NotNull(driver.FindElements(By.XPath("//div[@id='cookie-banner']")));
			}
		}

		// If renders page (takes screenshot of page) and checks if screenshot matches approved file
		// You need to add ApprovalTests in nuget package reference
		[UseReporter(typeof(BeyondCompareReporter))]
		[Fact]
		public void RenderContactPage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(ContactUrl);
				ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
				Screenshot screenshot = screenshotDriver.GetScreenshot();
				screenshot.SaveAsFile("contactPage.bmp", ScreenshotImageFormat.Bmp);

				FileInfo file = new FileInfo("contactPage.bmp");

				Approvals.Verify(file);
				// When fails with error message:
				// ApprovalTests.Core.Exceptions.ApprovalMissingException : Failed Approval: Approval File not found
				// copy "contactPage.bmp" file from Pizza...UITests -> bin -> Debug to Pizza...UITests and
				// change it's name to Pizza_WebAppShould.RenderContactPage.approved.bmp
			}
		}
	}
}
