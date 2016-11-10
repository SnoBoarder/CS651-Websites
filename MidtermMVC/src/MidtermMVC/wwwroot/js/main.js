// member variable
var gridData;

// initialize
init();

// handle all functionality that needs to be initialized here
function init() {
	// generate the first instance of the grid
	generateGrid([]);
}

// constantly keep grid data updated whenever the strategy updates
var onGridUpdate = function (args) {
	gridData = args.grid.data;
};

// generates the grid with the provided strategy
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

$("#example").click(function () {
	$("#input").val("lbyeavb ha hpb lagyc at eacb sgbxzqwr. hpb zbm, xi mak vxm pxjb wahqebc, qi ha ihxgh lqhp hpb iqvdyb gkybi xsakh ybhhbg tgbfkbwem, xwc hgm wbl xddgaxepbi awym lpbw hpbib txqy. qw ahpbg lagci, mak dyxm hpb acci ha ihxgh, xwc hpbw xcukih makg ihgxhbrm xi wbbcbc.");
});

// handle clear click
$("#clear").click(function () {
	// clear the current strategy
	generateGrid([]);
});

// handle submit click
$("#submit").click(function () {
	// prepare data
	var data = {
		input: $("#input").val(),
		config: gridData
	};

	// JSONify it
	var dataString = JSON.stringify(data);

	$.ajax({
		type: "POST",
		url: '/Translator/Decipher',
		dataType: "text",
		data: data,
		success: function (data) {
			var input = $("#input").val();
			var output = data.replace(/"/g, "");

			var outputArray = output.split("");

			// highlight the letters that have not been translated yet!
			for (var i = 0; i < input.length; ++i) {
				if (input.charAt(i) == outputArray[i] && isLetterOrNumber(input.charAt(i))) {
					var letter = outputArray[i];

					outputArray[i] = "<span style=\"color: red\">" + letter + "</span>";
				}
			}
			
			var display;
			if (outputArray.length == 0) {
				display = "Please provide an input to see a translation.";
			} else {
				display = "<p>" + outputArray.join("") + "</p>";
			}

			$("#output").html(display);
		},
		error: function (msg) {
			alert("Error: " + msg.responseText);
		}
	});
});

// returns whether or not the passed in character is a letter or number
function isLetterOrNumber(char) {
	return char.match(/[a-z0-9]/i);
}

// click handler for handling letter frequency based on the input
$("#letter").click(function () {
	$.ajax({
		type: "GET",
		url: '/Frequency/Letter',
		dataType: "text",
		success: function (data) {
			handleLetterFrequency(JSON.parse(data));
		},
		error: function (msg) {
			alert("Error: " + msg.responseText);
		}
	});
});

// click handler for handling bigram frequency based on the input
$("#bigram").click(function () {
	$.ajax({
		type: "GET",
		url: '/Frequency/Bigram',
		dataType: "text",
		success: function (data) {
			handleBigramFrequency(JSON.parse(data));
		},
		error: function (msg) {
			alert("Error: " + msg.responseText);
		}
	});
});

// click handler for handling trigram frequency based on the input
$("#trigram").click(function () {
	$.ajax({
		type: "GET",
		url: '/Frequency/Trigram',
		dataType: "text",
		success: function (data) {
			handleBigramFrequency(JSON.parse(data));
		},
		error: function (msg) {
			alert("Error: " + msg.responseText);
		}
	});
});

// populate grid based on frequency
function handleLetterFrequency(frequency) {
	var inputString = $("#input").val();

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
}

// populate grid based on frequency
function handleBigramFrequency(frequency) {
	var inputString = $("#input").val();

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
}

// populate grid based on frequency
function handleTrigramFrequency(frequency) {
	var inputString = $("#input").val();

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
}

function makeNGrams(text, size) {
	var dictionary = {};

	var word = "";

	// traverse every character in the provided text
	for (var i = 0; i < text.length; ++i) {
		if (isLetterOrNumber(text.charAt(i))) {
			// it's a letter! add it to the word
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

