using System;
using System.Collections.Generic;

namespace ShowveoService.Entities
{
	/// <summary>
	/// An entity representing a container for movie information.
	/// </summary>
	public class Movie
	{
		#region Properties
		public virtual int ID { get; set; }
		public virtual string Name { get; set; }
		public virtual DateTime Year { get; set; }
		public virtual string Description { get; set; }
		public virtual string PosterLocation { get; set; }
		public virtual DateTime DateAdded { get; set; }
		public virtual string FileLocation { get; set; }

		public virtual IEnumerable<Person> Cast { get; set; }
		public virtual IEnumerable<Genre> Genres { get; set; } 
		#endregion
	}
}