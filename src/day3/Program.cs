var input = await File.ReadAllLinesAsync("input.csv");
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


var oxygenGeneratorRateInput = input.ToList();
var co2ScrubberRatingInput = input.ToList();

// Oxygen Generator Rate
while (oxygenGeneratorRateInput.Count() > 1)
{
    for (int i = 0; i < input.First().Length; i++)
    {
        var mostCommon = oxygenGeneratorRateInput
            .Select(i2 => i2[i])
            .GroupBy(i2 => i2)
            .Select(i2 => new { i2.Key, Count = i2.Count() })
            .OrderByDescending(i2 => i2.Key) // 1 in case of same count
            .MaxBy(i2 => i2.Count)?.Key;

        oxygenGeneratorRateInput.RemoveAll(r => r[i] != mostCommon);
    }
}

var oxygenGeneratorRate = oxygenGeneratorRateInput.First();

// Oxygen Generator Rate
while (co2ScrubberRatingInput.Count() > 1)
{
    for (int i = 0; i < input.First().Length; i++)
    {
        var mostCommon = co2ScrubberRatingInput
            .Select(i2 => i2[i])
            .GroupBy(i2 => i2)
            .Select(i2 => new { i2.Key, Count = i2.Count() })
            .OrderBy(i2 => i2.Key) // 0 in case of same count
            .MinBy(i2 => i2.Count)?.Key;

        co2ScrubberRatingInput.RemoveAll(r => r[i] != mostCommon);
    }
}

var co2ScrubberRating = co2ScrubberRatingInput.First();

Console.WriteLine($"Oxygen Rate: {oxygenGeneratorRate} ({Convert.ToInt32(oxygenGeneratorRate, 2)} decimal)");
Console.WriteLine($"CO2 Scrubber Rate: {co2ScrubberRating} ({Convert.ToInt32(co2ScrubberRating, 2)} decimal)");
Console.WriteLine($"[Challenge part two] Result {(Convert.ToInt32(oxygenGeneratorRate, 2) * Convert.ToInt32(co2ScrubberRating, 2))}");
