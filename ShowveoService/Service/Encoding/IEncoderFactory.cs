using System.Collections.Generic;
using ShowveoService.Entities;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// Defines a factory used to create encoding classes.
	/// </summary>
	public interface IEncoderFactory
	{
		#region Properties
		/// <summary>
		/// Returns the number of available encoders.
		/// </summary>
		int EncoderCount { get; }
		#endregion

		#region Public Methods
		/// <summary>
		/// Creates an encoder based on a preset.
		/// </summary>
		/// <param name="preset">The preset used to generate the appropriate encoder.</param>
		/// <returns>The created encoder.</returns>
		IEncoder Create(Preset preset);

		/// <summary>
		/// Creates encoders for all presets.
		/// </summary>
		/// <returns>The collection of all encoders.</returns>
		IEnumerable<IEncoder> CreateAll();
		#endregion
	}
}