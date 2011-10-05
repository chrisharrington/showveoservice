Showveo.Home.MovieCreator = new function () {

	//--------------------------------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Creates a movie panel derived from a movie.
	* movie: The movie used to create the movie panel.
	* width: The width of the movie panel.
	* Returns: The created movie panel.
	*/
	this.create = function (movie, width) {
		var panel = $("<div></div>").width(width).click(onMovieClicked).data("movie", movie);
		panel.append(createPoster(movie, width));
		return panel;
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after the user clicks on a movie. Shows the movie information panel.
	*/
	var onMovieClicked = function () {
		var movie = $(this).data("movie");
		inspect(movie);
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Creates a poster used to select a movie.
	* movie: The movie to be represented by the poster.
	* width: The width of the poster.
	* Returns: The created poster control.
	*/
	var createPoster = function (movie, width) {
		return $("<img />").attr("alt", "").attr("src", movie.PosterLocation).width(width).height(width * 3 / 2);
	};
};