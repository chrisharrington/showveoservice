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

	/*
	* Subtracts one from the uncategorized count.
	*/
	this.decrementUncategorizedCount = function () {
		var string = _components.menu.text().replace("Uncategorized", "").replace("(", "").replace(")", "").replace(" ", "");
		var count = parseInt(string, 10);
		if (isNaN(count))
			return;

		_components.menu.text("Uncategorized" + (count == 1 ? "" : " (" + (count - 1) + ")"));
	};

	/*
	* Adds one to the uncategorized count.
	*/
	this.incrementUncategorizedCount = function () {
		var string = _components.menu.text().replace("Uncategorized", "").replace("(", "").replace(")", "").replace(" ", "");
		var count = parseInt(string, 10);
		if (isNaN(count))
			return;

		_components.menu.text("Uncategorized  (" + (count + 1) + ")");
	};

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
			url: "uncategorized/count",
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