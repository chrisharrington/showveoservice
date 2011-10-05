using System.Web.Mvc;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to handle events on the home page.
	/// </summary>
	public class HomeController : Controller
	{
		#region Public Methods
		/// <summary>
		/// Renders the default view for the home page.
		/// </summary>
		/// <returns>The rendered view.</returns>
		public ActionResult Home()
		{
			return View();
		}
		#endregion
	}
}