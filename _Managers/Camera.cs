namespace CastleGame;

class Camera
{
    private Matrix _transform;
    private static Vector2 _pos;
    private int _viewportWidth;
    private int _viewportHeight;

    public Camera(Viewport viewport)
    {
        _viewportWidth = viewport.Width;
        _viewportHeight = viewport.Height;
    }

    public Vector2 Pos
    {
        get { return _pos; }
        set 
        {
            
        }
    }

    public void Update()
    {   
        var movement = Vector2.Zero;
        if(InputManager.Direction != Point.Zero)
        {
            if(InputManager.Direction.X != 0)
                movement.X += 0.2f * InputManager.Direction.X;
            if(InputManager.Direction.Y != 0)
                movement.Y += 0.2f * InputManager.Direction.Y;
        }
        _pos += movement*8;
    }

    public Matrix GetTransformation()
    {
        _transform =
            Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
            // Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
            Matrix.CreateTranslation(new Vector3(_viewportWidth * 0.5f,
                _viewportHeight * 0.5f, 0));

        return _transform;
    }

}