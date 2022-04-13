using OpenQA.Selenium;
using System;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels
{
	public class HomePage : Page
	{
		public HomePage(IWebDriver driver)
		{
			Driver = driver;
		}

		protected override string PageUrl => "https://localhost:44303/";
		protected override string PageTitle => "Home - My Pizzerias App";

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
				isUrlCorrect = Driver.Url.StartsWith(PageUrl);
			}
			else
			{
				isUrlCorrect = Driver.Url == PageUrl;
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
