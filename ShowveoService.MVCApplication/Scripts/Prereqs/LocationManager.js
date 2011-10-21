/*
* A class used to manage the current location of the application.
*/
Showveo.LocationManager = new function () {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The header control.
	var _header;

	// The movies control.
	var _movies;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* header: The header control.
	* movies: The movies control.
	*/
	this.initialize = function (parameters) {
		_header = parameters.header;
		_movies = parameters.movies;

		loadComponents();
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Changes the current location to the given location.
	* location: The new location.
	*/
	this.navigate = function (location) {
		_header.select(location);
		_movies.select(location);
	};

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	//-------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	*/
	var loadComponents = function () {
		_components = {};
	};

};