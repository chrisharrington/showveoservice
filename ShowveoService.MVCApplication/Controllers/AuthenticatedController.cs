using System.Web.Mvc;
using ShowveoService.Data;
using ShowveoService.Entities;
using ShowveoService.MVCApplication.Load;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller that provides access to the user making the request.
	/// </summary>
	public class AuthenticatedController : Controller
	{
		#region Properties
		/// <summary>
		/// The authenticated user.
		/// </summary>
		protected new User User { get; private set; }
		#endregion

		#region Protected Methods
		/// <summary>
		/// Fired at the authorization phase. Retrieves user identity information from the request, which is used to
		/// determine which user is making the request.
		/// </summary>
		protected override void OnAuthorization(AuthorizationContext filterContext)
		{
			var cookie = filterContext.HttpContext.Request.Cookies["identity"];
			if (cookie == null)
			{
				User = DR.Get<IUserRepository>().Authenticate("blah");
				return;
			}

			var identity = cookie.Value;
			if (string.IsNullOrEmpty(identity))
				return;

			User = DR.Get<IUserRepository>().Authenticate(identity);
			
			base.OnAuthorization(filterContext);
		}
		#endregion
	}
}