using FluentNHibernate.Mapping;
using ShowveoService.Entities;

namespace ShowveoService.Data.Maps
{
	/// <summary>
	/// The entity map for the UserMovie class.
	/// </summary>
	public class UserMovieMap : ClassMap<UserMovie>
	{
		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		public UserMovieMap()
		{
			Id(x => x.ID).GeneratedBy.Identity();

			Map(x => x.IsFavorite).Not.Nullable();
			Map(x => x.LastWatched);

			References(x => x.Movie).Not.Nullable();
			References(x => x.User).Not.Nullable();
		}
		#endregion
	}
}