namespace ConsoleDungeonCrawler
{
    struct UIBox
    {
        public string Name;
        public int PosX;
        public int PosY;
        public int Width;
        public int Height;
        public ConsoleColor BackgroundColor;
        public ConsoleColor TextColor;
        public UIBox(string name, int x, int y, int width, int height, ConsoleColor backgroundColor, ConsoleColor textColor)
        {
            Name = name;
            PosX = x;
            PosY = y;
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
        }
    }
    struct Vector2
    {
        public int X;
        public int Y;
    }
    struct Range
    {
        public bool On;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Range(bool on, int rangeWidth, int rangeHeight)
        {
            On = on;
            Width = rangeWidth;
            Height = rangeHeight;
        }
        public Range(int rangeWidth, int rangeHeight)
        {
            Width = rangeWidth;
            Height = rangeHeight;
            On = true;
        }
    }
    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    enum Tile
    {
        Empty,
        Wall,
        Player,
        Enemy,
        Chest,
        Coin,
        Trap,
        Key,
        Door,
        Computer,
        Script,
        Entry,
        Exit
    }
    enum ItemType
    {
        Weapon = 1,
        Potion = 2,
        Armor = 3,
        Coin = 4
    }
    enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
    enum EnemyType
    {
        Freshmen,
        Junior,
        Senior
    }
    enum StatType
    {
        EnemiesKilled,
        BossesKilled,
        ChestsOpened,
        CoinsCollected,
        TrapsRevealed,
        LevelsPassed
    }
}