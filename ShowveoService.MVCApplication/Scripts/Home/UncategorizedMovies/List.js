if (!Showveo.Home)
	Showveo.Home = {};
if (!Showveo.Home.UncategorizedMovies)
	Showveo.Home.UncategorizedMovies = { };

Showveo.Home.UncategorizedMovies.List = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The callback method to fire after the user has selected a movie to categorize.
	var onMovieSelected;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* onMovieSelected: The callback method to fire after the user has selected a movie to categorize.
	*/
	this.initialize = function (parameters) {
		_onMovieSelected = parameters.onMovieSelected;

		loadComponents(parameters.panel);
		loadUncategorizedMovies(populateUncategorizedMovies);
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	//-------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel: The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;
	};

	/*
	* Retrieves the list of uncategorized movies from the server.
	* callback: The callback function to execute with the list of retrieved movies.
	*/
	var loadUncategorizedMovies = function (callback) {
		$.ajax({
			type: "GET",
			url: "uncategorized/all",
			success: callback,
			error: Showveo.Controls.Feedback.error
		});
	};

	/*
	* Takes a collection of uncategorized movies and loads the list with panels representative of those movies.
	* movies: The uncategorized movie list.
	*/
	var populateUncategorizedMovies = function (movies) {
		_components.panel.find("div.um").remove();
		$(movies).each(function (i, movie) {
			var panel = $("<div></div>").addClass("um").text(movie.OriginalFile).click(function () {
				_onMovieSelected(movie);
			});
			panel.insertBefore(_components.panel.find("div.clear"));
		});
	};

	this.initialize(parameters);
};