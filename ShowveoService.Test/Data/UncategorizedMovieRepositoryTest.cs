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
	/// Tests the UncategorizedMovieRepository class.
	/// </summary>
	[TestFixture]
	public class UncategorizedMovieRepositoryTest : DataTest
	{
		#region Data Members
		/// <summary>
		/// The subject under test.
		/// </summary>
		private IUncategorizedMovieRepository _sut;
		#endregion

		#region Setup
		/// <summary>
		/// Sets up the test.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			SessionProvider.CurrentSession = InMemorySession;
			_sut = new UncategorizedMovieRepository();
		}
		#endregion

		#region Tests
		/// <summary>
		/// Tests that the Insert method fails gracefully when given an invalid uncategorized movie.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidUncategorizedMovieOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(null));
		}

		/// <summary>
		/// Tests that the Insert method successfully inserts a uncategorized movie by calling Insert, then querying the session
		/// to ensure that the uncategorized movie was actually inserted.
		/// </summary>
		[Test]
		public void ShouldRetrieveInsertedUncategorizedMovieAfterInsert()
		{
			var uncategorizedMovie = new UncategorizedMovie {EncodedFile = "bromance"};
			_sut.Insert(uncategorizedMovie);

			var retrieved = InMemorySession.Query<UncategorizedMovie>().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().EncodedFile, uncategorizedMovie.EncodedFile);
		}

		/// <summary>
		/// Tests that the GetAll method successfully retrieves previously inserted uncategorized movies.
		/// </summary>
		[Test]
		public void ShouldRetrievePreviouslyInsertedUncategorizedMoviesOnGetAll()
		{
			var uncategorizedMovie = new UncategorizedMovie { EncodedFile = "bromance" };
			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(uncategorizedMovie);
				transaction.Commit();
			}

			var retrieved = _sut.GetAll().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().EncodedFile, uncategorizedMovie.EncodedFile);
		}
		#endregion
	}
}