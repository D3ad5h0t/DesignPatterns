var numberOfPlayers = 2;
int currentPlayer = 0;
int turn = 1, maxTurn = 10;

void Start()
{
    Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
}

bool HaveWinner() => turn == maxTurn;

void TakeTurn()
{
    Console.WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
    currentPlayer = (currentPlayer + 1) % numberOfPlayers;
}

int WinningPlayer() => currentPlayer;

GameTemplate.Run(Start, TakeTurn, HaveWinner, WinningPlayer);


public class GameTemplate
{
    public static void Run(
        Action start,
        Action takeTurn,
        Func<bool> haveWinner,
        Func<int> winningPlayer)
    {
        start();
        while (!haveWinner())
        {
            takeTurn();
        }

        Console.WriteLine($"Player {winningPlayer()} wins.");
    }
}