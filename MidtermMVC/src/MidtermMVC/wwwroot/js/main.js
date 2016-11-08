var strategy = [];

var gridData;

var grid = {
	width: "100%",
	height: "400px",

	inserting: true,
	editing: true,
	sorting: true,
	paging: true,

	data: strategy,

	onItemInserted: onGridUpdate,
	onItemDeleted: onGridUpdate,
	onItemUpdated: onGridUpdate,

	fields: [{
		name: "OriginalCharacter",
		type: "text",
		rangeLength: 1,
		validate: [
			"required",
			{
				validator: "rangeLength",
				message: function (value, item) {
					return "Must have only 1 character.";
				},
				param: [1, 1]
			}
		]
	},
	{
		name: "ReplacementCharacter",
		type: "text",
		validate: [
			"required",
			{
				validator: "rangeLength",
				message: function (value, item) {
					return "Must have only 1 character.";
				},
				param: [1, 1]
			}
		]
	},
	{
		type: "control"
	}]
};

var onGridUpdate = function (args) {
	//alert(args.grid.data[0].InputLetter + "|" + args.grid.data[0].SubstituteLetter);
	gridData = args.grid.data;
};

$("#jsGrid").jsGrid(grid);

$("#submit").click(function () {
	// TODO: setup the ajax call to decipher based on the strategy
	var data = {
		input: $("#input").val(),
		config: gridData
	};

	var dataString = JSON.stringify(data);

	$.ajax({
		type: "POST",
		url: '/Home/Decipher',
		//contentType: "application/json; charset=utf-8",
		dataType: "text",
		data: data,
		success: function (data) {
			//var str = JSON.parse(data).d.replace(/"/g, "");
			$("#output").text(data);
		},
		error: function (msg) {
			alert("Error: " + msg.responseText);
		}
	});
});

$("#letter").click(function () {
	var inputString = $("#input").val();

	// organize first attempt of cipher based on letter frequency from the following URL:
	// https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language
	var frequency = ["e", "t", "a", "o", "i", "n", "s", "h", "r", "d", "l", "c", "u", "m", "w", "f", "g", "y", "p", "b", "v", "k", "j", "x", "q", "z"];

	// get number count for every letter
	var dictionary = {};
	for (var i = 0; i < inputString.length; ++i) {
		var char = inputString.charAt(i);

		// only handle letters
		if (!char.match(/[a-z]/i)) {
			continue;
		}

		if (!(char in dictionary)) {
			dictionary[char] = 0;
		}

		dictionary[char]++;
	}

	// sort dictionary by highest value
	var items = Object.keys(dictionary).map(function (key) {
		return [key, dictionary[key]];
	});

	// Sort the array based on the second element
	items.sort(function (first, second) {
		return second[1] - first[1];
	});

	// populate strategy based on item list and the frequency stat
	strategy = [];
	for (var i = 0; i < items.length; ++i) {
		strategy.push({ OriginalCharacter: items[i][0], ReplacementCharacter: frequency[i] });
	}

	//console.log(JSON.stringify(strategy));

	// TODO: Figure out how to keep the same jsGrid and just repopulate the data
	$("#jsGrid").jsGrid({
		width: "100%",
		height: "400px",

		inserting: true,
		editing: true,
		sorting: true,
		paging: true,

		data: strategy,

		onInit: onGridUpdate,
		onItemInserted: onGridUpdate,
		onItemDeleted: onGridUpdate,
		onItemUpdated: onGridUpdate,

		fields: [{
			name: "OriginalCharacter",
			type: "text",
			rangeLength: 1,
			validate: [
				"required",
				{
					validator: "rangeLength",
					message: function (value, item) {
						return "Must have only 1 character.";
					},
					param: [1, 1]
				}
			]
		},
		{
			name: "ReplacementCharacter",
			type: "text",
			validate: [
				"required",
				{
					validator: "rangeLength",
					message: function (value, item) {
						return "Must have only 1 character.";
					},
					param: [1, 1]
				}
			]
		},
		{
			type: "control"
		}]
	});
});

$("#bigram").click(function () {

});

$("#trigram").click(function () {

});

