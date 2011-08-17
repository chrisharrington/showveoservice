using FluentNHibernate.Mapping;
using ShowveoService.Entities;

namespace ShowveoService.Data.Maps
{
	/// <summary>
	/// Maps the NHibernate User entity.
	/// </summary>
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Id(x => x.ID).GeneratedBy.Assigned();

			Map(x => x.EmailAddress).Not.Nullable();
			Map(x => x.FirstName).Not.Nullable();
			Map(x => x.Identity, "[Identity]").Not.Nullable();
			Map(x => x.LastName).Not.Nullable();
			Map(x => x.Password).Not.Nullable();
		}
	}
}