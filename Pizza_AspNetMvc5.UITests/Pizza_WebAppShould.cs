using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Pizza_AspNetMvc5.UITests
{
	public class Pizza_WebAppShould
	{
		[Fact]
		[Trait("Category", "Smoke")] // if loads without error
		public void LoadApplicationPage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				const string appUrl = "https://localhost:44303/";
				driver.Navigate().GoToUrl(appUrl);
				string pageTitle = driver.Title;

				Assert.Equal(appUrl, driver.Url);
				Assert.Equal("Home - My Pizzerias App", pageTitle);
			}
		}
	}
}
