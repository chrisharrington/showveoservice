using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ShowveoService.Data;
using ShowveoService.Entities;
using ShowveoService.Service.Logging;
using ShowveoService.Web.Remote;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to provide access to uncategorized movies.
	/// </summary>
	public class UncategorizedMovieController : AuthenticatedController
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
		/// A container for user-movie information.
		/// </summary>
		private readonly IUserMovieRepository _userMovieRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="uncategorizedMovieRepository">A container for uncategorized movie information.</param>
		/// <param name="remoteMovieRepository">A container for remote movie information.</param>
		/// <param name="userUserMovieRepository">A container for user-movie information.</param>
		public UncategorizedMovieController(IUncategorizedMovieRepository uncategorizedMovieRepository, IRemoteMovieRepository remoteMovieRepository,
			IUserMovieRepository userUserMovieRepository)
		{
			if (uncategorizedMovieRepository == null)
				throw new ArgumentNullException("uncategorizedMovieRepository");
			if (remoteMovieRepository == null)
				throw new ArgumentNullException("remoteMovieRepository");
			if (userUserMovieRepository == null)
				throw new ArgumentNullException("userUserMovieRepository");

			_uncategorizedMovieRepository = uncategorizedMovieRepository;
			_remoteMovieRepository = remoteMovieRepository;
			_userMovieRepository = userUserMovieRepository;
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
		[HttpPost]
		public void CategorizeMovie(int uncategorizedMovieID, int categorizedMovieID)
		{
			try
			{
				if (uncategorizedMovieID < 1)
					throw new ArgumentOutOfRangeException("uncategorizedMovieID");
				if (categorizedMovieID < 1)
					throw new ArgumentOutOfRangeException("categorizedMovieID");

				var details = _remoteMovieRepository.GetDetails(categorizedMovieID);
				if (details == null)
					throw new InvalidOperationException("An error occurred while retrieving the details for the movie with ID " + categorizedMovieID + ".");

				var uncategorizedMovie = _uncategorizedMovieRepository.GetAll().Where(x => x.ID == uncategorizedMovieID).FirstOrDefault();
				if (uncategorizedMovie == null)
					throw new InvalidOperationException("The uncategorized movie ID " + uncategorizedMovieID + " corresponds to no uncategorized movie.");

				details.FileLocation = uncategorizedMovie.EncodedFile;

				_userMovieRepository.Insert(new UserMovie {IsFavorite = false, Movie = details, User = User});
				_uncategorizedMovieRepository.Remove(uncategorizedMovieID);
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