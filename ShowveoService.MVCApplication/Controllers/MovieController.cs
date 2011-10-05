using System;
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

		/// <summary>
		/// A container for uncategorized movie information.
		/// </summary>
		private readonly IUncategorizedMovieRepository _uncategorizedMovieRepository;

		/// <summary>
		/// Contains information on currently encoding movies.
		/// </summary>
		private readonly IEncoderManager _encoderManager;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="movieRepository">A container for movie information.</param>
		/// <param name="uncategorizedMovieRepository">A container for uncategorized movie information.</param>
		/// <param name="encoderManager">Contains information on currently encoding movies.</param>
		public MovieController(IMovieRepository movieRepository, IUncategorizedMovieRepository uncategorizedMovieRepository,
			IEncoderManager encoderManager)
		{
			if (movieRepository == null)
				throw new ArgumentNullException("movieRepository");
			if (uncategorizedMovieRepository == null)
				throw new ArgumentNullException("uncategorizedMovieRepository");
			if (encoderManager == null)
				throw new ArgumentNullException("encoderManager");

			_movieRepository = movieRepository;
			_uncategorizedMovieRepository = uncategorizedMovieRepository;
			_encoderManager = encoderManager;
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
					Cast = x.Cast.Select(y => new { y.ID, y.FirstName, y.LastName, Job = new { y.Job.ID, y.Job.Name } }),
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
					Cast = x.Cast.Select(y => new { y.ID, y.FirstName, y.LastName, Job = new { y.Job.ID, y.Job.Name } }),
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
				Logger.Error("Error during MovieController.GetUncategorizedMovieCount.", ex);
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
				return Json(_encoderManager.GetAllTasks(), JsonRequestBehavior.AllowGet);
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