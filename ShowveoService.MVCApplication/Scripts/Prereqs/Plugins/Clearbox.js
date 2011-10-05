/*
* A plugin showing greyed out text in a textbox which is cleared on focus.
*/
(function ($) {
	$.fn.extend({
		clearbox: function (value) {
			return this.each(function () {
				var input = $(this);
				if (!value) {
					input.val(input.data("cbvalue")).css("color", "#888");
					return;
				}

				input.val(value);
				input.css("color", "#888").data("cbvalue", value).focus(function () {
					if (input.attr("readonly"))
						return;
					if (input.val() == value)
						input.val("").css("color", "Black");
				}).blur(function () {
					if (input.attr("readonly"))
						return;
					if (input.val() == "")
						input.val(value).css("color", "#888");
				});
			});
		}
	});
})(jQuery);