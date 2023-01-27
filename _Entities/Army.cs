namespace CastleGame;

public class Army
{
    private readonly Soldier[] _soldiers;
    private readonly Point TEXTURE_SIZE = new(16, 17);

    public Army()
    {
        Random R = new();
        _soldiers = new Soldier[20];
        var SpriteSheet = Globals.Content.Load<Texture2D>("entities");

        Rectangle[] textures =
        {
            new Rectangle(0, 0, TEXTURE_SIZE.X, TEXTURE_SIZE.Y) //Basic foot soldier
        };

        Vector2 initial_pos = new(0,0);
        for(int i = 0; i < _soldiers.GetLength(0); i++) 
        {
            Vector2 random_offset = new(R.Next(-40,40), R.Next(-40,40));
            _soldiers[i] = new(SpriteSheet, initial_pos, random_offset, textures[0]);
        }
    }
    public void Update()
    {

        if(InputManager._mouseButton == "left")
        {      
            Vector2 Transform = Vector2.Transform(new(InputManager.MousePosition.X, InputManager.MousePosition.Y), Matrix.Invert(Globals.Camera.TransformMatrix));
            for(int i = 0; i < _soldiers.GetLength(0); i++) 
            {
                _soldiers[i].Move(Transform);
            }
        }            
        for(int i = 0; i < _soldiers.GetLength(0); i++) 
        {
            _soldiers[i].Update();
        }
    }

    public void Draw()
    {
        for(int i = 0; i < _soldiers.GetLength(0); i++)
        {
            _soldiers[i].Draw();
        }
    }
}