namespace GameCoreProject
{
    public class GameConfig
    {
        public Character Player1 { get; set; }
        public Character Player2 { get; set; }

        public GameConfig(Character p1, Character p2)
        {
            Player1 = p1;
            Player2 = p2;
        }
    }
}