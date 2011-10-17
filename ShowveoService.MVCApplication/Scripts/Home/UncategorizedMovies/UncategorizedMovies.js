if (!Showveo.Home)
	Showveo.Home = {};
if (!Showveo.Home.UncategorizedMovies)
	Showveo.Home.UncategorizedMovies = { };
	
/*
* A class used to control the display and selection of uncategorized movies.
*/
Showveo.Home.UncategorizedMovies.UncategorizedMovies = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The number of movies to show on a row.
	var _columns;

	// The movie that the user wishes to categorize.
	var _movie;

	// The callback function to execute after the user has categorized a movie.
	var _onMovieCategorized;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* columns: The number of movies to show on a row.
	* onMovieCategorized: The callback function to execute after the user has categorized a movie.
	*/
	this.initialize = function (parameters) {
		_columns = parameters.columns;
		_onMovieCategorized = parameters.onMovieCategorized;

		loadComponents(parameters.panel);
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Shows this movie grid.
	* callback: The optional callback method fired after the grid has been shown.
	*/
	this.show = function (callback) {
		_components.panel.fadeIn(200, function () {
			if (callback)
				callback();
		});
	};

	/*
	* Hides this movie grid.
	* callback: The optional callback method fired after the grid has been hidden.
	*/
	this.hide = function (callback) {
		_components.panel.fadeOut(200, function () {
			if (callback)
				callback();
		});
	};

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after the user selected an uncategorized movie to categorize.
	* movie: The uncategorized movie to categorize.
	*/
	var onUncategorizedMovieSelected = function (movie) {
		_movie = movie;
		_components.list.hide(_components.search.show);
	};

	/*
	* Fired after the user has selected a movie that represents a search result selection.
	* movie: The selected movie.
	*/
	var onSearchMovieSelected = function (movie) {
		$.ajax({
			type: "POST",
			url: "/uncategorized/categorize/" + _movie.ID + "/" + movie.ID,
			success: _onMovieCategorized,
			error: Showveo.Controls.Feedback.error
		});
	};

	//-------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel: The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;
		_components.panelList = panel.find(">div.l");

		_components.list = new Showveo.Home.UncategorizedMovies.List({
			panel: panel.find(">div.l"),
			onMovieSelected: onUncategorizedMovieSelected
		});

		_components.search = new Showveo.Home.UncategorizedMovies.Search({
			panel: panel.find(">div.s"),
			columns: _columns,
			onMovieSelected: onSearchMovieSelected
		});
	};

	this.initialize(parameters);
};