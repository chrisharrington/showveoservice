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

	// The width of the movies displayed in the search results panel.
	var _movieWidth;

	// The movie that the user wishes to categorize.
	var _movie;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* movieWidth: The width of the movies displayed in the search results panel.
	*/
	this.initialize = function (parameters) {
		_movieWidth = parameters.movieWidth;

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
	var onMovieSelected = function (movie) {
		_movie = movie;
		_components.list.hide(_components.search.show);
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
			onMovieSelected: onMovieSelected
		});

		_components.search = new Showveo.Home.UncategorizedMovies.Search({
			panel: panel.find(">div.s"),
			movieWidth: _movieWidth
		});
	};

	this.initialize(parameters);
};