using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels
{
	public sealed class ChromeDriverFixture : IDisposable
	{
		public IWebDriver Driver { get; private set; }

		public ChromeDriverFixture()
		{
			Driver = new ChromeDriver();
		}

		public void Dispose()
		{
			Driver.Dispose();
		}
	}
}
