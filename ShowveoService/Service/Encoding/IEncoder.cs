using System;
using ShowveoService.Entities;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// Defines a class used to encode a video and audio file.
	/// </summary>
	public interface IEncoder
	{
		#region Public Methods
		/// <summary>
		/// Encodes a video and audio file.
		/// </summary>
		/// <param name="file">The location of the file to encode.</param>
		/// <param name="progress">The callback function fired when progress of a file's encoding is updated. The action is given the percentage complete.</param>
		/// <param name="complete">The callback function fired when encoding is complete. The action is given the location of the encoded file.</param>
		/// <returns>An ID used to track the item being encoded.</returns>
		Guid Encode(string file, Action<EncodingMovieTask> progress, Action<EncodingMovieTask, string> complete);
		#endregion
	}
}