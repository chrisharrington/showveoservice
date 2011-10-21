using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using NHibernate.Linq;
using NUnit.Framework;
using ShowveoService.Data;
using ShowveoService.Data.Repositories;
using ShowveoService.Entities;

namespace ShowveoService.Test.Data
{
	/// <summary>
	/// Tests the UserMovieRepository class.
	/// </summary>
	[TestFixture]
	public class UserMovieRepositoryTest : DataTest
	{
		#region Data Members
		/// <summary>
		/// The subject under test.
		/// </summary>
		private IUserMovieRepository _sut;
		#endregion

		#region Setup
		/// <summary>
		/// Sets up the test.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			SessionProvider.CurrentSession = InMemorySession;
			_sut = new UserMovieRepository();
		}
		#endregion

		#region Tests
		/// <summary>
		/// Tests that the Insert method fails gracefully when given an invalid user-movie object.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidUserMovieOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(null));
		}

		/// <summary>
		/// Tests that the Insert method fails gracefully when given a user-movie object containing an invalid user.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidUserOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(new UserMovie { Movie = new Movie() }));
		}

		/// <summary>
		/// Tests that the Insert method fails gracefully when given a user-movie object containing an invalid movie.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidMovieOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(new UserMovie {User = new User()}));
		}

		/// <summary>
		/// Tests that the Insert method successfully inserts a user-movie object by calling Insert, then querying the session
		/// to ensure that the user-movie was actually inserted.
		/// </summary>
		[Test]
		public void ShouldRetrieveInsertedUserMovieAfterInsert()
		{
			var user = new User {FirstName = "blah", LastName = "lastname", EmailAddress = "email", Identity = "identity", Password = "password"};
			var movie = new Movie {Name = "boo", Year = DateTime.Now, DateAdded = DateTime.Now, FileLocation = "file", Description = "description"};

			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(user);
				InMemorySession.Save(movie);
				transaction.Commit();
			}

			var userMovie = new UserMovie {User = user, Movie = movie, IsFavorite = false};

			_sut.Insert(userMovie);

			var retrieved = InMemorySession.Query<UserMovie>().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().User.FirstName, user.FirstName);
			Assert.AreEqual(retrieved.First().Movie.Name, movie.Name);
		}

		/// <summary>
		/// Tests that the GetForUser method successfully retrieves previously inserted user-movie objects.
		/// </summary>
		[Test]
		public void ShouldRetrievePreviouslyInsertedUserMoviesOnGetAll()
		{
			var user = new User { FirstName = "blah", LastName = "lastname", EmailAddress = "email", Identity = "identity", Password = "password" };
			var movie = new Movie {Name = "boo", Year = DateTime.Now, DateAdded = DateTime.Now, FileLocation = "file", Description = "description"};

			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(user);
				InMemorySession.Save(movie);
				transaction.Commit();
			}

			var userMovie = new UserMovie {User = user, Movie = movie, IsFavorite = false};
			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(userMovie);
				transaction.Commit();
			}

			var retrieved = _sut.GetForUser(user).ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().User.FirstName, user.FirstName);
			Assert.AreEqual(retrieved.First().Movie.Name, movie.Name);
		}

		/// <summary>
		/// Tests that the GetForUser method returns no information when inserting user-movie objects with one user
		/// but retrieving with a second.
		/// </summary>
		[Test]
		public void ShouldRetrieveNothingOnGetForUserWithDifferentUser()
		{
			var first = new User { FirstName = "first", LastName = "lastname", EmailAddress = "email", Identity = "identity", Password = "password" };
			var second = new User { FirstName = "second", LastName = "lastname", EmailAddress = "email", Identity = "identity", Password = "password" };
			var movie = new Movie {Name = "boo", Year = DateTime.Now, DateAdded = DateTime.Now, FileLocation = "file", Description = "description"};

			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(first);
				InMemorySession.Save(second);
				InMemorySession.Save(movie);
				transaction.Commit();
			}

			var userMovie = new UserMovie { User = first, Movie = movie };
			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(userMovie);
				transaction.Commit();
			}

			var retrieved = _sut.GetForUser(second).ToArray();
			Assert.AreEqual(retrieved.Length, 0);
		}
		#endregion
	}
}