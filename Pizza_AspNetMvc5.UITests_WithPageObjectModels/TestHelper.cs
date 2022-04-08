using System.Threading;

namespace Pizza_AspNetMvc5.UITests_WithPageObjectModels
{
	/// <summary>
	/// Delay to slow down browser interactions when testing UI
	/// </summary>
	internal static class TestHelper
	{
		public static void Pause(int pauseTimeInSeconds = 1000)
		{
			Thread.Sleep(pauseTimeInSeconds);
		}
	}
}
