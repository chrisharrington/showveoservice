using System;

namespace ShowveoService.Service.File
{
	/// <summary>
	/// Defines a class used to watch a folder for movie file additions.
	/// </summary>
	public interface IFolderWatcher
	{
		#region Public Methods
		/// <summary>
		/// Watches a folder for additions of specific file types.
		/// </summary>
		/// <param name="folder">The folder to watch.</param>
		/// <param name="callback">The callback function to execute with added file information.</param>
		/// <param name="extensions">The file extensions to watch for.</param>
		void WatchForAdditions(string folder, Action<string> callback, params string[] extensions);
		#endregion
	}
}