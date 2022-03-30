using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Pizza_AspNetMvc5.UITests
{
	[Trait("Category", "Applications")]
	public class Pizza_ApplicationShould
	{
		private const string HomeUrl = "https://localhost:44303/";
		private const string PizzeriasUrl = "https://localhost:44303/Pizzerias";
		private const string CreateNewUri = "/Create";
		private const string PizzeriasTitle = "Index - My Pizzerias App";
		private const string CreateNewTitle = "Create - My Pizzerias App";

		[Fact]
		public void SeeAllPizzeriasFromHomePage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(HomeUrl);
				TestHelper.Pause();

				IWebElement seeAllPizzeriasLink = driver.FindElement(By.Name("SeeAllPizzerias"));
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
				driver.Navigate().GoToUrl(HomeUrl);
				TestHelper.Pause();

				IWebElement seeAllPizzeriasLink = driver.FindElement(By.Id("allPizzeriasLogo"));
				seeAllPizzeriasLink.Click();
				TestHelper.Pause();

				Assert.Equal(PizzeriasTitle, driver.Title);
				Assert.Equal(PizzeriasUrl, driver.Url);
			}
		}

		[Fact]
		public void SeeCreateNewPizzeriaFromPizzeriasPage()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(PizzeriasUrl);
				TestHelper.Pause();

				IWebElement createNewPizzeriaLink = driver.FindElement(By.LinkText("Add New"));
				createNewPizzeriaLink.Click();
				TestHelper.Pause();

				Assert.Equal(CreateNewTitle, driver.Title);
				Assert.Equal($"{PizzeriasUrl}{CreateNewUri}", driver.Url);
			}
		}
	}
}
