using System;
using System.Web.Mvc;
using ShowveoService.Data;

namespace ShowveoService.MVCApplication.Controllers
{
	/// <summary>
	/// A controller used to authenticate a user.
	/// </summary>
	public class UserController : Controller
	{
		#region Data Members
		/// <summary>
		/// A container for user information.
		/// </summary>
		private readonly IUserRepository _userRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="userRepository">A container for user information.</param>
		public UserController(IUserRepository userRepository)
		{
			if (userRepository == null)
				throw new ArgumentNullException("userRepository");
			
			_userRepository = userRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Attempts to authenticate a user.
		/// </summary>
		/// <param name="emailAddress">The potential user's email address.</param>
		/// <param name="password">The potential user's encrypted password.</param>
		/// <returns>The authenticated user or null.</returns>
		public ActionResult Authenticate(string emailAddress, string password)
		{
			return Json(_userRepository.Authenticate(emailAddress, password), JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}