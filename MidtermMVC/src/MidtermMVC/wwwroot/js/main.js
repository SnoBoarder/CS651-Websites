var gridData;

var onGridUpdate = function (args) {
	//alert(args.grid.data[0].InputLetter + "|" + args.grid.data[0].SubstituteLetter);
	gridData = args.grid.data;
	console.log("updating grid");
};

function generateGrid(strategy) {
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
}

generateGrid();

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
			var input = $("#input").val();
			var output = data.replace(/"/g, "");

			var outputArray = output.split("");

			for (var i = 0; i < input.length; ++i) {
				if (input.charAt(i) == outputArray[i] && isLetterOrNumber(input.charAt(i))) {
					var letter = outputArray[i];

					outputArray[i] = "<span style=\"color: red\">" + letter + "</span>";
				}
			}

			var display = "<p>" + outputArray.join("") + "</p>";

			$("#output").html(display);
		},
		error: function (msg) {
			alert("Error: " + msg.responseText);
		}
	});
});

function isLetterOrNumber(char) {
	return char.match(/[a-z0-9]/i);
}

$("#letter").click(function () {
	var inputString = $("#input").val();

	// TODO: Store this on server side
	// organize first attempt of cipher based on letter frequency from the following URL:
	// https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language
	var frequency = ["e", "t", "a", "o", "i", "n", "s", "h", "r", "d", "l", "c", "u", "m", "w", "f", "g", "y", "p", "b", "v", "k", "j", "x", "q", "z"];

	// get number count for every letter
	var dictionary = {};
	for (var i = 0; i < inputString.length; ++i) {
		var char = inputString.charAt(i);

		// only handle letters
		if (!isLetterOrNumber(char)) {
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
	var strategy = [];
	for (var i = 0; i < items.length; ++i) {
		strategy.push({ OriginalCharacter: items[i][0], ReplacementCharacter: frequency[i] });
	}

	generateGrid(strategy);
});

$("#bigram").click(function () {
	var inputString = $("#input").val();

	// TODO: Store this on server side
	// organize first attempt of cipher based on bigram frequency from the following URL:
	// https://en.wikipedia.org/wiki/Bigram
	var frequency = ["th", "he", "in", "er", "an", "re", "nd", "at", "on", "nt", "ha", "es", "st", "en", "ed", "to", "it", "ou", "ea", "hi", "is", "or", "ti", "as", "te", "et", "ng", "of", "al", "de", "se", "le", "sa", "si", "ar", "ve", "ra", "ld", "ur"];

	var dictionary = makeNGrams(inputString, 2);

	// sort dictionary by highest value
	var items = Object.keys(dictionary).map(function (key) {
		return [key, dictionary[key]];
	});

	// Sort the array based on the second element
	items.sort(function (first, second) {
		return second[1] - first[1];
	});

	var strategy = generateStrategy(items, frequency);

	generateGrid(strategy);
});

$("#trigram").click(function () {
	var inputString = $("#input").val();

	// TODO: Store this on server side
	// organize first attempt of cipher based on bigram frequency from the following URL:
	// https://en.wikipedia.org/wiki/Trigram
	var frequency = ["the", "and", "tha", "ent", "ing", "ion", "tio", "for", "nde", "has", "nce", "edt", "tis", "oft", "sth", "men"];

	var dictionary = makeNGrams(inputString, 3);

	// sort dictionary by highest value
	var items = Object.keys(dictionary).map(function (key) {
		return [key, dictionary[key]];
	});

	// Sort the array based on the second element
	items.sort(function (first, second) {
		return second[1] - first[1];
	});

	var strategy = generateStrategy(items, frequency);

	generateGrid(strategy);
});

function makeNGrams(text, size) {
	var dictionary = {};

	var word = "";

	// add the first character if it's valid
	// this allows for bypassing for loop check for i==0 for before and after values
	// NOTE: This only handles letters!
	for (var i = 0; i < text.length; ++i) {
		if (isLetterOrNumber(text.charAt(i))) {
			// it's a letter! handle it
			word += text.charAt(i);

			if (word.length == size) {
				// we've reached the max size of the gram (bigram size = 2, trigram size = 3, etc.)
				// add to dictionary
				if (!(word in dictionary)) {
					dictionary[word] = 0;
				}

				dictionary[word]++;

				// remove the first letter in preparation for the next letter
				word = word.substring(1, word.length);
			}
		} else {
			// it's not a letter, clear it out
			word = "";
		}
	}

	return dictionary;
}

// populate strategy based on item list and the frequency stat
function generateStrategy(items, frequency) {
	var strategy = [];

	var len = Math.min(items.length, frequency.length);
	var usedOriginalLetters = [];
	var usedReplacementLetters = [];
	for (var i = 0; i < len; ++i) {
		var originalLetters = items[i][0];

		for (var j = 0; j < originalLetters.length; ++j) {

			var letter = originalLetters.charAt(j);
			var frequencyLetter = frequency[i].charAt(j);

			if (usedOriginalLetters.indexOf(letter) == -1 &&
				usedReplacementLetters.indexOf(frequencyLetter) == -1) {

				// first use of original letter AND replacement letter. add it
				strategy.push({ OriginalCharacter: letter, ReplacementCharacter: frequencyLetter });

				usedOriginalLetters.push(letter);
				usedReplacementLetters.push(frequencyLetter);
			}
		}
	}

	return strategy;
}

