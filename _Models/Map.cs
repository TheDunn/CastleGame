namespace CastleGame;

public class Map
{
    private readonly int MAP_HEIGHT = 5;
    private readonly Point MAP_SIZE = new(200, 200);
    private readonly Point TILE_SIZE = new(16,17);
    private readonly Vector2 MAP_OFFSET = new(2.5f, 2);
    private readonly int[,,] _tilesArray;
    private readonly Tile[,,] _tiles;
    private Point _keyboardSelected = new(0, 0);
    private Tile _lastMouseSelected;

    public Map()
    {
        Random R = new();

        _tilesArray = new int[MAP_HEIGHT, MAP_SIZE.X, MAP_SIZE.Y];
        _tiles = new Tile[MAP_HEIGHT, MAP_SIZE.X, MAP_SIZE.Y];
        var TileSheet = Globals.Content.Load<Texture2D>("tiles");
        float[,] noiseValues = Noise.Calc2D(MAP_SIZE.X, MAP_SIZE.Y, 0.02f);
        // Rectangle[] textures_rect =
        // {
        //     new ,
        // };

        TILE_SIZE.Y = TILE_SIZE.Y / 2;

        for(int x = 0; x < noiseValues.GetLength(0); x++)
        {
            for(int y = 0; y < noiseValues.GetLength(1); y++)
            {
                float noise = noiseValues[x,y]/100;
                _tilesArray[(int)noise,x,y] = 1;
            }
        }

        // for (int z = 0; z < MAP_HEIGHT; z++) 
        // {
        //     for (int y = 0; y < MAP_SIZE.Y; y++)
        //     {
        //         for (int x = 0; x < MAP_SIZE.X; x++)
        //         {
        //             int r = random.Next(0, 2);
        //             _tilesArray[z, x, y] = r;
        //         }
        //     }
        // }        
        
        for (int z = 0; z < MAP_HEIGHT; z++) 
        {
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    var tileVal = _tilesArray[z,x,y];
                    if(tileVal == 1) _tiles[z, x, y] = new(TileSheet, MapToScreen(x, y, z), new Rectangle(16,0,TILE_SIZE.X,17));
                }
            }
        }
    }

    private Vector2 MapToScreen(int mapX, int mapY, int mapZ)
    {
        var screenX = ((mapX - mapY) * TILE_SIZE.X / 2) + (MAP_OFFSET.X * TILE_SIZE.X);
        var screenY = ((mapY + mapX) * TILE_SIZE.Y / 2) - (mapZ * TILE_SIZE.Y) + (MAP_OFFSET.Y * TILE_SIZE.Y);

        return new(screenX, screenY);
    }

    private Point ScreenToMap(Point mousePos)
    {
        Vector2 cursor = new(mousePos.X - (int)(MAP_OFFSET.X * TILE_SIZE.X), mousePos.Y - (int)(MAP_OFFSET.Y * TILE_SIZE.Y));

        var x = cursor.X + (2 * cursor.Y) - (TILE_SIZE.X / 2);
        int mapX = (x < 0) ? -1 : (int)(x / TILE_SIZE.X);
        var y = -cursor.X + (2 * cursor.Y) + (TILE_SIZE.X / 2);
        int mapY = (y < 0) ? -1 : (int)(y / TILE_SIZE.X);

        return new(mapX, mapY); 
    }

    public void Update()
    {
        _lastMouseSelected?.MouseDeselect();

        var map = ScreenToMap(InputManager.MousePosition);

        if (map.X >= 0 && map.Y >= 0 && map.X < MAP_SIZE.X && map.Y < MAP_SIZE.Y)
        {
            // _lastMouseSelected = _tiles[0, map.X, map.Y];
            // _lastMouseSelected.MouseSelect();
        }
    }

    public void Draw()
    {
        for (int z = 0; z < MAP_HEIGHT; z++)
        {
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    if(_tilesArray[z,x,y] != 0) _tiles[z,x,y].Draw();
                }
            }
        }
    }
}