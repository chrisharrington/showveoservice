using System;
using System.Collections.Generic;
using ShowveoService.Entities;

namespace ShowveoService.Service.Encoding
{
	public interface IEncodingProgressContainer
	{
		/// <summary>
		/// Adds or updates the encoding movie task to the progress list.
		/// </summary>
		/// <param name="task">The task to add or update.</param>
		/// <param name="percentChanged">The amount by which the task has completed.</param>
		void AddOrUpdate(EncodingMovieTask task, double percentChanged = 0);

		/// <summary>
		/// Gets an encoding movie task by ID.
		/// </summary>
		/// <param name="id">The ID of the encoding movie task.</param>
		/// <returns>The retrieved encoding movie task.</returns>
		EncodingMovieTask Get(Guid id);

		/// <summary>
		/// Gets a collection of all ongoing movie encoding tasks.
		/// </summary>
		/// <returns>The list of all encoding movie tasks.</returns>
		IEnumerable<EncodingMovieTask> GetAll();
	}
}