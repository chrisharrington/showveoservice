Showveo.Controls.Feedback = new function () {

	//--------------------------------------------------------------------------------------------------------------
	/* Data Members */

	//	The common components for the control.
	var _components;

	//	The timeout key used to reference when the control will fade from view.
	var _timeout;

	//	A flag indicating whether or not the feedback control is visible.
	var _isVisible;

	//--------------------------------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Initializes the feedback control.
	* panel: The panel containing the control elements.
	*/
	this.initialize = function (panel) {
		_isVisible = false;

		loadComponents(panel);
	};

	/*
	* Indicates an error has occurred. Shows the message given as an error message.
	* message: The error message.
	*/
	this.error = function (message) {
		if (message.responseText)
			message = message.responseText;
		showMessage("error", message);
	};

	/*
	* Indicates a success condition. Shows the message given as a success message.
	* message: The success message.
	*/
	this.success = function (message) {
		showMessage("success", message);
		if (!_components)
			alert(message);
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel:				The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;
		_components.imageClose = panel.find("img").click(function () { _components.panel.fadeOut(200); _isVisible = false; });
		_components.labelFeedback = panel.find("span");
	};

	/*
	* Shows the message.
	* type:				The type of message to show: error or success.
	* message:			The message contents.
	*/
	var showMessage = function (type, message) {
		if (!_components) {
			alert(message);
			return;
		}

		if (_timeout)
			clearTimeout(_timeout);

		_components.labelFeedback.text(message);
		setPosition();
		_components.panel.removeClass("success error").addClass(type);

		if (!_isVisible) {
			_components.panel.fadeIn(200);
			_isVisible = true;
		}

		if (type != "error")
			_timeout = setTimeout(function () {
				_components.panel.fadeOut(2000);
				_isVisible = false;
			}, 10000);
	};

	/*
	* Sets the position of the feedback control.
	*/
	var setPosition = function () {
		var width = _components.panel.css({ left: "-1000px" }).show().outerWidth();
		if (!_isVisible)
			_components.panel.hide();

		_components.panel.css({ left: ($(window).width() / 2 - width / 2) + "px" });
		_components.imageClose.css({ top: "-8px", left: (width - 9) + "px" });
	};

};