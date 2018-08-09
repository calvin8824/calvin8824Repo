function countingCharacters(stringToCount)
{
	console.log(stringToCount + " has " + 
	stringToCount.length + " characters.");
}

function countingCharacters2(stringToCount, characterToFind)
{
	var characterCount = 0;
	for (var characterPosition = 0; 
	characterPosition < stringToCount.length;
	characterPosition++)
	{
		if(stringToCount[characterPosition] == characterToFind)
		{
			characterCount++;
		}
	}
	console.log("String to search in: " + stringToCount);
	console.log("Character to find: " + characterToFind);
	console.log("Number of times the character appears: " + characterCount);
}

function countingCharacters3(stringToCount, stringToSearch)
{
	var count = 0; 
	var numChars = stringToSearch.length;
	
	var lastIndex = stringToCount.length - numChars;
	
	for(var index = 0; index <= lastIndex; index++)
	{
		var current = stringToCount.substring(index, index + numChars);
		
		if(current == stringToSearch)
		{
			count++;
		}
	}
	
	console.log("String to search in: " + stringToCount);
    console.log("Character to find: " + stringToSearch);
    console.log("Number of times the character appears: " + count);

	return count;
}

function rollDice(maxRange, minRange)
{
	return Math.ceil(Math.random() * (1 + maxRange - minRange));
}
for(var i = 0; i < 100; i++)
{
	console.log(rollDice());
}

function addTwoNumbers(firstNumber, secondNumber){ 
    return (firstNumber + secondNumber);
}

var testArray = [0,1,1,"1",3,"311"];
for(var arrayPosition = 0; 
arrayPosition < testArray.length - 1;
arrayPosition++)
{
	var currentElement = testArray[arrayPosition];
	var nextElement = testArray[arrayPosition + 1];
	
	console.log("Testing " + currentElement + " and " + 
	nextElement + "(greater than): " + 
	(currentElement > nextElement));
	
	console.log("Testing " + currentElement + 
	" and " + nextElement + "(less than or equal to): " + 
	(currentElement <= nextElement));
	
	if(currentElement == nextElement)
	{
		console.log("Testing " + currentElement +
		" and " + nextElement + "(strictly equal to): " +
		(currentElement === nextElement));
	
		if(currentElement !== nextElement)
		{
			console.log(currentElement + " is " +
			typeof(currentElement));
			console.log(nextElement + " is " +
			typeof(nextElement));
		}
	}
	else
	{
		console.log("Testing " + currentElement +
		" and " + nextElement + "(equal to): false");
	}
}


var testArray = [17, 42, 311, 5, 9, 10, 28, 7, 6];
var sum = 0;
for (var arrayPosition = 0; arrayPosition < testArray.length; arrayPosition++) {
    sum += testArray[arrayPosition];
}
console.log("The sum of " + testArray + " is: " + sum);

function findMax(checkArray)
{
	var maxValue;
	for(var i = 0; i < checkArray.length; i++)
	{
		if(maxValue < checkArray[i])
		{
			maxValue = checkArray[i];
		}
		else if(maxValue === undefined)
		{
			maxValue = checkArray[i];
		}
	}
	console.log("The max value is: " + maxValue);
}
