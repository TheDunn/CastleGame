namespace CastleGame;

public static class InputManager
{
    private static KeyboardState _lastKeyboardState;
    private static MouseState _lastMouseState;
    private static Point _direction;
    public static Point Direction => _direction;
    public static int ScrollValue => Mouse.GetState().ScrollWheelValue;
    public static Point MousePosition => Mouse.GetState().Position;
    public static string _mouseButton;

    public static void Update()
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        _direction = Point.Zero;

        if (keyboardState.IsKeyDown(Keys.W)) _direction.Y--;
        if (keyboardState.IsKeyDown(Keys.S)) _direction.Y++;
        if (keyboardState.IsKeyDown(Keys.A)) _direction.X--;
        if (keyboardState.IsKeyDown(Keys.D)) _direction.X++;

        _mouseButton = "none";

        if (_lastMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed) _mouseButton = "left";
        if (_lastMouseState.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed) _mouseButton = "right";

        _lastKeyboardState = keyboardState;
        _lastMouseState = mouseState;
    }
}