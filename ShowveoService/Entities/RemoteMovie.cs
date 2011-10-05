using System;

namespace ShowveoService.Entities
{
	/// <summary>
	/// Represents movie information retrieved from a remote source.
	/// </summary>
	public class RemoteMovie
	{
		#region Properties
		public string Title { get; set; }
		public Uri Thumbnail { get; set; }
		public Uri Poster { get; set; }
		#endregion
	}
}