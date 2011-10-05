Showveo.Controls.Header = function (parameters) {

	//--------------------------------------------------------------------------------------------------------------
	/* Data Members */

	//	The common components for the control.
	var _components;

	//	The callback function to execute after the user has selected a menu item.
	var _onMenuItemSelected;

	//--------------------------------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* onMenuItemSelected: The callback function to execute after the user has selected a menu item.
	*/
	this.initialize = function (parameters) {
		_onMenuItemSelected = parameters.onMenuItemSelected;

		loadComponents(parameters.panel);
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after the user has clicked a menu item. Shows the appropriate panel.
	*/
	var onMenuItemClicked = function () {
		var menu = $(this);
		if (menu.hasClass("s"))
			return;

		menu.parent().find(".s").removeClass("s");
		menu.addClass("s");

		if (_onMenuItemSelected)
			_onMenuItemSelected(menu.attr("id"));
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel: The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;
		_components.textSearch = panel.find("div.s>input").clearbox("Search");
		_components.labelMenuItems = panel.find("div.m>span").click(onMenuItemClicked);

		_components.uncategorized = new Showveo.Controls.UncategorizedMovies({
			menu: panel.find("#uncategorized")
		});

		_components.encodingchecker = new Showveo.Controls.EncodingChecker({
			panel: panel.find("div.m>div.e")
		});
	};

	this.initialize(parameters);
};