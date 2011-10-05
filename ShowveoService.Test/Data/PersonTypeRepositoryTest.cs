using System;
using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;
using ShowveoService.Data;
using ShowveoService.Data.Repositories;
using ShowveoService.Entities;

namespace ShowveoService.Test.Data
{
	/// <summary>
	/// Tests the PersonTypeRepository class.
	/// </summary>
	[TestFixture]
	public class PersonTypeRepositoryTest : DataTest
	{
		#region Data Members
		/// <summary>
		/// The subject under test.
		/// </summary>
		private IPersonTypeRepository _sut;
		#endregion

		#region Setup
		/// <summary>
		/// Sets up the tests.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			SessionProvider.CurrentSession = InMemorySession;
			_sut = new PersonTypeRepository();
		}
		#endregion

		#region Tests
		/// <summary>
		/// Tests that the Insert method fails gracefully when given an invalid person type.
		/// </summary>
		[Test]
		public void ShouldFailWithNullPersonTypeOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(null));
		}

		/// <summary>
		/// Tests that the Insert method successfully inserts a person type into the repository.
		/// </summary>
		[Test]
		public void ShouldSuccessfullyInsertAPersonTypeOnInsert()
		{
			var personType = new PersonType {Name = "dolly grip"};
			_sut.Insert(personType);

			var retrieved = InMemorySession.Query<PersonType>().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().Name, personType.Name);
		}
		#endregion
	}
}