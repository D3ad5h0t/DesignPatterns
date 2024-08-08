var game = new Game();
var player = new Player("Sam", game);
var coach = new Coach(game);

player.Score();
player.Score();
player.Score();


public abstract class GameEventArgs : EventArgs
{
    public abstract void Print();
}

public class PlayerScoredEventArgs : GameEventArgs
{
    public string PlayerName;
    public int GoalsScoredSoFar;

    public PlayerScoredEventArgs(string playerName, int goalsScoredSoFar)
    {
        PlayerName = playerName;
        GoalsScoredSoFar = goalsScoredSoFar;
    }

    public override void Print()
    {
        Console.WriteLine($"{PlayerName} has scored! " +
                          $"(their {GoalsScoredSoFar} goal");
    }
}

public class Game
{
    public event EventHandler<GameEventArgs> Events;

    public void Fire(GameEventArgs args)
    {
        Events?.Invoke(this, args);
    }
}

public class Player
{
    private string Name;
    private Game _game;
    private int _goalsScored = 0;

    public Player(string name, Game game)
    {
        Name = name;
        _game = game;
    }

    public void Score()
    {
        _goalsScored++;
        var args = new PlayerScoredEventArgs(Name, _goalsScored);
        _game.Fire(args);
    }
}

public class Coach
{
    private Game _game;

    public Coach(Game game)
    {
        _game = game;

        _game.Events += (sender, args) =>
        {
            if (args is PlayerScoredEventArgs scored && scored.GoalsScoredSoFar < 3)
            {
                Console.WriteLine($"Coach says: well done, {scored.PlayerName}!");
            }
        };
    }
}