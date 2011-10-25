﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		/// <summary>
		/// A container for encoding progress information.
		/// </summary>
		private readonly IEncodingProgressContainer _encodingProgressContainer;

		/// <summary>
		/// The queue for encoding tasks.
		/// </summary>
		private readonly IList<IList<Action>> _queue; 
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="factory">A factory used to create encoders.</param>
		/// <param name="uncategorizedMovieRepository">A container for uncategorized movie information.</param>
		/// <param name="encodingProgressContainer">A container for encoding progress information.</param>
		public EncoderManager(IEncoderFactory factory, IUncategorizedMovieRepository uncategorizedMovieRepository,
			IEncodingProgressContainer encodingProgressContainer)
		{
			if (factory == null)
				throw new ArgumentNullException("factory");
			if (uncategorizedMovieRepository == null)
				throw new ArgumentNullException("uncategorizedMovieRepository");
			if (encodingProgressContainer == null)
				throw new ArgumentNullException("encodingProgressContainer");

			_factory = factory;
			_uncategorizedMovieRepository = uncategorizedMovieRepository;
			_encodingProgressContainer = encodingProgressContainer;
			_queue = new List<IList<Action>>();
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

			var id = Guid.NewGuid();
			var tasks = _factory.CreateAll().Select(localEncoder => (Action) (() => localEncoder.Encode(id, file, OnProgressReceived, OnEncodingCompleted))).ToList();
			var task = new EncodingMovieTask {File = file, ID = id, PercentComplete = 0};
			_encodingProgressContainer.AddOrUpdate(task);

			if (_queue.Count() == 0)
				tasks.First().Invoke();

			_queue.Add(tasks);
		}

		/// <summary>
		/// Retrieves a collection of all currently encoding movie information.
		/// </summary>
		/// <returns>The information for all currently encoding movies.</returns>
		public IEnumerable<EncodingMovieTask> GetAllTasks()
		{
			return _encodingProgressContainer.GetAll();
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Fired after progress has been received for an encoding operation.
		/// </summary>
		/// <param name="task">The encoding movie task for which progress should be updated.</param>
		/// <param name="percentChanged">The amount by which the task has completed.</param>
		private void OnProgressReceived(EncodingMovieTask task, double percentChanged)
		{
			percentChanged /= _factory.EncoderCount;
			_encodingProgressContainer.AddOrUpdate(task, percentChanged);
		}

		/// <summary>
		/// Fired after an encoding task has been completed.
		/// </summary>
		/// <param name="task">The completed task.</param>
		private void OnEncodingCompleted(EncodingMovieTask task)
		{
			var tasks = _queue[0];
			tasks.RemoveAt(0);
			if (tasks.Count > 0)
			{
				tasks.First().Invoke();
				return;
			}

			_encodingProgressContainer.AddOrUpdate(task, 100);
			_uncategorizedMovieRepository.Insert(new UncategorizedMovie { OriginalFile = task.File, EncodedFile = task.ID.ToString("N") });

			_queue.RemoveAt(0);
			if (_queue.Count > 0)
			{
				tasks = _queue[0];
				tasks.First().Invoke();
			}
		}
		#endregion
	}
}