using OpenQA.Selenium;
using System;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels
{
	public class ApplicationPage
	{
		private readonly IWebDriver Driver;
		private const string HomeUrl = "https://localhost:44303/Pizzerias";
		private const string HomeTitle = "Index - My Pizzerias App";

		public ApplicationPage(IWebDriver driver)
		{
			Driver = driver;
		}

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
