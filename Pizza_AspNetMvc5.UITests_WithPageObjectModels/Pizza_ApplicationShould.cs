using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels;
using Xunit;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels
{
	[Trait("Category", "Applications")]
	public class Pizza_ApplicationShould : IClassFixture<ChromeDriverFixture>
	{
		private const string HomeUrl = "https://localhost:44303/";
		private const string PizzeriasUrl = "https://localhost:44303/Pizzerias";
		private const string CreateNewUrl = "https://localhost:44303/Pizzerias/Create";
		private const string DetailsOfNewUrl = "https://localhost:44303/Pizzerias/Details";
		private const string HomeTitle = "Home - My Pizzerias App";
		private const string PizzeriasTitle = "Index - My Pizzerias App";
		private const string CreateNewTitle = "Create - My Pizzerias App";
		private const string DetailsOfNewTitle = "Details - My Pizzerias App";

		private readonly ChromeDriverFixture ChromeDriverFixture;

		public Pizza_ApplicationShould(ChromeDriverFixture chromeDriverFixture)
		{
			ChromeDriverFixture = chromeDriverFixture;
			ChromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
			ChromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
		}

		[Fact]
		public void SeeAllPizzeriasFromHomePage()
		{
				HomePage homePage = new HomePage(ChromeDriverFixture.Driver);
				homePage.NavigateTo(HomeUrl, HomeTitle);
				// Minimize browser window to prevent from accidential clicks
				ChromeDriverFixture.Driver.Manage().Window.Minimize();
				// TestHelper.Pause();
				// IWebElement seeAllPizzeriasLink = driver.FindElement(By.Name("SeeAllPizzerias"));

				// Better option (waits until 1 sec but it's not fixed time, proceeds when element loaded before that time):
				WebDriverWait wait = new WebDriverWait(ChromeDriverFixture.Driver, TimeSpan.FromSeconds(1));
				// IWebElement seeAllPizzeriasLink = wait.Until((d)=> d.FindElement(By.Name("SeeAllPizzerias")));
				IWebElement seeAllPizzeriasLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("SeeAllPizzerias")));
				seeAllPizzeriasLink.Click();
				TestHelper.Pause();

				Assert.Equal(PizzeriasTitle, ChromeDriverFixture.Driver.Title); 
				Assert.Equal(PizzeriasUrl, ChromeDriverFixture.Driver.Url);
		}

		[Fact]
		public void SeeAllPizzeriasFromHomePageLogo()
		{
				HomePage homePage = new HomePage(ChromeDriverFixture.Driver);
				homePage.NavigateTo(HomeUrl, HomeTitle);
				// Minimize browser window to prevent from accidential clicks
				ChromeDriverFixture.Driver.Manage().Window.Minimize();
				TestHelper.Pause();

				ApplicationPage applicationPage = homePage.AllPizzeriasLinkClick();

				applicationPage.EnsureHomePageLoaded(PizzeriasTitle);
		}

		[Fact]
		public void SeeCreateNewPizzeriaFromPizzeriasPage()
		{
				ChromeDriverFixture.Driver.Navigate().GoToUrl(PizzeriasUrl);
				// Minimize browser window to prevent from accidential clicks
				ChromeDriverFixture.Driver.Manage().Window.Minimize();
				TestHelper.Pause();

				IWebElement createNewPizzeriaLink = ChromeDriverFixture.Driver.FindElement(By.LinkText("Add New"));
				createNewPizzeriaLink.Click();
				TestHelper.Pause();

				Assert.Equal(CreateNewTitle, ChromeDriverFixture.Driver.Title);
				Assert.Equal(CreateNewUrl, ChromeDriverFixture.Driver.Url);
		}

		[Fact]
		public void SeeCreateNewPizzeriaFromPizzeriasPage_UsingXPath()
		{
				ChromeDriverFixture.Driver.Navigate().GoToUrl(PizzeriasUrl);
				// Minimize browser window to prevent from accidential clicks
				ChromeDriverFixture.Driver.Manage().Window.Minimize();
				TestHelper.Pause();

				IWebElement createNewPizzeriaLink = ChromeDriverFixture.Driver.FindElement(By.XPath("//p/a[contains(text(),'Add New')]"));
				createNewPizzeriaLink.Click();
				TestHelper.Pause();

				Assert.Equal(CreateNewTitle, ChromeDriverFixture.Driver.Title);
				Assert.Equal(CreateNewUrl, ChromeDriverFixture.Driver.Url);
		}

		[Fact]
		public void BeCreatedWhenNewPizzeriaValid()
		{
			const string newName = "Pizzeria";
			const string newLocation = "La Mano";
			const string newType = "Turkish";
			
			ApplicationPage homePage = new ApplicationPage(ChromeDriverFixture.Driver);
			homePage.NavigateTo(CreateNewUrl, CreateNewTitle);
			
			homePage.EnterName(newName);
			homePage.EnterLocation(newLocation);
			homePage.EnterType(newType);
			
			ApplicationCompletePage applicationCompletePage = homePage.CreateNew();
			
			applicationCompletePage.EnsureHomePageLoaded(DetailsOfNewTitle);
			Assert.Equal($"Details for {newName}", applicationCompletePage.MainName);
			Assert.Equal($"{newName}", applicationCompletePage.Name);
			Assert.Equal(newLocation, applicationCompletePage.Location);
			Assert.Equal(newType, applicationCompletePage.Type); 
		}

		[Fact]
		public void BeCreatedWhenValidationErrorsCorrected()
		{
			const string newName = "Pizzeria";
			const string invalidLocation = "";
			const string validLocation = "La Mano";
			const string newType = "Turkish";
			
			ApplicationPage applicationPage = new ApplicationPage(ChromeDriverFixture.Driver);
			applicationPage.NavigateTo(CreateNewUrl, CreateNewTitle);
			
			applicationPage.EnterName(newName);
			// Don't enter location
			applicationPage.EnterLocation(invalidLocation);
			applicationPage.EnterType(newType);
			
			// Submit form
			ApplicationCompletePage applicationCompletePage = applicationPage.CreateNew();

			// Asserts that validation failed
			// Instead of: Assert.Equal(1, applicationPage.ValidationErrorMessages.Count);
			Assert.Single(applicationPage.ValidationErrorMessages);
			Assert.Contains("The Location field is required.", applicationPage.ValidationErrorMessages);

			// Fix error - set correct location
			applicationPage.ClearLocation();
			applicationPage.EnterLocation(validLocation);

			// Resubmit form
			applicationPage.CreateNew();

			// Check new pizzeria created
			applicationCompletePage.EnsureHomePageLoaded(DetailsOfNewTitle);
			Assert.Equal($"Details for {newName}", applicationCompletePage.MainName);
			Assert.Equal($"{newName}", applicationCompletePage.Name);
			Assert.Equal(validLocation, applicationCompletePage.Location);
			Assert.Equal(newType, applicationCompletePage.Type);
		}
	}
}
