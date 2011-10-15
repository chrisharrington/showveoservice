using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ShowveoService.Data;
using ShowveoService.Service.Encoding;
using ShowveoService.Service.Logging;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to retrieve movie information.
	/// </summary>
	public class MovieController : Controller
	{
		#region Data Members
		/// <summary>
		/// A container for movie information.
		/// </summary>
		private readonly IMovieRepository _movieRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="movieRepository">A container for movie information.</param>
		public MovieController(IMovieRepository movieRepository)
		{
			if (movieRepository == null)
				throw new ArgumentNullException("movieRepository");

			_movieRepository = movieRepository;
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
				return Json(_movieRepository.GetAll().ToArray().Select(x => new
				{
					x.ID,
					x.Name,
					x.Description,
					Cast = x.Actors.Select(y => new { y.ID, y.FirstName, y.LastName }),
					Genres = x.Genres.Select(y => new { y.ID, y.Name }),
					x.PosterLocation,
					x.Year,
					x.DateAdded
				}), JsonRequestBehavior.AllowGet);
			} catch (Exception ex)
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
				return Json(_movieRepository.GetAll().OrderByDescending(x => x.DateAdded).ToArray().Select(x => new
				{
					x.ID,
					x.Name,
					x.Description,
					Cast = x.Actors.Select(y => new { y.ID, y.FirstName, y.LastName }),
					Genres = x.Genres.Select(y => new { y.ID, y.Name }),
					x.PosterLocation,
					x.Year,
					x.DateAdded
				}), JsonRequestBehavior.AllowGet);
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
	}
}