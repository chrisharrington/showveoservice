using System;
using System.Web.Mvc;
using ShowveoService.Service.Logging;
using ShowveoService.Web;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to provide handling for remote movie queries.
	/// </summary>
	public class RemoteMoviesController : Controller
	{
		#region Data Members
		/// <summary>
		/// A container for remote movie information.
		/// </summary>
		private readonly IRemoteMovieRepository _remoteMovieRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="remoteMovieRepository">A container for remote movie information.</param>
		public RemoteMoviesController(IRemoteMovieRepository remoteMovieRepository)
		{
			if (remoteMovieRepository == null)
				throw new ArgumentNullException("remoteMovieRepository");

			_remoteMovieRepository = remoteMovieRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Performs a search for movie information using the given query.
		/// </summary>
		/// <param name="query">The query used to perform the search.</param>
		/// <returns>The found movie information.</returns>
		public ActionResult Search(string query)
		{
			try
			{
				if (query == null)
					throw new ArgumentNullException("query");

				return Json(_remoteMovieRepository.Search(query), JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Error("Error during RemoteMoviesController.Search.", ex);
				throw;
			}
		}
		#endregion
	}
}