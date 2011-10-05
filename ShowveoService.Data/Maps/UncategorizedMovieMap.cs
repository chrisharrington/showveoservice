using FluentNHibernate.Mapping;
using ShowveoService.Entities;

namespace ShowveoService.Data.Maps
{
	public class UncategorizedMovieMap : ClassMap<UncategorizedMovie>
	{
		 public UncategorizedMovieMap()
		 {
			Id(x => x.ID);

		 	Map(x => x.EncodedFile).Not.Nullable();
		 	Map(x => x.OriginalFile).Not.Nullable();
		 }
	}
}