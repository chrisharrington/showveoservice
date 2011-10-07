using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// A class used to store the progress of ongoing encoding operations.
	/// </summary>
	public static class EncodingProgressContainer
	{
		#region Data Members
		/// <summary>
		/// The underlying progress collection.
		/// </summary>
		private static readonly ConcurrentDictionary<Guid, EncodingMovieTask> _progress;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		static EncodingProgressContainer()
		{
			_progress = new ConcurrentDictionary<Guid, EncodingMovieTask>();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds or updates the encoding movie task to the progress list.
		/// </summary>
		/// <param name="task">The task to add or update.</param>
		/// <param name="percentChanged">The amount by which the task has completed.</param>
		public static void AddOrUpdate(EncodingMovieTask task, double percentChanged = 0)
		{
			if (!_progress.ContainsKey(task.ID))
				_progress[task.ID] = task;

			_progress[task.ID].PercentComplete += percentChanged;
			if (_progress[task.ID].PercentComplete > 100)
				_progress[task.ID].PercentComplete = 100;
		}

		/// <summary>
		/// Gets an encoding movie task by ID.
		/// </summary>
		/// <param name="id">The ID of the encoding movie task.</param>
		/// <returns>The retrieved encoding movie task.</returns>
		public static EncodingMovieTask Get(Guid id)
		{
			EncodingMovieTask task;
			if (!_progress.TryGetValue(id, out task))
				throw new ArgumentException("The ID \"" + id + "\" corresponds to no stored encoding movie task.");
			return task;
		}

		/// <summary>
		/// Gets a collection of all ongoing movie encoding tasks.
		/// </summary>
		/// <returns>The list of all encoding movie tasks.</returns>
		public static IEnumerable<EncodingMovieTask> GetAll()
		{
			return _progress.Select(x => x.Value);
		}
		#endregion
	}
}