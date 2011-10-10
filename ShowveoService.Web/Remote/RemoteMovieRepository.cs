using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using ShowveoService.Entities;
using ShowveoService.Service.Configuration;

namespace ShowveoService.Web.Remote
{
	/// <summary>
	/// A container for remote movie information.
	/// </summary>
	public class RemoteMovieRepository : IRemoteMovieRepository
	{
		#region Data Members
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
		public RemoteMovieRepository(IConfigurationProvider configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException("configuration");

			_apiKey = configuration.MovieDBAPIKey;
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
			return Adapt(new JavaScriptSerializer().Deserialize<dynamic>("[{\"score\":null,\"popularity\":3,\"translated\":true,\"adult\":false,\"language\":\"en\",\"original_name\":\"Moneyball\",\"name\":\"Moneyball\",\"alternative_name\":null,\"movie_type\":\"movie\",\"id\":60308,\"imdb_id\":\"tt1210166\",\"url\":\"http://www.themoviedb.org/movie/60308\",\"votes\":1,\"rating\":8.5,\"certification\":\"PG-13\",\"overview\":\"The story of Oakland A's general manager Billy Beane's successful attempt to put together a baseball club on a budget by employing computer-generated analysis to draft his players.\",\"released\":\"2011-09-23\",\"posters\":[{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1500,\"width\":1012,\"url\":\"http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg\",\"id\":\"4e4306015e73d6408900017e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":741,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-mid.jpg\",\"id\":\"4e4306015e73d6408900017e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":507,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-w342.jpg\",\"id\":\"4e4306015e73d6408900017e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":274,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-cover.jpg\",\"id\":\"4e4306015e73d6408900017e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":228,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-w154.jpg\",\"id\":\"4e4306015e73d6408900017e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":136,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-thumb.jpg\",\"id\":\"4e4306015e73d6408900017e\"}}],\"backdrops\":[{\"image\":{\"type\":\"backdrop\",\"size\":\"original\",\"height\":1080,\"width\":1920,\"url\":\"http://cf1.imgobject.com/backdrops/365/4e3023c07b9aa12a37002365/moneyball-original.jpg\",\"id\":\"4e3023c07b9aa12a37002365\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"w1280\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/365/4e3023c07b9aa12a37002365/moneyball-w1280.jpg\",\"id\":\"4e3023c07b9aa12a37002365\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"poster\",\"height\":439,\"width\":780,\"url\":\"http://cf1.imgobject.com/backdrops/365/4e3023c07b9aa12a37002365/moneyball-poster.jpg\",\"id\":\"4e3023c07b9aa12a37002365\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"thumb\",\"height\":169,\"width\":300,\"url\":\"http://cf1.imgobject.com/backdrops/365/4e3023c07b9aa12a37002365/moneyball-thumb.jpg\",\"id\":\"4e3023c07b9aa12a37002365\"}}],\"version\":192,\"last_modified_at\":\"2011-10-08 13:27:53\"}]"));

			//if (string.IsNullOrEmpty(query))
			//    throw new ArgumentNullException("query");

			//var url = "http://api.themoviedb.org/2.1/Movie.search/en/json/" + _apiKey + "/" + query.Replace(" ", "+");

			//var request = WebRequest.Create(url);
			//var response = request.GetResponse();
			//if (response == null)
			//    throw new InvalidOperationException("The response for request \"" + url + "\" is null.");

			//var stream = response.GetResponseStream();
			//if (stream == null)
			//    throw new InvalidOperationException("The stream for the response for the request at \"" + url + "\" is null.");

			//using (var reader = new StreamReader(stream))
			//{
			//    var responseText = reader.ReadToEnd();

			//    var deserializer = new JavaScriptSerializer();
			//    return Adapt(deserializer.Deserialize<dynamic>(responseText));
			//}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Adapts a response to a useable collection of remote movie objects.
		/// </summary>
		/// <param name="response">The repsonse from the remote server.</param>
		/// <returns>The adapted information.</returns>
		private IEnumerable<RemoteMovie> Adapt(dynamic response)
		{
			var movies = new List<RemoteMovie>();
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
		private string DerivePoster(dynamic movie)
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
		#endregion
	}
}