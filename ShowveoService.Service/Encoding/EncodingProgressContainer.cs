using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ShowveoService.Entities;
using ShowveoService.Web.Push;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// A class used to store the progress of ongoing encoding operations.
	/// </summary>
	public class EncodingProgressContainer : IEncodingProgressContainer
	{
		#region Data Members
		/// <summary>
		/// The underlying progress collection.
		/// </summary>
		private readonly ConcurrentDictionary<Guid, EncodingMovieTask> _progress;

		/// <summary>
		/// Pushes encoding progress updates to subscribed clients.
		/// </summary>
		private readonly IEncodingProgressPusher _pusher;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		public EncodingProgressContainer(IEncodingProgressPusher pusher)
		{
			if (pusher == null)
				throw new ArgumentNullException("pusher");

			_pusher = pusher;
			_progress = new ConcurrentDictionary<Guid, EncodingMovieTask>();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds or updates the encoding movie task to the progress list.
		/// </summary>
		/// <param name="task">The task to add or update.</param>
		/// <param name="percentChanged">The amount by which the task has completed.</param>
		public void AddOrUpdate(EncodingMovieTask task, double percentChanged = 0)
		{
			if (!_progress.ContainsKey(task.ID))
				_progress[task.ID] = task;

			_progress[task.ID].PercentComplete += percentChanged;
			if (_progress[task.ID].PercentComplete > 100)
				_progress[task.ID].PercentComplete = 100;

			_pusher.Push(_progress[task.ID]);
		}

		/// <summary>
		/// Gets an encoding movie task by ID.
		/// </summary>
		/// <param name="id">The ID of the encoding movie task.</param>
		/// <returns>The retrieved encoding movie task.</returns>
		public EncodingMovieTask Get(Guid id)
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
		public IEnumerable<EncodingMovieTask> GetAll()
		{
			var tasks = _progress.Select(x => x.Value);
			foreach (var task in _progress.Values.Where(x => x.PercentComplete == 100))
			{
				EncodingMovieTask output;
				_progress.TryRemove(task.ID, out output);
			}
			return tasks;
		}
		#endregion
	}
}