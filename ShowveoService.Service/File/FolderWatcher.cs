using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ShowveoService.Service.File
{
	/// <summary>
	/// Watches a folder for file additions.
	/// </summary>
	public class FolderWatcher : IFolderWatcher
	{
		#region Public Methods
		/// <summary>
		/// Watches a folder for additions of specific file types.
		/// </summary>
		/// <param name="folder">The folder to watch.</param>
		/// <param name="callback">The callback function to execute with added file information.</param>
		/// <param name="extensions">The file extensions to watch for.</param>
		public void WatchForAdditions(string folder, Action<string> callback, params string[] extensions)
		{
			if (string.IsNullOrEmpty(folder))
				return;
			if (callback == null)
				return;
			if (extensions == null || extensions.Length == 0)
				return;

			var watcher = new FileSystemWatcher(folder);
			watcher.IncludeSubdirectories = true;
			watcher.Filter = "*.*";
			watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

			watcher.Created += (sender, e) => {
				var copied = false;
				do
				{
					try
					{
						using (System.IO.File.Open(e.FullPath, FileMode.Open))
						copied = true;
					}
					catch (IOException) {}
					Thread.Sleep(250);
				} while (!copied);

				//Thread.Sleep(10000);

				var extension = Path.GetExtension(e.FullPath);
				if (extensions.Contains(extension))
					callback.Invoke(e.FullPath);
			};

			watcher.EnableRaisingEvents = true;
		}
		#endregion
	}
}