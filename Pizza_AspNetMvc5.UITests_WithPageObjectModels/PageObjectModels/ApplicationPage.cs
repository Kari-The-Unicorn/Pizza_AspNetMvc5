using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels
{
	public class ApplicationPage : Page
	{
		public ApplicationPage(IWebDriver driver)
		{
			Driver = driver;
		}

		protected override string PageUrl => "https://localhost:44303/Pizzerias";
		protected override string PageTitle => "Index - My Pizzerias App";

		public ReadOnlyCollection<string> ValidationErrorMessages
		{
			get
			{
				return Driver.FindElements(
					By.XPath("//span[contains(@id,'error')]"))
					.Select(x => x.Text)
					.ToList()
					.AsReadOnly();
			}
		}

		public void ClearLocation() => Driver.FindElement(By.Id("Location")).Clear();

		public void EnterName(string name) => Driver.FindElement(By.XPath("//input[@id='Name']")).SendKeys(name);

		public void EnterLocation(string location) => Driver.FindElement(By.Id("Location")).SendKeys(location);

		public void EnterType(string type)
		{
			IWebElement createNewTypeSelect = Driver.FindElement(By.Id("Type"));
			SelectElement createNewType = new SelectElement(createNewTypeSelect);
			createNewType.SelectByText($"{type}");
		}

		public ApplicationCompletePage CreateNew()
		{
			Driver.FindElement(By.XPath("//input[@value='Create']")).Click();
			return new ApplicationCompletePage(Driver);
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
