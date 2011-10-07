using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ShowveoService.Entities;
using ShowveoService.Service.Logging;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// A base encoder using Handbrake.
	/// </summary>
	public class HandbrakeEncoder
	{
		#region Data Members
		/// <summary>
		/// The percentage complete as recorded the last time progress was received.
		/// </summary>
		private double _previousPercentage;
		#endregion

		#region Public Methods
		/// <summary>
		/// Encodes a video and audio file.
		/// </summary>
		/// <param name="id">An ID used to track the item being encoded.</param>
		/// <param name="file">The location of the file to encode.</param>
		/// <param name="command">The command used to tell Handbrake how to encode the video.</param>
		/// <param name="progress">The callback function fired when progress of a file's encoding is updated. The action is given the percentage complete.</param>
		/// <param name="complete">The callback function fired when encoding is complete. The action is given the location of the encoded file.</param>
		/// <returns>An ID used to track the item being encoded.</returns>
		public void Encode(Guid id, string file, string command, Action<EncodingMovieTask, double> progress, Action<EncodingMovieTask, string> complete)
		{
			if (string.IsNullOrEmpty(file))
				throw new ArgumentNullException("file");
			if (!System.IO.File.Exists(file))
				throw new FileNotFoundException(file);

			command = string.Format(command, "\"" + file + "\"", id.ToString("N"));

			var task = new EncodingMovieTask {ID = id, File = file, PercentComplete = 0};

			Logger.Info("Beginning encoding: " + command);

			new Thread(() => {
				var process = new Process();
				process.StartInfo.FileName = @"c:\Code\showveoservice\ShowveoService.MVCApplication\Resources\HandbrakeCLI.exe";
				process.StartInfo.Arguments = command;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.OutputDataReceived += (sender, args) => OnDataReceived(progress, task, args);
				process.ErrorDataReceived += (sender, args) => OnErrorReceived(args);
				process.Exited += (sender, args) => complete.Invoke(task, task.ID.ToString("N") + ".mp4");
				process.Start();

				process.BeginOutputReadLine();

				while (!process.HasExited) {}
			}).Start();
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Fired after data has been received from an encoding operation
		/// </summary>
		private void OnDataReceived(Action<EncodingMovieTask, double> progress, EncodingMovieTask task, DataReceivedEventArgs e)
		{
			if (e.Data == null || !e.Data.Contains("%"))
				return;

			var begin = e.Data.IndexOf(",") + 1;
			var end = e.Data.IndexOf("%") - 1;
			var newPercent = Convert.ToDouble(e.Data.Substring(begin, end - begin).Replace(" ", ""));
			progress.Invoke(task, newPercent - _previousPercentage);

			_previousPercentage = newPercent;
		}

		/// <summary>
		/// Fired after error data has been received from an encoding operation.
		/// </summary>
		private void OnErrorReceived(DataReceivedEventArgs e)
		{
			Logger.Warn("Error during encoding operation: " + e.Data);
		}
		#endregion
	}
}