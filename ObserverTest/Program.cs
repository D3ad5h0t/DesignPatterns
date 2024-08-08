var game = new Game();
var rat1 = new Rat(game);
var rat2 = new Rat(game);
Console.WriteLine($"Rat 1 Attach: {rat1.Attack}");
Console.WriteLine($"Rat 2 Attach: {rat2.Attack}");
rat2.Dispose();
Console.WriteLine($"Rat 1 Attach: {rat1.Attack}");

public class Game
{
    public event EventHandler<UpdateFieldEventArgs> UpdateFieldEvent;
    
    public void RatsNumberChanged()
    {
        var count = GetSubscriberCount();
        UpdateFieldEvent?.Invoke(this, new UpdateFieldEventArgs
        {
            TotalAttack = count
        });
    }

    private int GetSubscriberCount()
    {
        if (UpdateFieldEvent == null) return 0;

        var subscribers = UpdateFieldEvent.GetInvocationList();

        return subscribers.Length;
    }
}

public class UpdateFieldEventArgs : EventArgs
{
    public int TotalAttack;
}

public class Rat : IDisposable
{
    private Game _game;
    
    public int Attack = 1;
    
    public Rat(Game game)
    {
        _game = game;
        _game.UpdateFieldEvent += HandleUpdateFieldEvent;
        _game.RatsNumberChanged();
    }

    private void HandleUpdateFieldEvent(object sender, UpdateFieldEventArgs e)
    {
        Attack = e.TotalAttack;
    }

    public void Dispose()
    {
        _game.UpdateFieldEvent -= HandleUpdateFieldEvent;
        _game.RatsNumberChanged();
    }
}