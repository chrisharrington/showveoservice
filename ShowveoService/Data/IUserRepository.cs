using ShowveoService.Entities;

namespace ShowveoService.Data
{
	/// <summary>
	/// Defines a container for user information.
	/// </summary>
	public interface IUserRepository
	{
		#region Public Methods
		/// <summary>
		/// Attempts to authenticate a user. Null is returned if the authentication fails.
		/// </summary>
		/// <param name="emailAddress">The potential user's email address.</param>
		/// <param name="password">The potential user's encrypted password.</param>
		/// <returns>The authenticated user or null.</returns>
		User Authenticate(string emailAddress, string password);

		/// <summary>
		/// Authenticates a user using his or her identity string.
		/// </summary>
		/// <param name="identity">The user's identity string.</param>
		/// <returns>The authenticated user.</returns>
		User Authenticate(string identity);
		#endregion
	}
}