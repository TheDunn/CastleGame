namespace CastleGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;
    private Camera _camera;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.IsFullScreen = true;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        Globals.Content = Content;

        _gameManager = new();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _camera = new Camera(GraphicsDevice.Viewport);
        Globals.SpriteBatch = _spriteBatch;    
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.Update(gameTime);
        _gameManager.Update();
        _camera.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(71,45,60));

        SamplerState state = new SamplerState();
        state.Filter = TextureFilter.Point;

        _spriteBatch.Begin(SpriteSortMode.Deferred,
                        null, state, null, null, null,
                        _camera.GetTransformation());
        _gameManager.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
