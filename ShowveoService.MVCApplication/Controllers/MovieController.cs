using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ShowveoService.Data;
using ShowveoService.Entities;
using ShowveoService.Service.Encoding;
using ShowveoService.Service.Logging;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to retrieve movie information.
	/// </summary>
	public class MovieController : AuthenticatedController
	{
		#region Data Members
		/// <summary>
		/// A container for user-movie information.
		/// </summary>
		private readonly IUserMovieRepository _userMovieRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="userMovieRepository">A container for user-movie information.</param>
		public MovieController(IUserMovieRepository userMovieRepository)
		{
			if (userMovieRepository == null)
				throw new ArgumentNullException("userMovieRepository");

			_userMovieRepository = userMovieRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Retrieves a collection of all movies.
		/// </summary>
		/// <returns>The collection of all movies.</returns>
		public ActionResult GetAll()
		{
			try
			{
				return Json(SelectSubset(_userMovieRepository.GetForUser(User)), JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Error("Error during MovieController.GetAll.", ex);
				throw;
			}
		}

		/// <summary>
		/// Retrieves a collection of the latest movies.
		/// </summary>
		/// <returns>The collection of latest movies.</returns>
		public ActionResult GetLatest()
		{
			try
			{
				return Json(SelectSubset(_userMovieRepository.GetForUser(User).OrderByDescending(x => x.Movie.DateAdded)), JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Error("Error during MovieController.GetLatest.", ex);
				throw;
			}
		}

		/// <summary>
		/// Retrieves information pertaining to currently encoding movies.
		/// </summary>
		/// <returns>The encoding movie information.</returns>
		public ActionResult GetEncodingMovies()
		{
			try
			{
				return Json(EncodingProgressContainer.GetAll().Select(x => new { x.ID, File = Path.GetFileName(x.File), x.PercentComplete }), JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Error("Error during MovieController.GetEncodingMovies.", ex);
				throw;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Returns a serializable subset of the given collection of user-movie object.s
		/// </summary>
		/// <param name="usermovies">The collection of user-movie objects.</param>
		/// <returns></returns>
		private object SelectSubset(IEnumerable<UserMovie> usermovies)
		{
			return usermovies.Select(x => new {
					x.User,
					Movie = new {
						x.Movie.ID,
						x.Movie.Name,
						x.Movie.Description,
						Cast = x.Movie.Actors.Select(y => new { y.ID, y.FirstName, y.LastName }),
						Genres = x.Movie.Genres.Select(y => new { y.ID, y.Name }),
						x.Movie.PosterLocation,
						x.Movie.Year,
						x.Movie.DateAdded
					},
					x.IsFavorite,
					x.LastWatched
				});
		}
		#endregion
	}
}