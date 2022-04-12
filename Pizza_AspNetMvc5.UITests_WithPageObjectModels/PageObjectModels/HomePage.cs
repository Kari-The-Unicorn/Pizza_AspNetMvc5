using OpenQA.Selenium;
using System;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels
{
	public class HomePage
	{
		private readonly IWebDriver Driver;
		private const string HomeUrl = "https://localhost:44303/";
		private const string HomeTitle = "Home - My Pizzerias App";

		public HomePage(IWebDriver driver)
		{
			Driver = driver;
		}

		// public string exampleToken => Driver.FindElement(By.XPath("exampleXpath")).Text;
		// public bool isCookieMessagePreent => Driver.FindElement(By.XPath("exampleXpath")).Any();
		public ApplicationPage AllPizzeriasLinkClick()
		{
			Driver.FindElement(By.Id("allPizzeriasLogo")).Click();
			return new ApplicationPage(Driver);
		}

		public void NavigateTo(string url, string title)
		{
			Driver.Navigate().GoToUrl(url);
			EnsureHomePageLoaded(title);
		}

		public void EnsureHomePageLoaded(string title, bool onlyCheckUrlStartsWithExpectedText = true)
		{
			bool isUrlCorrect;

			if (onlyCheckUrlStartsWithExpectedText)
			{
				isUrlCorrect = Driver.Url.StartsWith(HomeUrl);
			}
			else
			{
				isUrlCorrect = Driver.Url == HomeUrl;
			}

			bool pageHasLoaded = isUrlCorrect && (Driver.Title == title);

			if (!pageHasLoaded)
			{
				throw new Exception($"Failed to load home page with page URL: {Driver.Url}" +
					$"and page Source: {Driver.PageSource}");
			}
		}
	}
}
