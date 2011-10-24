using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShowveoService.Data;
using ShowveoService.Entities;
using ShowveoService.Service.Configuration;
using ShowveoService.Service.Logging;
using ShowveoService.Service.Presets;

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

		/// <summary>
		/// A container for movie information.
		/// </summary>
		private readonly IMovieRepository _movieRepository;

		/// <summary>
		/// The base movie location containing all movies indexed by ID.
		/// </summary>
		private readonly string _baseMovieLocation;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="userMovieRepository">A container for user-movie information.</param>
		/// <param name="movieRepository">A container for movie information.</param>
		/// <param name="configurationProvider">The application configuration.</param>
		public MovieController(IUserMovieRepository userMovieRepository, IMovieRepository movieRepository,
			IConfigurationProvider configurationProvider)
		{
			if (userMovieRepository == null)
				throw new ArgumentNullException("userMovieRepository");
			if (movieRepository == null)
				throw new ArgumentNullException("movieRepository");
			if (configurationProvider == null)
				throw new ArgumentNullException("configurationProvider");
			if (configurationProvider.EncodedMovieLocation == null)
				throw new ArgumentNullException("configurationProvider.EncodedMovieLocation");

			_userMovieRepository = userMovieRepository;
			_movieRepository = movieRepository;
			_baseMovieLocation = configurationProvider.PublicMovieLocation;
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
		/// Plays a movie.
		/// </summary>
		/// <param name="type">The type of movie to play. Phone, tablet or tv.</param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult PlayMovie(Preset? type, int? id)
		{
			try
			{
				if (type == null)
					throw new HttpException(404, "The given preset type is invalid.");
				if (id == null)
					throw new HttpException(404, "The movie ID is invalid.");

				var movie = _movieRepository.GetAll().Where(x => x.ID == id).FirstOrDefault();
				if (movie == null)
					throw new HttpException(404, "The movie ID \"" + id + "\" corresponds to no movie.");

				return Redirect(_baseMovieLocation + movie.FileLocation + ".mp4" + PresetIndicator.Get(type.Value));
			}
			catch (Exception ex)
			{
				Logger.Error("Error during MovieController.PlayMovie.", ex);
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
						Actors = x.Movie.Actors.Select(y => y.FirstName + " " + y.LastName),
						Genres = x.Movie.Genres.Select(y => new { y.ID, y.Name }),
						x.Movie.PosterLocation,
						x.Movie.Year.Year,
						DateAdded = x.Movie.DateAdded.ToString("u"),
						Director = x.Movie.Director == null ? "" : (x.Movie.Director.FirstName + " " + x.Movie.Director.LastName)
					},
					x.IsFavorite,
					x.LastWatched
				});
		}
		#endregion
	}
}