using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels
{
	public class ApplicationCompletePage
	{
		private readonly IWebDriver Driver;
		private const string HomeUrl = "https://localhost:44303/Pizzerias";
		private const string CreateNewUrl = "https://localhost:44303/Pizzerias/Create";
		private const string HomeTitle = "Index - My Pizzerias App";
		private const string CreateNewTitle = "Create - My Pizzerias App";

		public ApplicationCompletePage(IWebDriver driver)
		{
			Driver = driver;
		}

		public string MainName => Driver.FindElement(By.TagName("h2")).Text;

		public string Name => Driver.FindElement(By.XPath("(//dt/following-sibling::dd)[1]")).Text;

		public string Location => Driver.FindElement(By.XPath("(//dt/following-sibling::dd)[2]")).Text;

		public string Type => Driver.FindElement(By.XPath("(//dt/following-sibling::dd)[3]")).Text;

		public void EnterType(string type)
		{
			IWebElement createNewTypeSelect = Driver.FindElement(By.Id("Type"));
			SelectElement createNewType = new SelectElement(createNewTypeSelect);
			createNewType.SelectByText($"{type}");
		}

		public void CreateNew() => Driver.FindElement(By.XPath("//input[@value='Create']")).Click();

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
