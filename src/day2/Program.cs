var horizontalPosition = 0;
var depth = 0;

var input = await File.ReadAllLinesAsync("input.csv");

var inputParsed = input
    .Select(i => new 
    { 
        Command = i?.Split(' ').First(), 
        Value = i?.Split(' ').Last() 
    });

foreach (var record in inputParsed)
{
    if (int.TryParse(record.Value, out int valueInt))
    {
        switch (record.Command)
        {
            case "forward":
                horizontalPosition += valueInt;
                break;
            case "down":
                depth += valueInt;
                break;
            case "up":
                depth -= valueInt;
                break;
            default:
                break;
        }
    }
}

Console.WriteLine($"Horizontal Position: {horizontalPosition} Depth: {depth}");
Console.WriteLine($"[Challenge part 1] Final Position: {horizontalPosition * depth}");

// Reset values
horizontalPosition = 0;
depth = 0;

var aim = 0;

foreach (var record in inputParsed)
{
    if (int.TryParse(record.Value, out int valueInt))
    {
        switch (record.Command)
        {
            case "forward":
                horizontalPosition += valueInt;
                depth += aim * valueInt;
                break;
            case "down":
                aim += valueInt;
                break;
            case "up":
                aim -= valueInt;
                break;
            default:
                break;
        }
    }
}

Console.WriteLine($"Horizontal Position: {horizontalPosition} Depth: {depth}");
Console.WriteLine($"[Challenge part 2] Final Position: {horizontalPosition * depth}");