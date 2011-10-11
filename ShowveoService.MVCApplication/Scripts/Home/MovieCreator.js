Showveo.Home.MovieCreator = new function () {

	//--------------------------------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Creates a movie panel derived from a movie.
	* movie: The movie used to create the movie panel.
	* width: The width of the movie panel.
	* onClick: The function to call when the user clicks on the movie panel.
	* Returns: The created movie panel.
	*/
	this.create = function (movie, width, onClick) {
		var panel = $("<div></div>").width(width).height(width * 3 / 2).data("movie", movie).click(function () {
			onClick(movie);
		});
		panel.append(createPoster(movie, width));
		return panel;
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Event Handlers */

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