if (!Showveo.Home)
	Showveo.Home = {};

/*
* A control used to display a collection of movies in a grid.
*/
Showveo.Home.MovieGrid = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The width of each movie poster.
	var _movieWidth;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* movieWidth: The width of each movie poster.
	*/
	this.initialize = function (parameters) {
		_movieWidth = parameters.movieWidth;

		loadComponents(parameters.panel);
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Loads the movie grid with a collection of movies.
	* movies: The movies to load into the grid.
	*/
	this.load = function (movies) {
		populateMovies(movies);
	};

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
	* Populates a movie panel with a given list of movies.
	* movies: The list of movies used to populate the movies panel.
	*/
	var populateMovies = function (movies) {
		_components.panel.empty();
		var columns = calculateColumns(_movieWidth);
		var count = 0;
		while (count < movies.length) {
			var row = $("<div></div>");
			var min = Math.min(movies.length - count, columns);
			for (var i = 0; i < min; i++) {
				row.append(Showveo.Home.MovieCreator.create(movies[count], _movieWidth));
				count++;
			}
			_components.panel.append(row);
		}

		fadeInMovies();
	};

	/*
	* Calculates the number of columns used to display movies by taking the screen width and
	* dividing it by the individual movie width, and adding padding.
	* width: The width of a movie panel.
	* Returns: The calculated number of columns.
	*/
	var calculateColumns = function (width) {
		return Math.floor($(window).width() / (width + 10));
	};

	/*
	* Iterates through all of the added movies and fades in the posters in a random fashion.
	*/
	var fadeInMovies = function () {
		_components.panel.find("img").load(function () {
			var poster = $(this);
			setTimeout(function () {
				poster.fadeIn(250);
			}, Math.random() * 1000);
		});
	};

	this.initialize(parameters);
};