using DirectoryFileBrowsing.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace DirectoryFileBrowsing.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			//Check if bla bla
			Assert.AreEqual("Home Page", result.ViewBag.Title);
		}

		[TestMethod]
		public void EmptyTest()
		{

		}
	}
}
