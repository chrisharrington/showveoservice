using System;

namespace ShowveoService.Entities
{
	/// <summary>
	/// An entity representing the information specific to a user and a movie, like favorite status, last watched, etc.
	/// </summary>
	public class UserMovie
	{
		#region Properties
		public virtual int ID { get; set; }
		public virtual bool IsFavorite { get; set; }
		public virtual DateTime? LastWatched { get; set; }
		public virtual Movie Movie { get; set; }
		public virtual User User { get; set; }
		#endregion
	}
}