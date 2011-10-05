using System.Collections.Generic;

namespace ShowveoService.Entities
{
	public class Genre
	{
		public virtual int ID { get; set; }
		public virtual string Name { get; set; }
		public virtual IEnumerable<Movie> Movies { get; set; }
	}
}