using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Pizza_AspNetMvc5.UITests
{
	public class Pizza_JavascriptTests
	{
		private const string AboutUrl = "https://localhost:44303/Home/About";

		// Hidden overlayed page comes from _Overlay.cshtml
		[Fact]
		public void ClickOverlayedLink()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(AboutUrl);
				TestHelper.Pause();

				// This line doesn't work because element is not clickable (grey overlay hides element)
				// driver.FindElement(By.Id("hiddenLink")).Click();

				string jsscript = "document.getElementById('hiddenLink').click()";
				IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
				jsExecutor.ExecuteScript(jsscript);

				Assert.Equal("https://www.w3schools.com/", driver.Url);
			}
		}

		[Fact]
		public void GetOverlayedLinkText()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(AboutUrl);
				TestHelper.Pause();

				string jsscript = "return document.getElementById('hiddenLink').innerHTML;";
				IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
				string linkText = (string)jsExecutor.ExecuteScript(jsscript);

				Assert.Equal("Go to w3schools page", linkText);
			}
		}
	}
}
