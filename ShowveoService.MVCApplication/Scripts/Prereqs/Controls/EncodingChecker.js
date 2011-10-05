if (!Showveo.Controls)
	Showveo.Controls = { };

/*
* A class used to periodically check to see if any encoding tasks are in progress and, if so, displays them
* to the user.
*/
Showveo.Controls.EncodingChecker = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	*/
	this.initialize = function (parameters) {
		loadComponents(parameters.panel);

		checkContinuous();
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
		_components.panelProgress = panel.find(">div>div");
		_components.labelFile = panel.find(">span>b");
	};

	/*
	* Continuously checks for new encoding tasks. If tasks exist, displays them to the user.
	*/
	var checkContinuous = function () {
		$.ajax({
			type: "GET",
			url: "movies/encoding",
			success: function (tasks) {
				populate(tasks);
				setTimeout(function() {
					checkContinuous();
				}, 5000);
			}
		});
	};

	/*
	* Populates the encoding task area with the given task information.
	* tasks: The current encoding tasks.
	*/
	var populate = function (tasks) {
		if (tasks.length == 0) {
			_components.panel.fadeOut(200);
			return;
		}

		var task = tasks[0];
		_components.labelFile.text(task.File + " (" + task.PercentComplete + "%)");
		_components.panelProgress.css("width", task.PercentComplete + "%");
		_components.panel.fadeIn(200);
	};

	this.initialize(parameters);
};