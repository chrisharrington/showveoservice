using System.Collections.Generic;

namespace ShowveoService.Entities
{
	/// <summary>
	/// An entity representing a person associated with a TV show or movie, such as an actor or producer.
	/// </summary>
	public class Person
	{
		#region Properties
		public virtual int ID { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual PersonType Job { get; set; }
		public virtual IEnumerable<Movie> Movies { get; set; }
		#endregion
	}
}