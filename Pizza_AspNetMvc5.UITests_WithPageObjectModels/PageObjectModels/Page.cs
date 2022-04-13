using OpenQA.Selenium;
using System;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels
{
	public class Page
	{
		protected IWebDriver Driver;

		protected virtual string PageUrl { get; }

		protected virtual string PageTitle { get; }

		public void NavigateTo()
		{
			Driver.Navigate().GoToUrl(PageUrl);
			EnsurePageLoaded(PageTitle);
		}

		public void EnsurePageLoaded(string title, bool onlyCheckUrlStartsWithExpectedText = true)
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
