using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Showveo.Container;

namespace Showveo.MVCApplication
{
	/// <summary>
	/// The application class used to start Showveo.
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
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("Default", "{controller}/{action}/{id}", new {controller = "Home", action = "Index", id = UrlParameter.Optional});
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