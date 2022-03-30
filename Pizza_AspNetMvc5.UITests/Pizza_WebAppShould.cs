using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Pizza_AspNetMvc5.UITests
{
	public class Pizza_WebAppShould
	{
		private const string HomeUrl = "https://localhost:44303/";
		private const string AboutUrl = "https://localhost:44303/Home/About";
		private const string HomeTitle = "Home - My Pizzerias App";

		// if loads without error
		[Fact]
		[Trait("Category", "Smoke")] 
		public void LoadHomePage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				TestHelper.Pause();
				string pageTitle = driver.Title;

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, pageTitle);
			}
		}

		// if reloads without error
		[Fact]
		[Trait("Category", "Smoke")] 
		public void ReloadHomePage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				TestHelper.Pause();
				driver.Navigate().Refresh();

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, driver.Title);
			}
		}

		// if reloads home page when going to different url and clicking back
		[Fact]
		[Trait("Category", "Smoke")] 
		public void ReloadHomePageOnBack()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				TestHelper.Pause();
				driver.Navigate().GoToUrl(AboutUrl);
				TestHelper.Pause();
				driver.Navigate().Back();
				TestHelper.Pause();

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, driver.Title);
			}
		}

		// if reloads home page when going to different url, then home and after that clicking back and then forward
		[Fact]
		[Trait("Category", "Smoke")]
		public void ReloadHomePageOnForward()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(AboutUrl);
				TestHelper.Pause();

				driver.Navigate().GoToUrl(HomeUrl);
				TestHelper.Pause();

				driver.Navigate().Back();
				TestHelper.Pause();

				driver.Navigate().Forward();
				TestHelper.Pause();

				Assert.Equal(HomeUrl, driver.Url);
				Assert.Equal(HomeTitle, driver.Title);
			}
		}
	}
}
