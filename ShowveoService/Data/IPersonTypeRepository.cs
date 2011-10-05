using ShowveoService.Entities;

namespace ShowveoService.Data
{
	/// <summary>
	/// Defines a container for person types.
	/// </summary>
	public interface IPersonTypeRepository
	{
		#region Public Methods
		/// <summary>
		/// Inserts a person type into the repository.
		/// </summary>
		/// <param name="personType">The person type to insert.</param>
		void Insert(PersonType personType);
		#endregion
	}
}