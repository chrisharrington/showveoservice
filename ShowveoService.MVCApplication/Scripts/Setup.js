Showveo = {};
Showveo.Home = {};
Showveo.Controls = { };

$(document).ready(function () {
	Showveo.Controls.Feedback.initialize($("body>div.f"));
	Showveo.Home.Home.initialize();
});

var inspect = function (value) {
	var string = "";
	for (var name in value)
		string += name + ": " + value[name] + "\n";
	alert(string);
};