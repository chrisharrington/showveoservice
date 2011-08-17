using System.Web.Mvc;
using ShowveoService.Entities;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to authenticate a user.
	/// </summary>
	public class UserController : Controller
	{
		#region Public Methods
		/// <summary>
		/// Attempts to authenticate a user.
		/// </summary>
		/// <param name="emailAddress">The potential user's email address.</param>
		/// <param name="password">The potential user's encrypted password.</param>
		/// <returns>The authenticated user or null.</returns>
		public ActionResult Authenticate(string emailAddress, string password)
		{
			return Json(new User {FirstName = "Chris", LastName = "Harrington"}, JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}