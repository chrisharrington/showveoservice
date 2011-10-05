namespace ShowveoService.Service.Configuration
{
	/// <summary>
	/// Defines the configuration for the application.
	/// </summary>
	public interface IConfigurationProvider
	{
		#region Properties
		/// <summary>
		/// Retrieves the API key for accessing the public API for themoviedb.org.
		/// </summary>
		string MovieDBAPIKey { get; }

		/// <summary>
		/// The location to place encoded movies.
		/// </summary>
		string EncodedMovieLocation { get; }

		/// <summary>
		/// The directory to watch for movies to encode.
		/// </summary>
		string WatchedMovieLocation { get; }

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on a phone.
		/// </summary>
		string PhoneHandbrakeCommand { get; }

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on a tablet.
		/// </summary>
		string TabletHandbrakeCommand { get; }

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on TV.
		/// </summary>
		string TVHandbrakeCommand { get; }
		#endregion
	}
}