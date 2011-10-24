using ShowveoService.Entities;

namespace ShowveoService.Web.Push
{
	/// <summary>
	/// Defines a class used to push encoding progress information.
	/// </summary>
	public interface IEncodingProgressPusher
	{
		#region Public Methods
		/// <summary>
		/// Pushes information regarding the progress of an encoding movie task.
		/// </summary>
		/// <param name="task">The encoding movie task.</param>
		void Push(EncodingMovieTask task);
		#endregion
	}
}