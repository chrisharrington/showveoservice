using FluentNHibernate.Mapping;
using ShowveoService.Entities;

namespace ShowveoService.Data.Maps
{
	public class GenreMap : ClassMap<Genre>
	{
		 public GenreMap()
		 {
		 	Id(x => x.ID).GeneratedBy.Identity();

		 	Map(x => x.Name).Not.Nullable();

		 	HasManyToMany(x => x.Movies).Table("MoviesToGenres");
		 }
	}
}