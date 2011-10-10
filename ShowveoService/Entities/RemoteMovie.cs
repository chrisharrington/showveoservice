using System;

namespace ShowveoService.Entities
{
	/// <summary>
	/// Represents movie information retrieved from a remote source.
	/// </summary>
	public class RemoteMovie
	{
		#region Properties
		public int ID { get; set; }
		public string Title { get; set; }
		public string PosterLocation { get; set; }
		#endregion
	}
}