using ShowveoService.Service.Configuration;

namespace ShowveoService.Test
{
	public class TestConfigurationProvider : IConfigurationProvider
	{
		/// <summary>
		/// Retrieves the API key for accessing the public API for themoviedb.org.
		/// </summary>
		public string MovieDBAPIKey
		{
			get { return "c26c67ed161834067f4d91430df1024e"; }
		}

		/// <summary>
		/// The location to place encoded movies.
		/// </summary>
		public string EncodedMovieLocation
		{
			get { return @"e:\Media\Showveo\Encoded\"; }
		}

		/// <summary>
		/// The directory to watch for movies to encode.
		/// </summary>
		public string WatchedMovieLocation
		{
			get { return @"e:\Media\Showveo\Movies"; }
		}

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on a phone.
		/// </summary>
		public string PhoneHandbrakeCommand
		{
			get { return @"-i {0} -o e:\Media\Showveo\Encoded\{1}.mp4 -e x264 -S 500"; }
		}

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on a tablet.
		/// </summary>
		public string TabletHandbrakeCommand
		{
			get { return @"-i {0} -o e:\Media\Showveo\Encoded\{1}.mp4 -e x264 -S 1000"; }
		}

		/// <summary>
		/// The command used by Handbrake describing how to encode for viewing on TV.
		/// </summary>
		public string TVHandbrakeCommand
		{
			get { return @"-i {0} -o e:\Media\Showveo\Encoded\{1}.mp4 -e x264"; }
		}
	}
}