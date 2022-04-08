using OpenQA.Selenium;
using System;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels
{
	public class ApplicationPage
	{
		private readonly IWebDriver Driver;
		private const string HomeUrl = "https://localhost:44303/";
		private const string HomeTitle = "Home - My Pizzerias App";

		public ApplicationPage(IWebDriver driver)
		{
			Driver = driver;
		}

		// public string exampleToken => Driver.FindElement(By.XPath("exampleXpath")).Text;
		// public bool isCookieMessagePreent => Driver.FindElement(By.XPath("exampleXpath")).Any();
		public void AllPizzeriasLinkClick() => Driver.FindElement(By.Id("allPizzeriasLogo")).Click();

		public void NativateToHome()
		{
			Driver.Navigate().GoToUrl(HomeUrl);
			EnsureHomePageLoaded();
		}

		public void EnsureHomePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
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

			bool pageHasLoaded = isUrlCorrect && (Driver.Title == HomeTitle);

			if (!pageHasLoaded)
			{
				throw new Exception($"Failed to load home page with page URL: {Driver.Url}" +
					$"and page Source: {Driver.PageSource}");
			}
		}
	}
}
