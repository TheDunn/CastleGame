namespace CastleGame;

class Camera
{   
    private const float MAX_ZOOM = 0.2f;
    private const float MIN_ZOOM = 3f;
    private float _zoom;
    private int _previousScrollValue = InputManager.ScrollValue;
    private Matrix _transform;
    private static Vector2 _pos;
    private int _viewportWidth;
    private int _viewportHeight;

    public Camera(Viewport viewport)
    {
        _viewportWidth = viewport.Width;
        _viewportHeight = viewport.Height;
        _zoom = 1f;
    }

    public float Zoom
    {
        get { return _zoom; }
        set 
        { 
            _zoom += value; 
            if(_zoom < MAX_ZOOM)
                _zoom = MAX_ZOOM;
            if(_zoom > MIN_ZOOM)
                _zoom = MIN_ZOOM; 
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

        if(InputManager.ScrollValue > _previousScrollValue)
            Zoom = 0.1f;        
        if(InputManager.ScrollValue < _previousScrollValue)
            Zoom = -0.1f;

        _previousScrollValue = InputManager.ScrollValue;

        _pos += movement * 8;
    }

    public Matrix GetTransformation()
    {
        _transform =
            Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
            Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
            Matrix.CreateTranslation(new Vector3(_viewportWidth * 0.5f,
                _viewportHeight * 0.5f, 0));

        return _transform;
    }

}