namespace CastleGame;

public class Map
{
    private readonly int MAP_HEIGHT = 8;
    public static Point MAP_SIZE = new(300, 300);
    public static Point TILE_SIZE = new(16,17);
    private readonly int[,,] _tilesArray;
    private readonly Tile[,,] _tiles;

    public Map()
    {
        Random R = new();

        _tilesArray = new int[MAP_HEIGHT, MAP_SIZE.X, MAP_SIZE.Y];
        _tiles = new Tile[MAP_HEIGHT, MAP_SIZE.X, MAP_SIZE.Y];
        var TileSheet = Globals.Content.Load<Texture2D>("tiles");

        Noise.Seed = R.Next();
        Console.WriteLine("Seed {0}", Noise.Seed);
        float[,] noiseValues1 = Noise.Calc2D(MAP_SIZE.X, MAP_SIZE.Y, 0.01f);
        float[,] noiseValues2 = Noise.Calc2D(MAP_SIZE.X, MAP_SIZE.Y, 0.03f); 
        float[,] treeNoise = Noise.Calc2D(MAP_SIZE.X, MAP_SIZE.Y, 0.04f); 
        
        Rectangle[] textures_rect =
        {
            new Rectangle(TILE_SIZE.X,0,TILE_SIZE.X, TILE_SIZE.Y), //Light Grass
            new Rectangle(TILE_SIZE.X*3, TILE_SIZE.Y*2, TILE_SIZE.X, TILE_SIZE.Y), //Dark Grass    
            new Rectangle(TILE_SIZE.X*4,0, TILE_SIZE.X, TILE_SIZE.Y), //Water
            new Rectangle(TILE_SIZE.X*3,0, TILE_SIZE.X, TILE_SIZE.Y), //Sand
            new Rectangle(TILE_SIZE.X*8,TILE_SIZE.Y*3, TILE_SIZE.X, TILE_SIZE.Y), //Tree stump
            new Rectangle(TILE_SIZE.X*9,TILE_SIZE.Y, TILE_SIZE.X, TILE_SIZE.Y), //Tree top
            new Rectangle(TILE_SIZE.X*10,0, TILE_SIZE.X, TILE_SIZE.Y), //Sand plant
            new Rectangle(0,0,TILE_SIZE.X, TILE_SIZE.Y), //Dirt
        };

        TILE_SIZE.Y = (int) (TILE_SIZE.Y-1) / 2;

        for(int x = 0; x < noiseValues1.GetLength(0); x++)
        {
            for(int y = 0; y < noiseValues1.GetLength(1); y++)
            {   
                int noise = (int) (((1 * noiseValues1[x,y] + 0.5 * noiseValues2[x,y]) / (1 + 0.5)) * 3 + 3);
                int tree_noise = (int) (treeNoise[x,y] * 3 + 3);
    
                // Prevent noise height exceeding map height
                if(noise > MAP_HEIGHT)
                    noise = MAP_HEIGHT;

                int tile = 2;   
                if(noise == 0)
                    tile = 3;
                if(noise == 1)
                    tile = 4;

                _tilesArray[noise,x,y] = tile;

                // Add trees
                if(noise > 1 && tree_noise > 4 && R.Next(1,10) == 2)
                    _tilesArray[noise+1,x,y] = 5;   
                    
                if(noise > 1 && tree_noise > 4 && R.Next(1,5) == 2)
                    _tilesArray[noise,x,y] = 8;

                if(noise > 1 && tree_noise == 3 && R.Next(1,30) == 2)
                    _tilesArray[noise+1,x,y] = 6;

                if(noise == 1 && R.Next(1,50) == 2)
                    _tilesArray[noise+1,x,y] = 7;
            }
        }
        
        for (int z = 0; z < MAP_HEIGHT; z++) 
        {
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    var tileVal = _tilesArray[z,x,y];
                    if(tileVal != 0) _tiles[z, x, y] = new(TileSheet, MapToScreen(x, y, (z > 0 ? z-1 : z)), textures_rect[tileVal-1]);
                }
            }
        }
    }

    public static Vector2 MapToScreen(int mapX, int mapY, int mapZ)
    {
        var screenX = ((mapX - mapY) * TILE_SIZE.X / 2);
        var screenY = ((mapY + mapX) * TILE_SIZE.Y / 2) - (mapZ * (TILE_SIZE.Y));

        return new(screenX, screenY);
    }

    public static Vector2 ScreenToMap(Point mousePos)
    {   
        Vector2 cursor = new(mousePos.X - (int)(TILE_SIZE.X), mousePos.Y - (int)(TILE_SIZE.Y));

        var x = cursor.X + (2 * cursor.Y) - (TILE_SIZE.X / 2);
        int mapX = (x < 0) ? -1 : (int)(x / TILE_SIZE.X);
        var y = -cursor.X + (2 * cursor.Y) + (TILE_SIZE.X / 2);
        int mapY = (y < 0) ? -1 : (int)(y / TILE_SIZE.X);

        return new(mapX, mapY); 
    }

    public void Draw()
    {
        for (int y = 0; y < MAP_SIZE.Y; y++)
        {
            for (int x = 0; x < MAP_SIZE.X; x++)
            {
                for (int z = 0; z < MAP_HEIGHT; z++)
                {
                    if(_tilesArray[z,x,y] != 0) _tiles[z,x,y].Draw();
                }
            }
        }
    }
}