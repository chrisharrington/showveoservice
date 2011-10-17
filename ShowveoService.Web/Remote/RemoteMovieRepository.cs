using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using ShowveoService.Data;
using ShowveoService.Entities;
using ShowveoService.Service.Configuration;
using ShowveoService.Service.Logging;

namespace ShowveoService.Web.Remote
{
	/// <summary>
	/// A container for remote movie information.
	/// </summary>
	public class RemoteMovieRepository : IRemoteMovieRepository
	{
		#region Data Members
		/// <summary>
		/// A container for person information.
		/// </summary>
		private readonly IPersonRepository _personRepository;

		/// <summary>
		/// A container for genre information.
		/// </summary>
		private readonly IGenreRepository _genreRepository;

		/// <summary>
		/// The API key used to access the information stored at themoviedb.org.
		/// </summary>
		private readonly string _apiKey;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="configuration">The configuration for the application.</param>
		/// <param name="personRepository">A container for person information.</param>
		/// <param name="genreRepository">A container for genre information.</param>
		public RemoteMovieRepository(IConfigurationProvider configuration, IPersonRepository personRepository, IGenreRepository genreRepository)
		{
			if (configuration == null)
				throw new ArgumentNullException("configuration");
			if (personRepository == null)
				throw new ArgumentNullException("personRepository");
			if (genreRepository == null)
				throw new ArgumentNullException("genreRepository");

			_apiKey = configuration.MovieDBAPIKey;
			_personRepository = personRepository;
			_genreRepository = genreRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Performs a search for movie information.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <returns>The resulting movie information.</returns>
		public IEnumerable<RemoteMovie> Search(string query)
		{
			if (string.IsNullOrEmpty(query))
				throw new ArgumentNullException("query");

			var url = "http://api.themoviedb.org/2.1/Movie.search/en/json/" + _apiKey + "/" + query.Replace(" ", "+");

			var request = WebRequest.Create(url);
			var response = request.GetResponse();
			if (response == null)
				throw new InvalidOperationException("The response for request \"" + url + "\" is null.");

			var stream = response.GetResponseStream();
			if (stream == null)
				throw new InvalidOperationException("The stream for the response for the request at \"" + url + "\" is null.");

			using (var reader = new StreamReader(stream))
			{
				var responseText = reader.ReadToEnd();

				var deserializer = new JavaScriptSerializer();
				return AdaptSearchResults(deserializer.Deserialize<dynamic>(responseText));
			}
		}

		/// <summary>
		/// Retrieves details for a movie.
		/// </summary>
		/// <param name="id">The movie ID.</param>
		/// <returns>The retrieved movie details.</returns>
		public Movie GetDetails(int id)
		{
			try
			{
				if (id < 1)
					throw new ArgumentOutOfRangeException("id");

				var url = "http://api.themoviedb.org/2.1/Movie.getInfo/en/json/" + _apiKey + "/" + id;

				var request = WebRequest.Create(url);
				var response = request.GetResponse();

				var stream = response.GetResponseStream();
				if (stream == null)
					throw new InvalidOperationException("The stream for the response for the request at \"" + url + "\" is null.");

				using (var reader = new StreamReader(stream))
				{
					var responseText = reader.ReadToEnd();

					var deserializer = new JavaScriptSerializer();
					return AdaptDetails(deserializer.Deserialize<dynamic>(responseText));
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error during RemoteMovieRepository.GetDetails.", ex);
				return null;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Adapts a response to a useable collection of remote movie objects.
		/// </summary>
		/// <param name="response">The repsonse from the remote server.</param>
		/// <returns>The adapted information.</returns>
		private static IEnumerable<RemoteMovie> AdaptSearchResults(dynamic response)
		{
			var movies = new List<RemoteMovie>();

			if (response[0] is string)
				return movies;

			foreach (var movie in response)
			{
				movies.Add(new RemoteMovie
				{
					ID = Convert.ToInt32(movie["id"]),
					Title = movie["name"],
					PosterLocation = DerivePoster(movie)
				});
			}
			return movies;
		}

		/// <summary>
		/// Derives the location of a poster from the response.
		/// </summary>
		/// <param name="movie">The search request's response.</param>
		/// <returns>The location of a poster.</returns>
		private static string DerivePoster(dynamic movie)
		{
			dynamic poster = null;
			foreach (var image in movie["posters"])
			{
				var width = Convert.ToInt32(image["image"]["width"]);
				var height = Convert.ToInt32(image["image"]["height"]);
				if (poster == null || width * height > poster.width * poster.height)
					poster = new { width, height, url = image["image"]["url"] };
			}

			return poster == null ? null : poster.url;
		}

		/// <summary>
		/// Adapts retrieved detailed movie information into a useable movie object.
		/// </summary>
		/// <param name="info">The retrieved information.</param>
		/// <returns>The adapted information.</returns>
		private Movie AdaptDetails(dynamic info)
		{
			var data = info[0];
			var year = new DateTime(Convert.ToInt32(data["released"].Substring(0, 4)), 1, 1);
			var movie = new Movie
			{
				DateAdded = DateTime.Now,
				Description = data["overview"],
				Name = data["name"],
				PosterLocation = DerivePoster(data),
				Genres = DeriveGenres(data),
				Year = year
			};

			var cast = data["cast"];
			var actors = new List<Person>();
			var producers = new List<Person>();
			foreach (IDictionary<string, object> member in cast)
			{
				var person = _personRepository.GetByName(member["name"] as string);
				if (person == null)
				{
					person = new Person {FirstName = ((string) member["name"]).Split(' ')[0], LastName = ((string) member["name"]).Split(' ')[1]};
					_personRepository.SaveOrUpdate(person);
				}

				switch ((string)member["job"])
				{
					case "Director":
						movie.Director = person;
						break;
					case "Producer":
						producers.Add(person);
						break;
					case "Actor":
						actors.Add(person);
						break;
				}
			}

			movie.Producers = producers;
			movie.Actors = actors;

			return movie;
		}

		/// <summary>
		/// Derives a collection of genres.
		/// </summary>
		/// <param name="data">The incoming data.</param>
		/// <returns>The derived genres.</returns>
		private IEnumerable<Genre> DeriveGenres(dynamic data)
		{
			var retrieved = _genreRepository.GetAll().ToDictionary(x => x.Name);
			var genres = new List<Genre>();
			foreach (var raw in data["genres"])
			{
				var name = raw["name"];
				genres.Add(retrieved.ContainsKey(name) ? retrieved[name] : new Genre {Name = name});
			}
			return genres;
		}
		#endregion
	}
}