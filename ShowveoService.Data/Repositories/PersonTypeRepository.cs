using System;
using ShowveoService.Entities;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// A container for person type information.
	/// </summary>
	public class PersonTypeRepository : Repository, IPersonTypeRepository
	{
		#region Public Methods
		/// <summary>
		/// Inserts a person type into the repository.
		/// </summary>
		/// <param name="personType">The person type to insert.</param>
		public void Insert(PersonType personType)
		{
			if (personType == null)
				throw new ArgumentNullException("personType");

			CurrentSession.Save(personType);
		}
		#endregion
	}
}