﻿using System;
using System.Collections.Generic;
using System.IO;
using ShowveoService.Data;
using ShowveoService.Entities;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// Manages the encoding of video and audio files.
	/// </summary>
	public class EncoderManager : IEncoderManager
	{
		#region Data Members
		/// <summary>
		/// A factory used to create encoders.
		/// </summary>
		private readonly IEncoderFactory _factory;

		/// <summary>
		/// A container for uncategorized movie information.
		/// </summary>
		private readonly IUncategorizedMovieRepository _uncategorizedMovieRepository;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="factory">A factory used to create encoders.</param>
		/// <param name="uncategorizedMovieRepository">A container for uncategorized movie information.</param>
		public EncoderManager(IEncoderFactory factory, IUncategorizedMovieRepository uncategorizedMovieRepository)
		{
			if (factory == null)
				throw new ArgumentNullException("factory");
			if (uncategorizedMovieRepository == null)
				throw new ArgumentNullException("uncategorizedMovieRepository");

			_factory = factory;
			_uncategorizedMovieRepository = uncategorizedMovieRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Encodes a file.
		/// </summary>
		/// <param name="file">The file to encode.</param>
		/// <returns>The ID of the encoding task.</returns>
		public void Encode(string file)
		{
			if (string.IsNullOrEmpty(file))
				throw new ArgumentNullException("file");
			if (!System.IO.File.Exists(file))
				throw new FileNotFoundException(file);

			var id = _factory.Create(EncodingPreset.Phone).Encode(file, OnProgressReceived, OnEncodingCompleted);
			var task = new EncodingMovieTask {File = file, ID = id, PercentComplete = 0};
			EncodingProgressContainer.AddOrUpdate(task);
		}

		/// <summary>
		/// Gets the percentage complete of an encoding task.
		/// </summary>
		/// <param name="id">The ID of the encoding task to check.</param>
		/// <returns>The percentage complete.</returns>
		public EncodingMovieTask GetProgress(Guid id)
		{
			return EncodingProgressContainer.Get(id);
		}

		/// <summary>
		/// Retrieves a collection of all currently encoding movie information.
		/// </summary>
		/// <returns>The information for all currently encoding movies.</returns>
		public IEnumerable<EncodingMovieTask> GetAllTasks()
		{
			return EncodingProgressContainer.GetAll();
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Fired after progress has been received for an encoding operation.
		/// </summary>
		/// <param name="task">The encoding movie task for which progress should be updated.</param>
		private void OnProgressReceived(EncodingMovieTask task)
		{
			EncodingProgressContainer.AddOrUpdate(task);
		}

		/// <summary>
		/// Fired after an encoding task has been completed.
		/// </summary>
		/// <param name="task">The completed task.</param>
		/// <param name="filename">The name of the encoded file.</param>
		private void OnEncodingCompleted(EncodingMovieTask task, string filename)
		{
			task.PercentComplete = 100;
			EncodingProgressContainer.AddOrUpdate(task);
			_uncategorizedMovieRepository.Insert(new UncategorizedMovie {OriginalFile = task.File, EncodedFile = filename});
		}
		#endregion
	}
}