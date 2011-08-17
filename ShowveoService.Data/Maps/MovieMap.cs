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

			Map(x => x.Description).Not.Nullable();
			Map(x => x.Name).Not.Nullable();
			Map(x => x.Year).Not.Nullable();

			HasManyToMany(x => x.Cast).Table("MoviesToPeople");
		}
		#endregion
	}
}