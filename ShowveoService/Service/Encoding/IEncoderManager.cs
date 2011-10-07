using System;
using System.Collections.Generic;
using ShowveoService.Entities;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// Manages encoding of video and audio files.
	/// </summary>
	public interface IEncoderManager
	{
		#region Public Methods
		/// <summary>
		/// Encodes a file.
		/// </summary>
		/// <param name="file">The file to encode.</param>
		/// <returns>The ID of the encoding task.</returns>
		void Encode(string file);

		/// <summary>
		/// Retrieves a collection of all currently encoding movie information.
		/// </summary>
		/// <returns>The information for all currently encoding movies.</returns>
		IEnumerable<EncodingMovieTask> GetAllTasks();
		#endregion
	}
}