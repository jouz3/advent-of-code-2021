var input = await File.ReadAllTextAsync("input.csv");
var inputRows = input.Split("\n\n").AsEnumerable();

var numbersDrawn = GetNumbersDrawn(inputRows);
var boards = GetBoards(inputRows);

int scoreFirstToWin = GetScoreFirstToWin(numbersDrawn);
Console.WriteLine($"ScoreFirstToWin: {scoreFirstToWin}");

int scoreLastToWin = GetScoreLastToWin(numbersDrawn);
Console.WriteLine($"ScorelastToWin: {scoreLastToWin}");

int GetScoreLastToWin(IEnumerable<int> numbersDrawn)
{
    var score = 0;

    foreach (var numberDrawn in numbersDrawn)
    {
        foreach (var board in boards)
        {
            var boardCompleteBefore = board.IsComplete();

            board.AddNumberDrawn(numberDrawn);

            if (board.IsComplete() && !boardCompleteBefore)
                score = board.Score() * numberDrawn;
        }
    }

    return score;
}

int GetScoreFirstToWin(IEnumerable<int> numbersDrawn)
{
    foreach (var numberDrawn in numbersDrawn)
    {
        foreach (var board in boards)
        {
            board.AddNumberDrawn(numberDrawn);

            if (board.IsComplete())
                return board.Score() * numberDrawn;
        }
    }

    return 0;
}

IEnumerable<int> GetNumbersDrawn(IEnumerable<string> inputRows)
{
    int x = default;
    return inputRows
        .First()
        .Split(',')
        .Where(i => int.TryParse(i, out x))
        .Select(i => x)
        .AsEnumerable();
}

ICollection<Board> GetBoards(IEnumerable<string> inputRows)
{
    return inputRows
        .Skip(1) // skip first row with numbers drawn
        .Select(i => new Board(i))
        .ToList();
}

class Board
{
    int boardSize = 5;
    public List<List<BoardNumber>> BoardNumbers { get; set; }

    public Board(string strBoard)
    {
        int x = default;

        this.BoardNumbers = strBoard
            .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)
            .Select(i2 => i2.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(i3 => int.TryParse(i3?.Trim(), out x))
                .Select(i3 => new BoardNumber(x))
                .ToList())
            .ToList();
    }

    public void AddNumberDrawn(int numberDrawn)
    {
        foreach (var boardRow in BoardNumbers)
        {
            boardRow
                .Where(b => b.Value == numberDrawn)
                .ToList()
                .ForEach(b => { b.Drawn = true; });
        }
    }

    public bool IsComplete() 
        => HasCompleteRow() || HasCompleteColumn();

    bool HasCompleteRow() 
        => BoardNumbers?.Any(b => b.All(n => n.Drawn)) ?? false;

    bool HasCompleteColumn() 
        => Enumerable.Range(0, boardSize).Any(i => BoardNumbers?.All(b => b[i].Drawn) ?? false);

    public int Score()
        => BoardNumbers.Sum(b => b.Where(n => !n.Drawn).Sum(n => n.Value));
}

record BoardNumber(int Value)
{
    public bool Drawn { get; set; } = false;
}