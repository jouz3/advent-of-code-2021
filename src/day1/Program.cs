var increases = 0;
var y = 0;
int? previousRecord = default;
var input = await File.ReadAllLinesAsync("input.csv");

// Parse input to integers collection
var inputParsed = input
    .Where(i => int.TryParse(i, out y))
    .Select(i => y);

foreach (var record in inputParsed)
{
    if (record > (previousRecord ?? record))
        increases++;

    previousRecord = record;
}

Console.WriteLine($"[Challenge part 1] Number of increases: {increases}");

// Reset increase number
increases = 0;

// Build three-measurement sliding window
var inputWindows = inputParsed
    .Zip(inputParsed.Skip(1), (current, next1) => new { current, next1})
    .Zip(inputParsed.Skip(2), (current, next2) => current.current + current.next1 + next2);

foreach (var record in inputWindows)
{
    if (record > (previousRecord ?? record))
        increases++;

    previousRecord = record;
}

Console.WriteLine($"[Challenge part 2] Number of increases: {increases}");