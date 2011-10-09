using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ShowveoService.Data;
using ShowveoService.Service.Logging;

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
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="uncategorizedMovieRepository">A container for uncategorized movie information.</param>
		public UncategorizedMovieController(IUncategorizedMovieRepository uncategorizedMovieRepository)
		{
			if (uncategorizedMovieRepository == null)
				throw new ArgumentNullException("uncategorizedMovieRepository");

			_uncategorizedMovieRepository = uncategorizedMovieRepository;
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
		#endregion
	}
}