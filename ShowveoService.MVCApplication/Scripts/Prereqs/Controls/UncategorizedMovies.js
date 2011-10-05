if (!Showveo.Controls)
	Showveo.Controls = { };

Showveo.Controls.UncategorizedMovies = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* menu: The menu item used to show the number of uncategorized movies.
	*/
	this.initialize = function (parameters) {
		loadComponents(parameters.menu);
		loadUncategorizedMovieCount();
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	//-------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* menu: The menu item used to show the number of uncategorized movies.
	*/
	var loadComponents = function (menu) {
		_components = {};
		_components.menu = menu;
	};

	/*
	* Loads the number of uncategorized movies and assigns it to the menu link.
	*/
	var loadUncategorizedMovieCount = function () {
		$.ajax({
			type: "GET",
			url: "movies/uncategorizedcount",
			success: function (count) {
				if (count == 0)
					_components.menu.text("Uncategorized");
				else
					_components.menu.text("Uncategorized (" + count + ")");
			},
			error: Showveo.Controls.Feedback.error
		});
	};

	this.initialize(parameters);
};