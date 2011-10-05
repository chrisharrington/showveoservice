using System;
using ShowveoService.Entities;
using ShowveoService.Service.Configuration;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// Encodes video and audio files for viewing on a phone using Handbrake.
	/// </summary>
	public class PhoneHandbrakeEncoder : HandbrakeEncoder, IEncoder
	{
		#region Data Members
		/// <summary>
		/// The command to give to Handbrake describing how to encode for a phone.
		/// </summary>
		private readonly string _phoneHandbrakeCommand;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="configuration">The application configuration.</param>
		public PhoneHandbrakeEncoder(IConfigurationProvider configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException("configuration");

			_phoneHandbrakeCommand = configuration.PhoneHandbrakeCommand;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Encodes a video and audio file.
		/// </summary>
		/// <param name="file">The location of the file to encode.</param>
		/// <param name="progress">The callback function fired when progress of a file's encoding is updated. The action is given the percentage complete.</param>
		/// <param name="complete">The callback function fired when encoding is complete. The action is given the location of the encoded file.</param>
		public Guid Encode(string file, Action<EncodingMovieTask> progress, Action<EncodingMovieTask, string> complete)
		{
			return Encode(file, _phoneHandbrakeCommand, progress, complete);
		}
		#endregion
	}
}