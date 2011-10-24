﻿using System;
using ShowveoService.Entities;
using ShowveoService.Service.Configuration;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// Encodes video and audio files for viewing on a tablet using Handbrake.
	/// </summary>
	public class TabletHandbrakeEncoder : HandbrakeEncoder, IEncoder
	{
		#region Data Members
		/// <summary>
		/// The command to give to Handbrake describing how to encode for a tablet.
		/// </summary>
		private readonly string _tabletHandbrakeCommand;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="configuration">The application configuration.</param>
		public TabletHandbrakeEncoder(IConfigurationProvider configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException("configuration");

			_tabletHandbrakeCommand = configuration.TabletHandbrakeCommand;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Encodes a video and audio file.
		/// </summary>
		/// <param name="id">An ID used to track the item being encoded.</param>			
		/// <param name="file">The location of the file to encode.</param>
		/// <param name="progress">The callback function fired when progress of a file's encoding is updated. The action is given the percentage complete.</param>
		/// <param name="complete">The callback function fired when encoding is complete. The action is given the location of the encoded file.</param>
		public void Encode(Guid id, string file, Action<EncodingMovieTask, double> progress, Action<EncodingMovieTask> complete)
		{
			Encode(id, Preset.Tablet, file, _tabletHandbrakeCommand, progress, complete);
		}
		#endregion
	}
}