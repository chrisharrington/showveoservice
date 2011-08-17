using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ShowveoService.MVCApplication.Load;

namespace ShowveoService.MVCApplication
{
	/// <summary>
	/// The application class used to start ShowveoService.
	/// </summary>
	public class MvcApplication : HttpApplication
	{
		#region Public Methods
		/// <summary>
		/// Registers the global filters.
		/// </summary>
		/// <param name="filters">The filters to register.</param>
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		/// <summary>
		/// Sets the routes for incoming requests.
		/// </summary>
		/// <param name="routes">A collection of routes.</param>
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute("Authenticate", "user/authenticate", new {controller = "User", action = "Authenticate"});
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Fired when the application begins.
		/// </summary>
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			Loader.Start();
		}
		#endregion
	}
}