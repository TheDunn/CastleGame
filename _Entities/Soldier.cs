namespace CastleGame;

public class Soldier
{
    private readonly Texture2D _texture;
    private Vector2 _position;
    private Vector2 _offset;
    private Vector2 _dest;
    private Vector2 Direction { get; set; }
    private readonly Rectangle _textureRect;

    public Soldier(Texture2D texture, Vector2 position, Vector2 random_offset, Rectangle textureRect)
    {
        _texture = texture;
        _textureRect = textureRect;
        _position = position;
        _offset = random_offset;
    }

    public void Move(Vector2 destination)
    {   
        _dest = destination;
        Console.WriteLine(destination);
        Console.WriteLine(_position);
        var movement = Vector2.Zero;
        Direction = Vector2.Normalize(_dest - _position);
    }

    public void Update() 
    {   
        if(Vector2.Distance(_position, _dest) > 0.5f)
        {
            _position += Direction;
        }
    }

    public void Draw()
    {
        var color = Color.White;

        Globals.SpriteBatch.Draw(_texture, Vector2.Add(_position, _offset), _textureRect, color);
    }
}