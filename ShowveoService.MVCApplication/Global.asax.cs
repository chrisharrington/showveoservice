using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ShowveoService.MVCApplication.Load;
using ShowveoService.Service.Logging;
using ShowveoService.Web;
using ShowveoService.Web.Routing;
using log4net;
using log4net.Config;

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
		#endregion

		#region Event Handlers
		/// <summary>
		/// Fired when the application begins.
		/// </summary>
		protected void Application_Start()
		{
			XmlConfigurator.Configure();

			Loader.Start();
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);

			DR.Get<IRouteManager>().DefineRoutes(RouteTable.Routes);
		}
		#endregion
	}
}