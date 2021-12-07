var increases = 0;
int? previousRecord = default;
var input = await File.ReadAllLinesAsync("input.csv");

foreach (var record in input)
{
    if (int.TryParse(record, out int currentRecord))
    {
        if (currentRecord > (previousRecord ?? currentRecord))
            increases++;

        previousRecord = currentRecord;
    }
}

Console.WriteLine($"Number of increases: {increases}");