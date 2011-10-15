using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ShowveoService.Data;
using ShowveoService.Service.Logging;
using ShowveoService.Web;
using ShowveoService.Web.Remote;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to provide access to uncategorized movies.
	/// </summary>
	public class UncategorizedMovieController : Controller
	{
		#region Data Members
		/// <summary>
		/// A container for uncategorized movie information.
		/// </summary>
		private readonly IUncategorizedMovieRepository _uncategorizedMovieRepository;

		/// <summary>
		/// A container for remote movie information.
		/// </summary>
		private readonly IRemoteMovieRepository _remoteMovieRepository;

		/// <summary>
		/// A container for movie information.
		/// </summary>
		private readonly IMovieRepository _movieRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="uncategorizedMovieRepository">A container for uncategorized movie information.</param>
		/// <param name="remoteMovieRepository">A container for remote movie information.</param>
		/// <param name="movieRepository">A container for movie information.</param>
		public UncategorizedMovieController(IUncategorizedMovieRepository uncategorizedMovieRepository, IRemoteMovieRepository remoteMovieRepository,
			IMovieRepository movieRepository)
		{
			if (uncategorizedMovieRepository == null)
				throw new ArgumentNullException("uncategorizedMovieRepository");
			if (remoteMovieRepository == null)
				throw new ArgumentNullException("remoteMovieRepository");

			_uncategorizedMovieRepository = uncategorizedMovieRepository;
			_remoteMovieRepository = remoteMovieRepository;
			_movieRepository = movieRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Retrieves the number of movies that have yet to be categorized.
		/// </summary>
		/// <returns>The uncategorized movie count.</returns>
		public ActionResult GetUncategorizedMovieCount()
		{
			try
			{
				return Json(_uncategorizedMovieRepository.GetAll().Count(), JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Error("Error during UncategorizedMovieController.GetUncategorizedMovieCount.", ex);
				throw;
			}
		}

		/// <summary>
		/// Retrieves the collection of all uncategorized movies.
		/// </summary>
		/// <returns>The retrieved collection.</returns>
		public ActionResult GetUncategorizedMovies()
		{
			try
			{
				return Json(_uncategorizedMovieRepository.GetAll().Select(x => new {
					x.ID,
                    x.EncodedFile,
                    OriginalFile = Path.GetFileName(x.OriginalFile)                       		
				}), JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Error("Error during UncategorizedMovieController.GetUncategorizedMovies.", ex);
				throw;
			}
		}

		/// <summary>
		/// Categorizes a movie.
		/// </summary>
		public void CategorizeMovie(int uncategorizedMovieID, int categorizedMovieID)
		{
			try
			{
				if (uncategorizedMovieID < 1)
					throw new ArgumentOutOfRangeException("uncategorizedMovieID");
				if (categorizedMovieID < 1)
					throw new ArgumentOutOfRangeException("categorizedMovieID");

				var details = _remoteMovieRepository.GetDetails(categorizedMovieID);
			}
			catch (Exception ex)
			{
				Logger.Error("Error during UncategorizedMovieController.CategorizeMovie", ex);
				throw;
			}
		}
		#endregion
	}
}