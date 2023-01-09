namespace CastleGame;

public static class InputManager
{
    private static KeyboardState _lastKeyboardState;
    private static Point _direction;
    public static Point Direction => _direction;
    public static int ScrollValue => Mouse.GetState().ScrollWheelValue;
    public static Point MousePosition => Mouse.GetState().Position;

    public static void Update()
    {
        var keyboardState = Keyboard.GetState();

        _direction = Point.Zero;

        if (keyboardState.IsKeyDown(Keys.W)) _direction.Y--;
        if (keyboardState.IsKeyDown(Keys.S)) _direction.Y++;
        if (keyboardState.IsKeyDown(Keys.A)) _direction.X--;
        if (keyboardState.IsKeyDown(Keys.D)) _direction.X++;

        _lastKeyboardState = keyboardState;
    }
}