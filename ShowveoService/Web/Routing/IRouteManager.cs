using System.Web.Routing;

namespace ShowveoService.Web.Routing
{
	/// <summary>
	/// Defines a class used to manage url routes.
	/// </summary>
	public interface IRouteManager
	{
		#region Public Methods
		/// <summary>
		/// Defines a set of routes.
		/// </summary>
		/// <param name="routes">The pre-existing collection of routes.</param>
		void DefineRoutes(RouteCollection routes);
		#endregion
	}
}