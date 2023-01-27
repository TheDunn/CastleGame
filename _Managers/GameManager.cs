namespace CastleGame;

public class GameManager
{
    private readonly Map _map = new();
    private readonly Army _army = new();

    public void Update()
    {
        InputManager.Update();
        _army.Update();
    }

    public void Draw()
    {
        _map.Draw();
        _army.Draw();
    }
}