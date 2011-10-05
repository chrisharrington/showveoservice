using FluentNHibernate.Mapping;
using ShowveoService.Entities;

namespace ShowveoService.Data.Maps
{
	/// <summary>
	/// The entity map for the Person class.
	/// </summary>
	public class PersonMap : ClassMap<Person>
	{
		public PersonMap()
		{
			Id(x => x.ID).GeneratedBy.Identity();

			Map(x => x.FirstName).Not.Nullable();
			Map(x => x.LastName).Not.Nullable();

			References(x => x.Job);

			HasManyToMany(x => x.Movies).Table("MoviesToPeople");
		}
	}
}