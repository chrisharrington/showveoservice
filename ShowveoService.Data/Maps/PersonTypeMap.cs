using FluentNHibernate.Mapping;
using ShowveoService.Entities;

namespace ShowveoService.Data.Maps
{
	/// <summary>
	/// The entity map for the PersonType class.
	/// </summary>
	public class PersonTypeMap : ClassMap<PersonType>
	{
		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		public PersonTypeMap()
		{
			Id(x => x.ID).GeneratedBy.Identity();

			Map(x => x.Name).Not.Nullable();
		}
		#endregion
	}
}