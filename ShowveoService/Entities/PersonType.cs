using System.Collections.Generic;

namespace ShowveoService.Entities
{
	/// <summary>
	/// An entity representing the type a person, in the context of a movie or TV show (director, actor, etc.).
	/// </summary>
	public class PersonType
	{
		#region Properties
		public virtual int ID { get; set; }
		public virtual string Name { get; set; }

		public virtual IEnumerable<Person> People { get; set; }
		#endregion
	}
}