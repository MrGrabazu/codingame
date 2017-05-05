/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

var n = parseInt(readline());
var inputs = readline().split(' ');
var values = [];
for (var i = 0; i < n; i++) {
    values.push(parseInt(inputs[i]));
}

var global = 0;
var stream = 0;
var higherValue = values[0];
var lastValue = values[0];
var length = values.length;
for (var i = 1; i < length; i++) {
    var value = values[i];
    if (value > higherValue) {
        higherValue = value;
        stream = 0;
    }
    else {
        stream += value - lastValue;
        global = global > stream ? stream : global;
    }
    lastValue = value;
}
print(global);