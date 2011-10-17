using FluentNHibernate.Mapping;
using ShowveoService.Entities;

namespace ShowveoService.Data.Maps
{
	/// <summary>
	/// The entity map for the Movie class.
	/// </summary>
	public class MovieMap : ClassMap<Movie>
	{
		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		public MovieMap()
		{
			Id(x => x.ID).GeneratedBy.Identity();

			Map(x => x.Description).Not.Nullable().Length(10000);
			Map(x => x.Name).Not.Nullable();
			Map(x => x.Year).Not.Nullable();
			Map(x => x.PosterLocation);
			Map(x => x.DateAdded).Not.Nullable();
			Map(x => x.FileLocation).Not.Nullable();

			References(x => x.Director);

			HasManyToMany(x => x.Producers).Table("MoviesToPeople").Cascade.SaveUpdate();
			HasManyToMany(x => x.Actors).Table("MoviesToPeople").Cascade.SaveUpdate();
			HasManyToMany(x => x.Genres).Table("MoviesToGenres").Cascade.SaveUpdate();
		}
		#endregion
	}
}