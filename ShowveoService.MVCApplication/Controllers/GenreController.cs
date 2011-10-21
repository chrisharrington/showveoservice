using System;
using System.Linq;
using System.Web.Mvc;
using ShowveoService.Data;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to provide genre specific control.
	/// </summary>
	public class GenreController : Controller
	{
		#region Data Members
		/// <summary>
		/// A container for genre information.
		/// </summary>
		private readonly IGenreRepository _genreRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="genreRepository">A container for genre information.</param>
		public GenreController(IGenreRepository genreRepository)
		{
			if (genreRepository == null)
				throw new ArgumentNullException("genreRepository");

			_genreRepository = genreRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Retrieves all genres.
		/// </summary>
		/// <returns>The collection of all genres.</returns>
		[HttpGet]
		public ActionResult GetAll()
		{
			return Json(_genreRepository.GetAll().Select(x => new { x.ID, x.Name }), JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}