var chess = new Chess();
chess.Run();


public abstract class Game
{
    protected readonly int NumberOfPlayers;

    protected int CurrentPlayer;
    
    public void Run()
    {
        Start();
        while (!HaveWinner)
        {
            TakeTurn();
        }

        Console.WriteLine($"Player {WinningPlayer} wins!");
    }

    protected abstract void Start();

    protected abstract void TakeTurn();
    
    protected abstract bool HaveWinner { get; }
    
    protected abstract int WinningPlayer { get; }

    public Game(int numberOfPlayers)
    {
        NumberOfPlayers = numberOfPlayers;
    }
}

public class Chess : Game
{
    private int _turn = 1, _maxTurns = 10;
    
    public Chess() : base(2)
    {
    }

    protected override void Start()
    {
        Console.WriteLine($"Starting a game of chess with {NumberOfPlayers} players/");
    }

    protected override void TakeTurn()
    {
        Console.WriteLine($"Turn {_turn++} take by player {CurrentPlayer}");
        CurrentPlayer = (CurrentPlayer + 1) % NumberOfPlayers;
    }

    protected override bool HaveWinner => _turn == _maxTurns;
    protected override int WinningPlayer => CurrentPlayer;
}