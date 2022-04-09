using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Pizza_AspNetMvc5.UITests_WithPageObjectModels.PageObjectModels;
using Xunit;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels
{
	[Trait("Category", "Applications")]
	public class Pizza_ApplicationShould
	{
		private const string PizzeriasUrl = "https://localhost:44303/Pizzerias";
		private const string CreateNewUrl = "https://localhost:44303/Pizzerias/Create";
		private const string DetailsOfNewUrl = "https://localhost:44303/Pizzerias/Details";
		private const string PizzeriasTitle = "Index - My Pizzerias App";
		private const string CreateNewTitle = "Create - My Pizzerias App";

		[Fact]
		public void SeeAllPizzeriasFromHomePage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				HomePage homePage = new HomePage(driver);
				homePage.NativateToHome();
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				// TestHelper.Pause();
				// IWebElement seeAllPizzeriasLink = driver.FindElement(By.Name("SeeAllPizzerias"));

				// Better option (waits until 1 sec but it's not fixed time, proceeds when element loaded before that time):
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
				// IWebElement seeAllPizzeriasLink = wait.Until((d)=> d.FindElement(By.Name("SeeAllPizzerias")));
				IWebElement seeAllPizzeriasLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("SeeAllPizzerias")));
				seeAllPizzeriasLink.Click();
				TestHelper.Pause();

				Assert.Equal(PizzeriasTitle, driver.Title); 
				Assert.Equal(PizzeriasUrl, driver.Url);
			}
		}

		[Fact]
		public void SeeAllPizzeriasFromHomePageLogo()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				HomePage homePage = new HomePage(driver);
				homePage.NativateToHome();
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				ApplicationPage applicationPage = homePage.AllPizzeriasLinkClick();

				applicationPage.EnsureHomePageLoaded();
			}
		}

		[Fact]
		public void SeeCreateNewPizzeriaFromPizzeriasPage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(PizzeriasUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				IWebElement createNewPizzeriaLink = driver.FindElement(By.LinkText("Add New"));
				createNewPizzeriaLink.Click();
				TestHelper.Pause();

				Assert.Equal(CreateNewTitle, driver.Title);
				Assert.Equal(CreateNewUrl, driver.Url);
			}
		}

		[Fact]
		public void SeeCreateNewPizzeriaFromPizzeriasPage_UsingXPath()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(PizzeriasUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				IWebElement createNewPizzeriaLink = driver.FindElement(By.XPath("//p/a[contains(text(),'Add New')]"));
				createNewPizzeriaLink.Click();
				TestHelper.Pause();

				Assert.Equal(CreateNewTitle, driver.Title);
				Assert.Equal(CreateNewUrl, driver.Url);
			}
		}

		[Fact]
		public void BeCreatedWhenNewPizzeriaValid()
		{
			const string newName = "Pizzeria";

			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(CreateNewUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				IWebElement createNewName = driver.FindElement(By.XPath("//input[@id='Name']"));
				// createNewName.Text = "Pizzeria"; can't be done because it's readonly so it can be done this way:
				createNewName.SendKeys(newName);
				TestHelper.Pause();

				IWebElement createNewLocation = driver.FindElement(By.Id("Location"));
				createNewLocation.SendKeys("La Mano");
				TestHelper.Pause();

				// Select from dropdown list (enums)
				IWebElement createNewTypeSelect = driver.FindElement(By.Id("Type"));
				SelectElement createNewType = new SelectElement(createNewTypeSelect);
				createNewType.SelectByText("Turkish");
				TestHelper.Pause();

				IWebElement createNewSubmit = driver.FindElement(By.XPath("//input[@value='Create']"));
				createNewSubmit.Click();
				TestHelper.Pause();

				Assert.StartsWith("Details - My Pizzerias App", driver.Title);
				Assert.StartsWith(DetailsOfNewUrl, driver.Url);
				Assert.Equal($"Details for {newName}", driver.FindElement(By.TagName("h2")).Text);
				Assert.Equal($"{newName}", driver.FindElement(By.XPath("(//dt/following-sibling::dd)[1]")).Text);
				Assert.Equal("La Mano", driver.FindElement(By.XPath("(//dt/following-sibling::dd)[2]")).Text);
				Assert.Equal("Turkish", driver.FindElement(By.XPath("(//dt/following-sibling::dd)[3]")).Text); 
			}
		}

		[Fact]
		public void BeCreatedWhenValidationErrorsCorrected()
		{
			const string newName = "Pizzeria";
			const string invalidLocation = "";
			const string validLocation = "La Mano";

			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(CreateNewUrl);
				// Minimize browser window to prevent from accidential clicks
				driver.Manage().Window.Minimize();
				TestHelper.Pause();

				IWebElement createNewName = driver.FindElement(By.XPath("//input[@id='Name']"));
				createNewName.SendKeys(newName);
				TestHelper.Pause();
				IWebElement createNewLocation = driver.FindElement(By.Id("Location"));
				// Don't enter valid location
				createNewLocation.SendKeys(invalidLocation);
				TestHelper.Pause();
				// Select from dropdown list (enums)
				IWebElement createNewTypeSelect = driver.FindElement(By.Id("Type"));
				SelectElement createNewType = new SelectElement(createNewTypeSelect);
				createNewType.SelectByText("Turkish");
				TestHelper.Pause();
				IWebElement createNewSubmit = driver.FindElement(By.XPath("//input[@value='Create']"));
				createNewSubmit.Click();
				TestHelper.Pause();

				// Asserts that validation failed
				var validationError = driver.FindElement(By.XPath("//span[@id='Location-error']")).Text;
				Assert.Equal("The Location field is required.", validationError);

				// Fix error - set correct location
				createNewLocation.SendKeys(validLocation);

				// Resubmit form
				createNewSubmit.Click();

				// Check new pizzeria created
				Assert.StartsWith("Details - My Pizzerias App", driver.Title);
				Assert.StartsWith(DetailsOfNewUrl, driver.Url);
				Assert.Equal($"Details for {newName}", driver.FindElement(By.TagName("h2")).Text);
				Assert.Equal($"{newName}", driver.FindElement(By.XPath("(//dt/following-sibling::dd)[1]")).Text);
				Assert.Equal("La Mano", driver.FindElement(By.XPath("(//dt/following-sibling::dd)[2]")).Text);
				Assert.Equal("Turkish", driver.FindElement(By.XPath("(//dt/following-sibling::dd)[3]")).Text);
			}
		}
	}
}
