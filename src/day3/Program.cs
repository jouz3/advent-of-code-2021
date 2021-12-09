var input = await File.ReadAllLinesAsync("input_sample.csv");
var gammaRateBinary = string.Empty;
var epsilonRateBinary = string.Empty;

for (int i = 0; i < input.First().Length; i++)
{
    var groupedInput = input
        .Select(i2 => i2[i])
        .GroupBy(i2 => i2)
        .Select(i2 => new { i2.Key, Count = i2.Count() });

    gammaRateBinary += groupedInput.MaxBy(i2 => i2.Count)?.Key;
    epsilonRateBinary += groupedInput.MinBy(i2 => i2.Count)?.Key;
}

Console.WriteLine($"Gamma Rate: {gammaRateBinary} ({Convert.ToInt32(gammaRateBinary, 2)} decimal)");
Console.WriteLine($"Epsilon Rate: {epsilonRateBinary} ({Convert.ToInt32(epsilonRateBinary, 2)} decimal)");
Console.WriteLine($"[Challenge part one] Result {(Convert.ToInt32(gammaRateBinary, 2) * Convert.ToInt32(epsilonRateBinary, 2))}");
