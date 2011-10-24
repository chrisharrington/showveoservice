using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShowveoService.Web.Routing
{
	/// <summary>
	/// Manages url routing.
	/// </summary>
	public class RouteManager : IRouteManager
	{
		#region Data Members
		/// <summary>
		/// The pre-existing collection of routes.
		/// </summary>
		private RouteCollection _routes;
		#endregion

		#region Public Methods
		/// <summary>
		/// Defines a set of routes.
		/// </summary>
		/// <param name="routes">The pre-existing collection of routes.</param>
		public void DefineRoutes(RouteCollection routes)
		{
			if (routes == null)
				throw new ArgumentNullException("routes");

			_routes = routes;

			DefinePageRoutes();
			DefineDataRoutes();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Defines a collection of routes pertaining to pages.
		/// </summary>
		private void DefinePageRoutes()
		{
			_routes.MapRoute("Home", "", new {controller = "Home", action = "Home"});
		}

		/// <summary>
		/// Defines a collection of routes used to retrieve dynamic data.
		/// </summary>
		private void DefineDataRoutes()
		{
			_routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			_routes.MapRoute("Authenticate", "user/authenticate", new { controller = "User", action = "Authenticate" });

			_routes.MapRoute("GetAllMovies", "movies/all", new { controller = "Movie", action = "GetAll" });
			_routes.MapRoute("GetLatestMovies", "movies/latest", new { controller = "Movie", action = "GetLatest" });
			_routes.MapRoute("PlayPhoneMovie", "movies/play/{type}/{id}", new { controller = "Movie", action = "PlayMovie" });

			_routes.MapRoute("GetUncategorizedMovieCount", "uncategorized/count", new { controller = "UncategorizedMovie", action = "GetUncategorizedMovieCount" });
			_routes.MapRoute("GetUncategorizedMovies", "uncategorized/all", new { controller = "UncategorizedMovie", action = "GetUncategorizedMovies" });
			_routes.MapRoute("CategorizeMovie", "uncategorized/categorize/{uncategorizedMovieID}/{categorizedMovieID}", new { controller = "UncategorizedMovie", action = "CategorizeMovie" });

			_routes.MapRoute("Search", "remotemovies/search", new { controller = "RemoteMovies", action = "Search" });

			_routes.MapRoute("GetAllGenres", "genres/all", new {controller = "Genre", action = "GetAll"});
		}
		#endregion
	}
}