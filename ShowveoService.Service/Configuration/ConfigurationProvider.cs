using System.Configuration;

namespace ShowveoService.Service.Configuration
{
	/// <summary>
	/// Provides configuration information for the application.
	/// </summary>
	public class ConfigurationProvider : IConfigurationProvider
	{
		#region Properties
		/// <summary>
		/// Retrieves the API key for accessing the public API for themoviedb.org.
		/// </summary>
		public string MovieDBAPIKey
		{
			get { return ConfigurationManager.AppSettings["MovieDBAPIKey"]; }
		}

		/// <summary>
		/// The location to place encoded movies.
		/// </summary>
		public string EncodedMovieLocation
		{
			get { return ConfigurationManager.AppSettings["EncodedMovieLocation"]; }
		}

		/// <summary>
		/// The publicly accessible movie location.
		/// </summary>
		public string PublicMovieLocation
		{
			get { return ConfigurationManager.AppSettings["PublicMovieLocation"]; }
		}

		/// <summary>
		/// The directory to watch for movies to encode.
		/// </summary>
		public string WatchedMovieLocation
		{
			get { return ConfigurationManager.AppSettings["WatchedMovieLocation"]; }
		}

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on a phone.
		/// </summary>
		public string PhoneHandbrakeCommand
		{
			get { return ConfigurationManager.AppSettings["PhoneHandbrakeCommand"]; }
		}

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on a tablet.
		/// </summary>
		public string TabletHandbrakeCommand
		{
			get { return ConfigurationManager.AppSettings["TabletHandbrakeCommand"]; }
		}

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on TV.
		/// </summary>
		public string TVHandbrakeCommand
		{
			get { return ConfigurationManager.AppSettings["TVHandbrakeCommand"]; }
		}

		/// <summary>
		/// The request url used to define a publisher.
		/// </summary>
		public string WebSyncRequestUrl
		{
			get { return ConfigurationManager.AppSettings["WebSyncRequestUrl"]; }
		}

		/// <summary>
		/// The channel used to determine the location to send a published message.
		/// </summary>
		public string EncodingChannel
		{
			get { return ConfigurationManager.AppSettings["EncodingChannel"]; }
		}
		#endregion
	}
}