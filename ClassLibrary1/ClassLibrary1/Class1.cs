using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Linq;

namespace ClassLibrary1
{
	[TestFixture]
	public class Homework
	{
		IWebDriver driver;

		[SetUp]
		public void SetUp()
		{
			//Option for Browser window
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddArguments
				(
					//"--headless",
					"--start-fullscreen",
					"--start-maximized"
				);
			TestContext.WriteLine("Creating driver with headles option");
			driver = new ChromeDriver(chromeOptions);
		}

		[TearDown]
		public void TearDown()
		{
			TestContext.WriteLine("Kill driver");
			driver.Quit();
		}

		[Test]
		public void TestHomeWork()
		{
			//ChromeDriver driver = new ChromeDriver();
			//Navigate to Leafground page
			TestContext.WriteLine("Go to Leafground page");
			driver.Navigate().GoToUrl("http://www.leafground.com/home.html");

			var goToHome = By.XPath("//a[text()='Go to Home Page']");

			//Open HiperLink in new tab
			TestContext.WriteLine("Open HyperLink page in new tab");
			new Actions(driver).KeyDown(Keys.Control).Click(driver.FindElement(By.LinkText("HyperLink"))).KeyUp(Keys.Control).Perform();

			//Switch to second tab
			TestContext.WriteLine("Switcn to the second tab");
			driver.SwitchTo().Window(driver.WindowHandles[1]);

			//Hover on “Go to Home Page” link
			TestContext.WriteLine("Hover on “Go to Home Page” link");
			new Actions(driver).MoveToElement(driver.FindElement(goToHome)).Perform();

			//Take Screenshot
			TestContext.WriteLine("Take Screenshot");
			var screenShot = ((ITakesScreenshot)driver).GetScreenshot();

			//Saving Screenshot
			TestContext.WriteLine("Saving Screenshot");
			var destinationPath = AppDomain.CurrentDomain.BaseDirectory;
			var screenshotPath = Path.Combine(destinationPath, "screenshot.png");
			screenShot.SaveAsFile(screenshotPath);

			//Adding Screenshtot to test output
			TestContext.AddTestAttachment(screenshotPath);

			//Close the tab
			TestContext.WriteLine("Close the tab");
			driver.Close();

			//Switch to the first tab
			TestContext.WriteLine("Switch to the first tab");
			driver.SwitchTo().Window(driver.WindowHandles[0]);

			//Navigate to Jquery 
			TestContext.WriteLine("Navigate to Jquery");
			driver.Navigate().GoToUrl("https://jqueryui.com/demos/");

			//Navigate to “Droppable” demo (Interactions section)
			var dropableDemo = driver.FindElement(By.XPath("//a[@href='https://jqueryui.com/droppable/']"));
			dropableDemo.Click();

			//Switch to frame
			//TestContext.WriteLine("Switch to frame");
			//driver.SwitchTo().Frame(driver.FindElement(By.XPath("//a[@href='https://jqueryui.com/droppable/']")));

			//Drag & Drop the small box into a big one
			TestContext.WriteLine("Drag & Drop the small box into a big one");
			new Actions(driver).DragAndDrop(driver.FindElement(By.CssSelector("#draggable")), driver.FindElement(By.CssSelector("#droppable"))).Perform();

			//Verify that big box now contains text “Dropped!”
			TestContext.WriteLine("Verify that big box now contains text “Dropped!”");
			Assert.That(driver.FindElement(By.CssSelector("div#droppable p")).Text, Is.EqualTo("Dropped!"));



			driver.Quit();
		}
	}
}
